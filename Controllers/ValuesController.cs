using DDWebAPI.Models;
using System;
using System.Collections.Generic;
//using System.Data;
//using System.Data.OracleClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DDWebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public BillModel Get(string id)
        {
            //return "value";
            //return new BillModel();
            //BillModel bm = new BillModel().getData(id);//BillModel.returnDummy();
            //BillModel bm = new BillModel().getActivityData(id);
            string[] keyWord = { "dispatched", "Completed", "completed", "Dispatched", "Supplement" };
            BillModel bm = new BillModel().getFilteredActivityData(id, keyWord);            
            return bm;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
        
    }
}