using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APIConnectService.Service
{
    public class GetUsersPaginationService
    {
        public string GetPagin(string url, int value)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url+value.ToString());
            request.Method = "GET";
            request.ContentType = "application/json";
            WebResponse response = request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
        public string Ban(string url,string email)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url + email);
            request.Method = "GET";
            request.ContentType = "application/json";
            WebResponse response = request.GetResponse();
            return "User BANED";

        }
    }
}
