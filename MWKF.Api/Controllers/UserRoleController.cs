using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

namespace AUSKF.Api.Controllers
{
    [RoutePrefix("api/v1/userroles")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserRoleController : ApiController
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IEntityRepository<UserRole, Guid> userRoleRepository;
        private readonly ICacheService cacheService;

        public UserRoleController(IEntityRepository<UserRole, Guid> userRoleRepository, ICacheService cacheService)
        {
            this.userRoleRepository = userRoleRepository;
            this.cacheService = cacheService;
        }

        [HttpGet]
        [Route( Name = "UserRolesV1")]
        [ResponseType(typeof(SerializablePagination<User>))]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var userRoles = this.cacheService.TryGet<Expression<Func<UserRole, bool>>,
                     Func<IQueryable<UserRole>, IOrderedQueryable<UserRole>>, string,
                     IEnumerable<UserRole>>("UserRoles", (x => x != null), null,
                     "Users", this.userRoleRepository.Get, null);

                var userArray = userRoles as UserRole[] ?? userRoles.ToArray();

                if (!userArray.Any())
                {
                    // no users? Ok just return
                    return await Task.FromResult(this.Ok());
                }

                
                return await Task.FromResult((IHttpActionResult)this.Ok(userArray));
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                return this.InternalServerError(e);
            }
        }

        [Route("{roleId}", Name = "GetUserRoleByRoleId")]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> Get(string roleId)
        {
            // Todo pass email or username rather than user id?
            try
            {
                Guid id;
                if (Guid.TryParse(roleId, out id))
                {
                    var userRole = await this.userRoleRepository.GetAsync
                        (x => x.RoleId == id, includeProperties: "Users");

                    if (userRole != null)
                    {
                        return this.Ok(userRole);
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
        public async Task<IHttpActionResult> Post([FromBody] UserRole userRole)
        {
            try
            {
                await this.userRoleRepository.InsertAsync(userRole);
                return this.CreatedAtRoute("GetUserRoleByRoleId", new { RoleId = userRole.RoleId}, userRole);
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                return this.InternalServerError(e);
            }
        }
    }
}