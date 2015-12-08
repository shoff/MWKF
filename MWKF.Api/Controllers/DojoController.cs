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
using AUSKF.Api.Data;

namespace AUSKF.Api.Controllers
{ 
    [RoutePrefix("api/v1/dojos")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DojoController : ApiController
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IEntityRepository<Dojo, Guid> dojoRepository;
        private readonly ICacheService cacheService;
        private List<Tuple<string, string>> states = new List<Tuple<string, string>>
        {
            new Tuple<string, string>("Alabama", "AK"),
            new Tuple<string, string>("Alaska", "AK"),
            new Tuple<string, string>("Arizona", "AZ"),
            new Tuple<string, string>("Arkansas", "AR"),
            new Tuple<string, string>("California", "CA"),
            new Tuple<string, string>("Colorado", "CO"),
            new Tuple<string, string>("Connecticut", "CT"),
            new Tuple<string, string>("Delaware", "DE"),
            new Tuple<string, string>("Florida", "FL"),
            new Tuple<string, string>("Georgia", "GA"),
            new Tuple<string, string>("Hawaii", "HI"),
            new Tuple<string, string>("Idaho", "ID"),
            new Tuple<string, string>("Illinois", "IL"),
            new Tuple<string, string>("Indiana", "IN"),
            new Tuple<string, string>("Iowa", "IA"),
            new Tuple<string, string>("Kansas", "KS"),
            new Tuple<string, string>("Kentucky", "KY"),
            new Tuple<string, string>("Lousiana", "LA"),
            new Tuple<string, string>("Maine", "ME"),
            new Tuple<string, string>("Maryland", "MD"),
            new Tuple<string, string>("Massachusetts", "MA"),
            new Tuple<string, string>("Michigan", "MI"),
            new Tuple<string, string>("Minnesota", "MN")
        };

        public DojoController(IEntityRepository<Dojo, Guid> dojoRepository, ICacheService cacheService)
        {
            this.dojoRepository = dojoRepository;
            this.cacheService = cacheService;
        }

        [HttpGet]
        [Route("paged/{pagenumber}", Name = "DojosV1")]
        [ResponseType(typeof(SerializablePagination<User>))]
        public async Task<IHttpActionResult> Get(int? pagenumber, [FromUri]Guid? federationId = null, [FromUri]string state = "")
        { 
            try
            { 
                var dojos = this.cacheService.TryGet<Expression<Func<Dojo, bool>>,
                                                     Func<IQueryable<Dojo>, IOrderedQueryable<Dojo>>, 
                                                     string,
                                                     IEnumerable<Dojo>
                                                    >("Dojos", (x => x != null), null, "Address", this.dojoRepository.Get, null);

                if (dojos != null)
                {
                    if (federationId != null)
                    {
                        dojos = dojos.Where<Dojo>(d => d.FederationId == federationId);
                    }
                    
                    if (!string.IsNullOrEmpty(state))
                    {
                        dojos = dojos.Where<Dojo>(d => d.Address.State == state);
                    }

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

        [HttpGet]
        [Route("states", Name = "DojoStatesV1")]
        [ResponseType(typeof(SerializablePagination<User>))]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var dojos = this.cacheService.TryGet<Expression<Func<Dojo, bool>>,
                                                     Func<IQueryable<Dojo>, IOrderedQueryable<Dojo>>,
                                                     string,
                                                     IEnumerable<Dojo>
                                                    >("Dojos", (x => x != null), null, "Address", this.dojoRepository.Get, null);

                if (dojos != null)
                {
                    

                    var dojoArray = dojos as Dojo[] ?? dojos.ToArray();

                    if (!dojoArray.Any())
                    {
                        // no dojos? Ok just return
                        return await Task.FromResult(this.Ok());
                    }

                    var dojoStates = dojos.Select(d => d.Address.State).Distinct().ToList() ;
                    
                    return await Task.FromResult((IHttpActionResult)
                        this.Ok(states.Where(s => dojoStates.Contains(s.Item2)).ToList()));
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