using BLL;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Web.Http.Results;

namespace WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        public  string AddEditProduct(HttpRequestMessage request)
        {
            var content = request.Content;
            string jsonContent = content.ReadAsStringAsync().Result;
            //ProductList IR = JsonConvert.DeserializeObject<ProductList>(jsonContent);
            //JObject studentObj = JObject.Parse(jsonContent);

            var myList = JsonConvert.DeserializeObject<List<Product>>("[" + ((Newtonsoft.Json.Linq.JContainer)JObject.Parse(jsonContent).First).First + "]");
            DAL obj = new DAL();
            try
            {
                return obj.AddProduct(myList[0]).ToString();
            }
            catch (Exception ex)
            {
                return "ERROR";
            }

        }


        [HttpPost]
        public string DeleteProduct(HttpRequestMessage request)
        {
            DAL obj = new DAL();
            try
            {
                var content = request.Content;
                string jsonContent = content.ReadAsStringAsync().Result;
                var myList = JsonConvert.DeserializeObject<List<Product>>("[" + ((Newtonsoft.Json.Linq.JContainer)JObject.Parse(jsonContent).First).First + "]");
                return obj.DeleteProduct(myList[0].Id).ToString();
            }
            catch (Exception ex)
            {
                return "ERROR";
            }

        }

        [HttpGet]
        public System.Web.Mvc.JsonResult GetAllorSingleProducts( int id)
        {
            DAL obj = new DAL();
            try
            {
                var data= DataTableToJSONWithJSONNet(obj.GetAllorSingleProducts(id).Tables[0]);
                var result = JsonConvert.DeserializeObject<List<Product>>(data);
                return new System.Web.Mvc.JsonResult()
                {
                    Data = result,
                    JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet,
                    MaxJsonLength = Int32.MaxValue
                };
            }
            catch (Exception ex)
            {
                return new System.Web.Mvc.JsonResult()
                {
                    Data = "NULL",
                    JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet,
                    MaxJsonLength = Int32.MaxValue
                };
            }

        }

        public string DataTableToJSONWithJSONNet(System.Data.DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }
    }
}
