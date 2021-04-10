using BusinessLayer.DataProviderProfilerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DZ3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DataProviderProfilerController : ControllerBase
    {
        private readonly IDataProviderProfilerService _dataProviderProfilerService;
        public DataProviderProfilerController(IDataProviderProfilerService dataProviderProfilerService)
        {
            _dataProviderProfilerService = dataProviderProfilerService;
        }       

        [HttpGet]          
        public ActionResult<List<string>> GetLeadTime()
        {
                var dictionaryResault = _dataProviderProfilerService.ComparePerformance();
            List<string> resList = new List<string>();
            resList.Add("Linq method lead time: " + dictionaryResault["LinqMethodLeadTime"] + " Milliseconds");
            resList.Add("Raw SQL query lead time: " + dictionaryResault["RawSqlQueryLeadTime"] + " Milliseconds");
            return resList;            
        }
    }
}
