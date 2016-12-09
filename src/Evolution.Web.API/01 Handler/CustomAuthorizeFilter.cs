using Evolution.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Text;
using System;
using Evolution.Application.SystemManage;

namespace Evolution.Web.API
{
    /// <summary>
    /// 用于权限控制
    /// </summary>
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }

        public string Permission { get; }
    }

    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly RoleAuthorizeApp roleAuth;

        public PermissionHandler(RoleAuthorizeApp roleAuth)
        {
            if (roleAuth == null)
                throw new ArgumentNullException(nameof(roleAuth));

            this.roleAuth = roleAuth;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User.Identity==null)
            {
                // no user authorizedd. Alternatively call context.Fail() to ensure a failure 
                // as another handler for this requirement may succeed
                return null;
            }
            if(!context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return Task.FromResult(1);
            }
            //check permission
            //bool hasPermission = roleAuth.CheckPermissionForUser(context);
            bool hasPermission = true;
            if (hasPermission)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
                //if (OperatorProvider.Provider.GetCurrent(context.HttpContext) == null)
                //{
                    //var response = context..Response;
                    //WebHelper.WriteCookie("nfine_login_error", "overdue", context.HttpContext);
                    //string s = "<script>top.location.href = '/Login';</script>";
                   // byte[] data = Encoding.UTF8.GetBytes(s);
                    //response.ContentType = "application/json";
                    //context.HttpContext.Response.Body.Write(data, 0, data.Length);

                    return Task.FromResult(1);
                //}
            }
            return Task.FromResult(1);
        }
    }

    public class CustomAuthorizeFilter : AuthorizeFilter
    {
        private AuthorizationPolicy policy = null;
        public CustomAuthorizeFilter(AuthorizationPolicy policy)
            : base(policy)
        {
            this.policy = policy;
        }

        public override Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                return Task.FromResult<int>(0);
            }
            
            return base.OnAuthorizationAsync(context);
        }

    }
}