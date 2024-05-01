using StudioData.Models.Business;
using System.Security.Claims;

namespace StudioWeb.Helppers
{
    public static class UserHelppers
    {
        public static bool IsAdmin(ClaimsPrincipal User) 
        {
            // Obtener los roles del usuario.
            var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            return roles.Contains("Admin");
        }
    }
}
