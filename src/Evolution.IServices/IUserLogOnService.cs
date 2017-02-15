using System.Threading.Tasks;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Framework;
using Microsoft.AspNetCore.Http;

namespace Evolution.Application.SystemManage
{
    public interface IUserLogOnService
    {
        Task<UserLogOnEntity> GetForm(string keyValue,string tenantId);
        Task<int> RevisePassword(string userPassword, string keyValue);
        void SignIn(LoginModel om, HttpContext context);
        void SignOut(HttpContext context);
        Task<int> UpdateForm(UserLogOnEntity userLogOnEntity);
    }
}