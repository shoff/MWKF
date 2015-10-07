namespace MWKF.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Web.Http.Description;
    using MWKF.Api.Collections;
    using MWKF.Api.Entities.Identity;
    using MWKF.Api.Repositories.Interfaces;
    using MWKF.Api.Services.Interfaces;
    using NLog;

    [RoutePrefix("api/v1/users")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IEntityRepository<User, Guid> userRepository;
        private readonly ICacheService cacheService;

        public UserController(IEntityRepository<User, Guid> userRepository, ICacheService cacheService)
        {
            this.userRepository = userRepository;
            this.cacheService = cacheService;
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
                     IEnumerable<User>>("Users", (x => x != null), null, "Profile,KendoRank", this.userRepository.Get, null);

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

    }
}