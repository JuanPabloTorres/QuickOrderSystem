using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiQuickOrder.Models
{
    public class Policies
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string Employee = "Employee";

        public const string UserAndEmployees = "UserAndEmployees";
        public const string StoreControl = "StoreControl";


        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();
        }

        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).Build();
        }

        public static AuthorizationPolicy EmployeePolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Employee).Build();
        }

        public static AuthorizationPolicy UserAndEmployeesPolicy()
        {
            IList<string> roles = new List<string>() { "User", "Admin" };
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(roles).Build();
        }

        public static AuthorizationPolicy StoreControlPolicy()
        {          
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(StoreControl).Build();
        }
    }
}
