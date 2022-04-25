using EcommerceSolution.BackendAPI.Common;
using EcommerceSolution.BackendAPI.ViewModels.Users;
using System.Threading.Tasks;

namespace EcommerceSolution.BackendAPI.Services.Users
{
    public interface IUserService
    {
        Task<ApiResult<string>> Login(LoginRequest request);
    }
}
