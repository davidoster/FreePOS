using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    public class RESTManager
    {
        IRestService restService;

        public RESTManager(IRestService service)
        {
            restService = service;
        }

        public Task<User> Login(string username, string password)
        {
            return restService.Login(username, password);
        }

        public Task<string> SignUp(User user)
        {
            return restService.SignUp(user);
        }
    }
}
