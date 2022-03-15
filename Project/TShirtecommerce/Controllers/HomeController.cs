using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TShirtecommerce.Models;

namespace TShirtecommerce.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProducts(int Id)
        {
            int idProd = 0;
            if(Id>0)
            {
                idProd = Id;
            }
            APiAccessLib.ApiAccessClass objda;
            objda = new APiAccessLib.ApiAccessClass();
            var response = await objda.GetAsyn("api/values/GetAllorSingleProducts?Id=" + idProd.ToString());
            return new JsonResult()
            {
                Data = response,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };

        }


        [HttpPost]
        public async Task<JsonResult> SaveImage(Product prod)
        {
            try
            {
                var postedFiles = Request.Files;
                string[] files = postedFiles.AllKeys;
                var responseText = string.Empty;
                string filename = "";
                if (files != null && files.Length > 0)
                {
                    foreach (var keys in files)
                    {
                        var qId = keys.Split('_')[0];
                        var file = Request.Files[keys];
                        FileInfo fi = new FileInfo(file.FileName);
                        filename = "ProdImage" + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + fi.Extension;
                        file.SaveAs(Server.MapPath("~/Content/ProdImages/") + filename);
                    }

                }

                Product IR = JsonConvert.DeserializeObject<Product>(Request["Result"]);
                APiAccessLib.ApiAccessClass objda;
                IR.Image = filename;
                objda = new APiAccessLib.ApiAccessClass();
                string json = JsonConvert.SerializeObject(IR, Formatting.None);
                var response = await objda.PostAsyn("api/values/AddEditProduct", json, null);

                return new JsonResult()
                {
                    Data = "SUccess",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = Int32.MaxValue
                };
            }
            catch (Exception ex)
            {
                return new JsonResult()
                {
                    Data = "Failure",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = Int32.MaxValue
                };
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteProd(Product prod)
        {
            APiAccessLib.ApiAccessClass objda;
            objda = new APiAccessLib.ApiAccessClass();
            var response = await objda.PostAsyn("api/values/DeleteProduct", prod, null);
            return new JsonResult()
            {
                Data = response,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }
    }
}