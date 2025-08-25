using Kaushal_Darpan.Core.Entities;
using Kaushal_Darpan.Core.Helper;

namespace Kaushal_Darpan.Core.Interfaces
{
    public interface IUsersRepository
    {
        Task<int> CreateUser(Users user);
        Task<List<Users>> GetAllUser(GenericPaginationSpecification specification);
        Task<Users> GetUserById(int userId);
        Task<int> UpdateUserById(Users user);
        Task<int> DeleteUserById(Users user);
        Task<Users> GetUserByUserEmailAndPass(string userEmail, string userPassword);

    }
}
