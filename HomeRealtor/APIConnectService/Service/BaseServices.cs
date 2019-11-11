using APIConnectService.Helpers;
using APIConnectService.Models;
using Newtonsoft.Json;
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
        public async Task<ServiceResult> RealEstateMethod(string url, string json, string method,string token)
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(url);
                request.Method = method; 
                request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token);
                request.ContentType = "application/json";
                if (json != string.Empty)
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
                    return new ServiceResult() { Success = true, ExceptionMessage = null, Result = responceFromServer };
                else return new ServiceResult() { Success = true, ExceptionMessage = null, Result = JsonConvert.DeserializeObject<List<RealEstateModel>>(responceFromServer) };
            }
            catch (Exception ex)
            {
                return new ServiceResult() { Success = false, ExceptionMessage = "Error: " + ex.Message, Result = null };
            }
        }
        public async Task<ServiceResult> UserMethod(string url, string json, string method, string token)
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(url);
                request.Method = method;
                request.ContentType = "application/json";
                if (token != string.Empty)
                {
                    request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token);
                }
                if (json != string.Empty)
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
                if (method != "GET")
                    return new ServiceResult() { Success = true, ExceptionMessage = null, Result = responceFromServer };
                else return new ServiceResult() { Success = true, ExceptionMessage = null, Result = JsonConvert.DeserializeObject<List<UserModel>>(responceFromServer) };
            }
            catch (Exception ex)
            {
                return new ServiceResult() { Success = false, ExceptionMessage = "Error: " + ex.Message, Result = null };
            }
        }
        public async Task<ServiceResult> OrderMethod(string url, string json, string method)
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(url);
                request.Method = method;
                request.ContentType = "application/json";
                if (json != string.Empty)
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
                    return new ServiceResult() { Success = true, ExceptionMessage = null, Result = responceFromServer };
                else return new ServiceResult() { Success = true, ExceptionMessage = null, Result = JsonConvert.DeserializeObject<List<OrderModel>>(responceFromServer) };
            }
            catch (Exception ex)
            {
                return new ServiceResult() { Success = false, ExceptionMessage = "Error: " + ex.Message, Result = null };
            }
        }

        public async Task<ServiceResult> AddOrderMethod(string url, string json, string method, string token)
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(url);
                request.Method = method;
                request.ContentType = "application/json";
                request.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {token}");
                if (json != string.Empty)
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
                    return new ServiceResult() { Success = true, ExceptionMessage = null, Result = responceFromServer };
                else return new ServiceResult() { Success = true, ExceptionMessage = null, Result = JsonConvert.DeserializeObject<List<OrderModel>>(responceFromServer) };
            }
            catch (Exception ex)
            {
                return new ServiceResult() { Success = false, ExceptionMessage = "Error: " + ex.Message, Result = null };
            }
        }

        public List<GetListEstateViewModel> GetEstates(string url, string method)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = method;
            WebResponse wr = request.GetResponse();
            string responceFromServer;
            using (Stream streamResponce = wr.GetResponseStream())
            {
                StreamReader reader = new StreamReader(streamResponce);
                responceFromServer = reader.ReadToEnd();
            }
            wr.Close();
            List<GetListEstateViewModel> models = JsonConvert.DeserializeObject<List<GetListEstateViewModel>>(responceFromServer);
            return models;
        }

        public async Task<List<GetListEstateViewModel>> GetFindedEstatesAsync(string url,string json, string method,string token)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = method; 
            request.ContentType = "application/json";
            request.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {token}");
            if (json != string.Empty)
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
            List<GetListEstateViewModel> models = JsonConvert.DeserializeObject<List<GetListEstateViewModel>>(responceFromServer);
            return models;
        }
        public List<TypeViewModel> GetEstateTypes(string url, string method,string token)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = method;
            request.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {token}");
            WebResponse wr = request.GetResponse();
            string responceFromServer;
            using (Stream streamResponce = wr.GetResponseStream())
            {
                StreamReader reader = new StreamReader(streamResponce);
                responceFromServer = reader.ReadToEnd();
            }
            wr.Close();
            List<TypeViewModel> models = JsonConvert.DeserializeObject<List<TypeViewModel>>(responceFromServer);
            return models;
        }
        public async Task<ServiceResult> GetCurrentUser(string url, string token)
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(url);
                request.Method = "GET";
                request.ContentType = "application/json"; 
                request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token);               
                WebResponse wr = await request.GetResponseAsync();
                string responceFromServer;
                using (Stream streamResponce = wr.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(streamResponce);
                    responceFromServer = reader.ReadToEnd();
                }
                wr.Close();
                UserInfoModel m = JsonConvert.DeserializeObject<UserInfoModel>(responceFromServer);
                return new ServiceResult() { Success = true, ExceptionMessage = null, Result = m};
            }
            catch (Exception ex)
            {
                return new ServiceResult() { Success = false, ExceptionMessage = "Error: " + ex.Message, Result = null };
            }
        }
        public async Task<ServiceResult> GetDistricts(string url, string token)
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token);
                WebResponse wr = await request.GetResponseAsync();
                string responceFromServer;
                using (Stream streamResponce = wr.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(streamResponce);
                    responceFromServer = reader.ReadToEnd();
                }
                wr.Close();
                List<DistrictModel> m = JsonConvert.DeserializeObject<List<DistrictModel>>(responceFromServer);
                return new ServiceResult() { Success = true, ExceptionMessage = null, Result = m };
            }
            catch (Exception ex)
            {
                return new ServiceResult() { Success = false, ExceptionMessage = "Error: " + ex.Message, Result = null };
            }
        }
        public async Task<ServiceResult> GetTowns(string url,string token)
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token);
                WebResponse wr = await request.GetResponseAsync();
                string responceFromServer;
                using (Stream streamResponce = wr.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(streamResponce);
                    responceFromServer = reader.ReadToEnd();
                }
                wr.Close();
                List<TownModel> m = JsonConvert.DeserializeObject<List<TownModel>>(responceFromServer);
                return new ServiceResult() { Success = true, ExceptionMessage = null, Result = m };
            }
            catch (Exception ex)
            {
                return new ServiceResult() { Success = false, ExceptionMessage = "Error: " + ex.Message, Result = null };
            }
        }
        public async Task<ServiceResult> GetHomePlaces(string url, string token)
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token);
                WebResponse wr = await request.GetResponseAsync();
                string responceFromServer;
                using (Stream streamResponce = wr.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(streamResponce);
                    responceFromServer = reader.ReadToEnd();
                }
                wr.Close();
                List<HomePlaceModel> m = JsonConvert.DeserializeObject<List<HomePlaceModel>>(responceFromServer);
                return new ServiceResult() { Success = true, ExceptionMessage = null, Result = m };
            }
            catch (Exception ex)
            {
                return new ServiceResult() { Success = false, ExceptionMessage = "Error: " + ex.Message, Result = null };
            }
        }
        public async Task<ServiceResult> GetRegions(string url, string token)
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token);
                WebResponse wr = await request.GetResponseAsync();
                string responceFromServer;
                using (Stream streamResponce = wr.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(streamResponce);
                    responceFromServer = reader.ReadToEnd();
                }
                wr.Close();
                List<RegionModel> m = JsonConvert.DeserializeObject<List<RegionModel>>(responceFromServer);
                return new ServiceResult() { Success = true, ExceptionMessage = null, Result = m };
            }
            catch (Exception ex)
            {
                return new ServiceResult() { Success = false, ExceptionMessage = "Error: " + ex.Message, Result = null };
            }
        }
        public async Task<ServiceResult> GetDistrictTypes(string url, string token)
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token);
                WebResponse wr = await request.GetResponseAsync();
                string responceFromServer;
                using (Stream streamResponce = wr.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(streamResponce);
                    responceFromServer = reader.ReadToEnd();
                }
                wr.Close();
                List<DistrictTypeModel> m = JsonConvert.DeserializeObject<List<DistrictTypeModel>>(responceFromServer);
                return new ServiceResult() { Success = true, ExceptionMessage = null, Result = m };
            }
            catch (Exception ex)
            {
                return new ServiceResult() { Success = false, ExceptionMessage = "Error: " + ex.Message, Result = null };
            }
        }
        public GetRealEstateViewModel GetEstate(string url, string method)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = method;
            request.ContentType = "application/json";
            WebResponse wr = request.GetResponse();
            string responceFromServer;
            using (Stream streamResponce = wr.GetResponseStream())
            {
                StreamReader reader = new StreamReader(streamResponce);
                responceFromServer = reader.ReadToEnd();
            }
            wr.Close();
            return JsonConvert.DeserializeObject<GetRealEstateViewModel>(responceFromServer);
        }

        public ServiceResult GetEstateImages(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                WebResponse wr =  request.GetResponse();
                string responceFromServer;
                using (Stream streamResponse = wr.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(streamResponse);
                    responceFromServer = reader.ReadToEnd();
                }
                wr.Close();
                List<GetEstateImagesViewModel> m = JsonConvert.DeserializeObject<List<GetEstateImagesViewModel>>(responceFromServer); 
                return new ServiceResult() { Success = true, ExceptionMessage = null, Result = m };
            }
            catch (Exception ex)
            {
                return new ServiceResult() { Success = false, ExceptionMessage = "Error: " + ex.Message, Result = null };
            }
        }
    }
}
