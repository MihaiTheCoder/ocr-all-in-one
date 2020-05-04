using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WindowsOcrWrapper;
using WindowsOcrWrapper.WindowsOcr;

namespace TestWebApp.Controllers
{
    //[Authorize]
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

        /// <summary>
        /// NOT WORKING FOR SOME STRANGE REASON
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Post()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count < 1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            foreach (string file in httpRequest.Files)
            {
                var postedFile = httpRequest.Files[file];
                var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                postedFile.SaveAs(filePath);
                WindowsOcrExecutor executor = new WindowsOcrExecutor();
                var r = executor.GetOcrResult(filePath);
            }

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
