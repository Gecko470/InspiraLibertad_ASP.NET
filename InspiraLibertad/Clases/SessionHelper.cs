using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace InspiraLibertad.Clases
{
    public class SessionHelper
    {
        public static string GetValue(IPrincipal user, string property)
        {
            var r = ((ClaimsIdentity)user.Identity).FindFirst(property);
            return r == null ? "" : r.Value;
        }

        public static string GetNameIndentifier(IPrincipal user)
        {
            var r = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier);
            return r == null ? "" : r.Value;
        }

        public static string GetName(IPrincipal user)
        {
            var r = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Name);
            return r == null ? "" : r.Value;
        }
    }
}
