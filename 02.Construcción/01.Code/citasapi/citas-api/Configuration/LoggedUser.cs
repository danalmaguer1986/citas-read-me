using Microsoft.AspNetCore.Http;
using Negocio.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace citas_api.Configuration
{
    public class LoggedUser : ILoggedUser
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public LoggedUser(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            this.LoadUser();
        }

        private void LoadUser()
        {
            if (_contextAccessor.HttpContext == null) return;

            var webUser = _contextAccessor.HttpContext.User;
            this.IsAnonymous = this.GetValue(webUser.Claims, "username") == null;
            this.UserName = this.GetValue(webUser.Claims, "username") ?? "Anónimo";
            var menuIdsString = this.GetValue(webUser.Claims, "menuIds") ?? "";
            if (menuIdsString != "")
            {
                this.MenuIds = menuIdsString.Split(',').Select(int.Parse).ToList();
            }
        }

        public bool IsAnonymous { get; set; }

        public string UserName { get; set; } = "Anónimo";
        public List<int> MenuIds { get; set; } = new List<int>();

        public string GetValue(IEnumerable<Claim> claims, string type)
        {
            return claims == null ? "" : claims.Where(e => e.Type == type).Select(e => e.Value).FirstOrDefault();
        }
    }
}
