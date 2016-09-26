using DDWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DDWebAPI.Controllers
{
    public class ProdController : ApiController
    {
        // GET api/values
        public BillModel Get()
        {
            return BillModel.returnDummy();//new string[] { "Dummy", "value" };
        }

        // GET api/values/5
        public BillModel Get(string id)
        {
            //return "value";
            //return new BillModel();
            //BillModel bm = new BillModel().getData(id);//BillModel.returnDummy();
            //BillModel bm = new BillModel().getActivityData(id);
            string[] keyWord = { "dispatched", "Completed", "completed", "Dispatched", "Supplement" };
            BillModel bm = new BillModel().getFilteredActivityDataProd(id, keyWord);
            return bm;
        }

        public BillModel Get(string id,string all)
        {
            //return "value";
            //return new BillModel();
            //BillModel bm = new BillModel().getData(id);//BillModel.returnDummy();
            //BillModel bm = new BillModel().getActivityData(id);
            //string[] keyWord = { "dispatched", "Completed", "completed", "Dispatched", "Supplement" };
            BillModel bm = new BillModel().getActivityDataProd(id);
            return bm;
        }
    }
}
