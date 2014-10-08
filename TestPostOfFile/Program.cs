using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TestPostOfFile
{
    class Program
    {
        static void Main(string[] args)
        {
            var form = new MultipartFormDataContent();
            var streamContent = new StreamContent(File.OpenRead("TestPostOfFile.exe"));
            form.Add(streamContent, "\"file\"", "\"TestPostOfFile.exe\"");

            var client = new HttpClient(); 
            var responseTask = client.PostAsync("http://localhost:50045/api/values",form);
            responseTask.Wait();

            Console.WriteLine(responseTask.Result.StatusCode);

        }
    }
}
