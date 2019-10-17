using APIConnectService.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APIConnectService.Service
{
    public class BaseServices
    {
        public async Task<ServiceResult> RealEstateMethod(string url, string json, string method)
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(url);
                request.Method = method;
                request.ContentType = "application/json";
                if (json!=string.Empty)
                    using (StreamWriter stream = new StreamWriter(request.GetRequestStream()))
                    {
                        stream.Write(json);
                    }
                WebResponse wr = await request.GetResponseAsync();
                string responceFromServer;
                using (Stream streamResponce = wr.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(streamResponce);
                    responceFromServer = reader.ReadToEnd();
                }
                wr.Close();
                if (method == "DELETE" || method == "PUT" || method == "POST")
                    return new ServiceResult() { Success=true, ExceptionMessage=null,Result= responceFromServer };
                else return JsonConvert.DeserializeObject<List<RealEstateModel>>(responceFromServer);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
