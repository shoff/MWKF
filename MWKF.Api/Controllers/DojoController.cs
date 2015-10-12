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
    using MWKF.Api.Entities;
    using MWKF.Api.Entities.Identity;
    using MWKF.Api.Repositories.Interfaces;
    using MWKF.Api.Services.Interfaces;
    using NLog;

    [RoutePrefix("api/v1/dojos")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DojoController : ApiController
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IEntityRepository<Dojo, Guid> dojoRepository;
        private readonly ICacheService cacheService;

        public DojoController(IEntityRepository<Dojo, Guid> dojoRepository, ICacheService cacheService)
        {
            this.dojoRepository = dojoRepository;
            this.cacheService = cacheService;
        }

        [HttpGet]
        [Route("paged/{pagenumber}", Name = "DojosV1")]
        [ResponseType(typeof(SerializablePagination<User>))]
        public async Task<IHttpActionResult> Get(int? pagenumber)
        {
            try
            {
                var dojos = this.cacheService.TryGet<Expression<Func<Dojo, bool>>,
                     Func<IQueryable<Dojo>, IOrderedQueryable<Dojo>>, string,
                     IEnumerable<Dojo>>("Dojos", (x => x != null), null, "Address", this.dojoRepository.Get, null);

                if (dojos != null)
                {
                    var dojoArray = dojos as Dojo[] ?? dojos.ToArray();
                    if (!dojoArray.Any())
                    {
                        // no users? Ok just return
                        return await Task.FromResult(this.Ok());
                    }

                    pagenumber = pagenumber.HasValue ? pagenumber : 0;

                    return  await Task.FromResult((IHttpActionResult)
                        this.Ok(new SerializablePagination<Dojo>(dojoArray.ToList(), (int)pagenumber)));
                }
                return await Task.FromResult((IHttpActionResult)this.NotFound());
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                return this.InternalServerError(e);
            }
        }
    }
}