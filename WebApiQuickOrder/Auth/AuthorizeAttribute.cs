using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiQuickOrder.Controllers;

namespace WebApiQuickOrder.Auth
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(PermissionItem item, PermissionAction action)
        : base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] { item, action };
        }
    }

    public class AuthorizeActionFilter : IAuthorizationFilter
    {
        private readonly PermissionItem _item;
        private readonly PermissionAction _action;
        private AuthController _authService;

        public AuthorizeActionFilter(PermissionItem item, PermissionAction action, AuthController authService)
        {
            _item = item;
            _action = action;
            _authService = authService;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            //bool isAuthorized = _authService.IsUserAuthorized(context, _item, _action);
            //CREATE AUTH LOGIC
            //if (!isAuthorized)
            //{
            //    context.Result = new UnauthorizedResult();
            //}
        }
    }
}
