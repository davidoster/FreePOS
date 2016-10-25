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

        public Task<string> AddCompany(Company company)
        {
            return restService.AddCompany(company);
        }

        public Task<string> AddStore(Store store)
        {
            return restService.AddStore(store);
        }

        public Task<List<Company>> FetchCompanies()
        {
            return restService.FetchCompanies();
        }

        public Task<List<Store>> FetchStores()
        {
            return restService.FetchStores();
        }

        public Task<string> AddInventory(Inventory inventory)
        {
            return restService.AddInventory(inventory);
        }

        public Task<List<Inventory>> FetchInventories()
        {
            return restService.FetchInventories();
        }
    }
}
