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
    }
}