using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    public interface IRestService
    {
        Task<User> Login(string username, string password);
        Task<string> SignUp(User user);
        Task<string> AddCompany(Company company);
        Task<string> AddStore(Store store);
        Task<List<Company>> FetchCompanies();
    }
}
