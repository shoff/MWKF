using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using AUSKF.Api.Attributes;
using AUSKF.Api.Collections;
using AUSKF.Api.Entities.Identity;
using AUSKF.Api.Repositories.Interfaces;
using AUSKF.Api.Services.Interfaces;
using NLog;
using AUSKF.Api.Entities;

namespace AUSKF.Api.Controllers
{
    [RoutePrefix("api/v1/users")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IEntityRepository<User, Guid> userRepository;
        private readonly IEntityRepository<KendoRank, Guid> kendoRankRepository;
        private readonly ICacheService cacheService;
        
        public UserController(IEntityRepository<User, Guid> userRepository, IEntityRepository<KendoRank, Guid> kendoRankRepository, ICacheService cacheService)
        {
            this.userRepository = userRepository;
            this.cacheService = cacheService;
            this.kendoRankRepository = kendoRankRepository;
        }

        [HttpGet]
        [Route("paged/{pagenumber}", Name = "UsersV1")]
        [ResponseType(typeof(SerializablePagination<User>))]
        public async Task<IHttpActionResult> Get(int? pagenumber)
        {
            try
            {
                var users = this.cacheService.TryGet<Expression<Func<User, bool>>,
                     Func<IQueryable<User>, IOrderedQueryable<User>>, string,
                     IEnumerable<User>>("Users", (x => x != null), null, "Profile,KendoRank,Roles,Claims,Logins", this.userRepository.Get, null);

                var userArray = users as User[] ?? users.ToArray();

                if (!userArray.Any())
                {
                    // no users? Ok just return
                    return await Task.FromResult(this.Ok());
                }

                pagenumber = pagenumber.HasValue ? pagenumber : 0;

                return await Task.FromResult((IHttpActionResult)this.Ok(
                    new SerializablePagination<User>(userArray.ToList(), (int)pagenumber)));
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                return this.InternalServerError(e);
            }
        }

        [Route("{userId}", Name = "GetUserById")]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> Get(string userId)
        {
            // Todo pass email or username rather than user id?
            try
            {
                Guid id;
                if (Guid.TryParse(userId, out id))
                {
                    var user = await this.userRepository.GetAsync
                        (x => x.Id == id, includeProperties: "Profile,KendoRank,Roles,Claims,Logins");

                    if (user != null)
                    {
                        return this.Ok(user);
                    }
                }
                else
                {
                    return this.BadRequest(Common.UnableToDetermineId);
                }
            }

            catch (Exception e)
            {
                logger.Error(e, e.Message);
                return this.InternalServerError(e);
            }
            return this.NotFound();
        }

        [Route("")]
        [CheckModelForNull]
        [ValidateModelState]
        public async Task<IHttpActionResult> Post([FromBody] User user)
        {
            try
            {
                var existingUser = this.userRepository.Get(u => u.FirstName == user.FirstName && u.MiddleName == user.MiddleName && u.LastName == user.LastName && u.DateOfBirth == user.DateOfBirth).FirstOrDefault();

                if (existingUser != null)
                {
                    return this.Conflict(); //TODO: Determine if what the appropriate response is if this is not it.
                }
                else
                {
                    var users = this.userRepository.Get().OrderByDescending(u => u.AuskfIdNumber).ToList();

                    user.AuskfIdNumber = users.Count > 0 ? users[0].AuskfIdNumber + 1 : 1;
                    user.PasswordLastChangedDate = DateTime.Now;
                    user.MaximumDaysBetweenPasswordChange = 0;
                    user.PostCount = 0;
                    user.TopicCount = 0;
                    user.JoinedDate = DateTime.Now;
                    user.LastLogin = DateTime.Now;
                    user.Active = true;
                    user.RowVersion = new byte[0];
                    user.EmailConfirmed = false;
                    user.PhoneNumberConfirmed = false;
                    user.TwoFactorEnabled = false;
                    user.LockoutEnabled = false;
                    user.AccessFailedCount = 0;

                    if (user.KendoRank == null)
                    {
                        user.KendoRank = kendoRankRepository.Get(k => k.KendoRankName == "Mudansha").First();
                    }

                    //TODO: Update this assignment
                    user.Profile = new UserProfile()
                    {
                        BirthDay = user.DateOfBirth,
                        Longitude = 0,
                        Latitude = 0,
                        AllowHtmlSig = true
                    };

                    await this.userRepository.InsertAsync(user);
                    return this.CreatedAtRoute("GetUserById", new { userId = user.Id }, user);
                } 
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                return this.InternalServerError(e);
            }
        }

        [Route("{userId}")]
        [CheckModelForNull]
        [ValidateModelState]
        public async Task<IHttpActionResult> Put(string userId, [FromBody] User user)
        {
            try
            {
                Guid id;
                if (Guid.TryParse(userId, out id))
                {
                    await this.userRepository.UpdateAsync(user, user.Id);
                    return this.StatusCode(HttpStatusCode.NoContent);
                }
                return this.BadRequest(Common.UnableToDetermineId);
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                return this.InternalServerError(e);
            }
        }

        [Route("{userId}")]
        [CheckModelForNull]
        [ValidateModelState]
        public async Task<IHttpActionResult> Delete(string userId, [FromBody] User user)
        {
            try
            {
                Guid id;
                if (Guid.TryParse(userId, out id))
                {
                    await this.userRepository.DeleteAsync(user);
                    return this.StatusCode(HttpStatusCode.NoContent);
                }
                logger.Warn(Common.UnableToDetermineId + " passed in parameter was " + userId);
                // don't let them know that an error has occured on delete.
                return this.Ok();
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                // don't let them know that an error has occured on delete.
                return this.Ok();
            }
        }
    }
}