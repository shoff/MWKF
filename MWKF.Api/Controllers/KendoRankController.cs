using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using AUSKF.Api.Collections;
using AUSKF.Api.Entities;
using AUSKF.Api.Entities.Identity;
using AUSKF.Api.Repositories.Interfaces;
using AUSKF.Api.Services.Interfaces;
using NLog;

namespace AUSKF.Api.Controllers
{
    [RoutePrefix("api/v1/kendoranks")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class KendoRankController : ApiController
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IEntityRepository<KendoRank, Guid> kendoRankRepository;
        private readonly ICacheService cacheService;

        public KendoRankController(IEntityRepository<KendoRank, Guid> kendoRankRepository, ICacheService cacheService)
        {
            this.kendoRankRepository = kendoRankRepository;
            this.cacheService = cacheService;
        }

        [HttpGet]
        [Route(Name = "KendoRanksV1")]
        [ResponseType(typeof(SerializablePagination<User>))]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var kendoRanks = this.cacheService.TryGet<Expression<Func<KendoRank, bool>>,
                     Func<IQueryable<KendoRank>, IOrderedQueryable<KendoRank>>, string,
                     IEnumerable<KendoRank>>("KendoRanks", (x => x != null), null,
                     null, this.kendoRankRepository.Get, null);

                var kendoRanksArray = kendoRanks as KendoRank[] ?? kendoRanks.ToArray();

                if (!kendoRanksArray.Any())
                {
                    // no users? Ok just return
                    return await Task.FromResult(this.Ok());
                }

               

                return await Task.FromResult((IHttpActionResult)this.Ok(kendoRanksArray));
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                return this.InternalServerError(e);
            }
        }
    }
}