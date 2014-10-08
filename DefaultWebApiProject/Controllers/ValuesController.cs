using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace DefaultWebApiProject.Controllers
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
        public async Task<HttpResponseMessage> Post(HttpRequestMessage message)
        {
            var provider = new MultipartMemoryStreamProvider();
            await message.Content.ReadAsMultipartAsync(provider);

            var fileContent = provider.Contents.FirstOrDefault(content => content.Headers.ContentDisposition.Name.Trim('\"') == "file");
            if (fileContent == null) return new HttpResponseMessage(HttpStatusCode.BadRequest);

            var fileBytes = await fileContent.ReadAsByteArrayAsync();

            var response = new HttpResponseMessage(HttpStatusCode.OK) {Content = new ByteArrayContent(fileBytes)};
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
//            var fileName = Path.GetFileNameWithoutExtension(fileContent.Headers.ContentDisposition.FileName);
//            fileName = Path.ChangeExtension(fileName, ".png");

            response.Content.Headers.ContentDisposition.FileName = fileContent.Headers.ContentDisposition.FileName;

            response.Content.Headers.ContentType = fileContent.Headers.ContentType;

            return response;
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