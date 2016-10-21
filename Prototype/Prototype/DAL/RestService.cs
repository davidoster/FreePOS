using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Prototype
{
    class RestService : IRestService
    {
        HttpClient client;
        public static User LoggedUser { get; set; }
        public static Company selectedCompany { get; set; }
        public static Store selectedStore { get; set; }
        public static Inventory selectedInventory { get; set; }

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        private dynamic CreateMessage(int operation, dynamic data)
        {
            dynamic message = new JObject();
            message.control = new JObject();
            message.data = new JObject();

            message.control.operation = operation;
            message.data = data;

            return message;
        }

        private void PrepareClient()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", LoggedUser.apikey);
        }

        public async Task<User> Login(string username, string password)
        {
            var uri = new Uri(Constants.RestURL);
            dynamic data = new JObject();
            data.password = password;
            data.email = username;
            dynamic sendContent = CreateMessage(Constants.LOGIN, data);

            try
            {
                var content = new StringContent(sendContent.ToString(), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    byte[] buffer = await response.Content.ReadAsByteArrayAsync();
                    string result = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    Debug.WriteLine(result);
                    ServiceResult sr = JsonConvert.DeserializeObject<ServiceResult>(result);
                    LoggedUser = JsonConvert.DeserializeObject<User>(sr.data.ToString());
                    return LoggedUser;
                }
                else
                {
                    byte[] buffer = await response.Content.ReadAsByteArrayAsync();
                    string result = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    Debug.WriteLine(result);
                    return null;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.WriteLine(e.Message);
            }

            return LoggedUser;
        }

        public async Task<string> SignUp(User user)
        {
            var uri = new Uri(Constants.RestURL);
            var data = JsonConvert.SerializeObject(user);
            dynamic sendContent = CreateMessage(Constants.SIGNUP, data);
            Debug.WriteLine(sendContent.ToString());
            try
            {
                var content = new StringContent(sendContent.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    byte[] buffer = await response.Content.ReadAsByteArrayAsync();
                    string result = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    Debug.WriteLine(result);
                    ServiceResult sr = JsonConvert.DeserializeObject<ServiceResult>(result);
                    LoggedUser = JsonConvert.DeserializeObject<User>(sr.data.ToString());
                    return result;
                }
                else
                {
                    byte[] buffer = await response.Content.ReadAsByteArrayAsync();
                    string result = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    Debug.WriteLine(result);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string>AddCompany(Company company)
        {
            var uri = new Uri(Constants.RestURL);
            var data = JsonConvert.SerializeObject(company);
            dynamic sendContent = CreateMessage(Constants.ADD_COMPANY, data);
            PrepareClient();
            Debug.WriteLine(sendContent.ToString());
            try
            {
                var content = new StringContent(sendContent.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    byte[] buffer = await response.Content.ReadAsByteArrayAsync();
                    string result = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    Debug.WriteLine(result);
                    ServiceResult sr = JsonConvert.DeserializeObject<ServiceResult>(result);
                    selectedCompany = JsonConvert.DeserializeObject<Company>(sr.data.ToString());
                    return result;
                }
                else
                {
                    byte[] buffer = await response.Content.ReadAsByteArrayAsync();
                    string result = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    Debug.WriteLine(result);
                    return null;
                }                

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> AddStore(Store store)
        {
            var uri = new Uri(Constants.RestURL);
            var data = JsonConvert.SerializeObject(store);
            dynamic sendContent = CreateMessage(Constants.ADD_STORE, data);
            PrepareClient();
            Debug.WriteLine(sendContent.ToString());
            try
            {
                var content = new StringContent(sendContent.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    byte[] buffer = await response.Content.ReadAsByteArrayAsync();
                    string result = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    Debug.WriteLine(result);
                    ServiceResult sr = JsonConvert.DeserializeObject<ServiceResult>(result);
                    selectedStore = JsonConvert.DeserializeObject<Store>(sr.data.ToString());
                    return result;
                }
                else
                {
                    byte[] buffer = await response.Content.ReadAsByteArrayAsync();
                    string result = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    Debug.WriteLine(result);
                    return null;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<Company>> FetchCompanies()
        {
            var uri = new Uri(Constants.RestURL);
            dynamic sendContent = CreateMessage(Constants.FETCH_COMPANY, null);
            PrepareClient();
            Debug.WriteLine(sendContent.ToString());

            try
            {
                var content = new StringContent(sendContent.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    byte[] buffer = await response.Content.ReadAsByteArrayAsync();
                    string result = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    Debug.WriteLine(result);
                    ServiceResult sr = JsonConvert.DeserializeObject<ServiceResult>(result);
                    List<Company> companies = JsonConvert.DeserializeObject<List<Company>>(sr.data.ToString());
                    return companies;
                }
                else
                {
                    byte[] buffer = await response.Content.ReadAsByteArrayAsync();
                    string result = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    Debug.WriteLine(result);
                    return null;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
