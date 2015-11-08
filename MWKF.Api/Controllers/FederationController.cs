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
using AUSKF.Api.Repositories.Interfaces;
using AUSKF.Api.Services.Interfaces;
using NLog;

namespace AUSKF.Api.Controllers
{
    [RoutePrefix("api/v1/federations")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FederationController : ApiController
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IEntityRepository<Federation, Guid> federationRepository; 
        private readonly ICacheService cacheService;

        public FederationController(IEntityRepository<Federation, Guid> federationRepository, ICacheService cacheService)
        {
            this.federationRepository = federationRepository;
            this.cacheService = cacheService;    
        }

       [HttpGet]
       [Route(Name = "FederationsV1")]
       [ResponseType(typeof(SerializablePagination<Federation>))]
       public async Task<IHttpActionResult> Get()
       {
            try
            {
                var federations = this.cacheService.TryGet<Expression<Func<Federation, bool>>,
                    Func<IQueryable<Federation>, IOrderedQueryable<Federation>>, string,
                    IEnumerable<Federation>>("Federations", (x => x != null), null,
                    null, this.federationRepository.Get, null);

                var federationsArray = federations as Federation[] ?? federations.ToArray();

                if (!federationsArray.Any())
                {
                    return await Task.FromResult(this.Ok());
                }
               
                return await Task.FromResult((IHttpActionResult)this.Ok(federationsArray));
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                return this.InternalServerError(e);
            }
       }
    }
}