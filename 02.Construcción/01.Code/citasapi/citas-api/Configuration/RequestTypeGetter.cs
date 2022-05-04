using Microsoft.AspNetCore.Http;
using Negocio.Shared;
namespace citas_api.Configuration
{
    public class RequestTypeGetter : IRequestTypeGetter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestTypeGetter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetRequestType()
        {
            return this._httpContextAccessor.HttpContext?.Request?.Method ?? "";
        }
    }
}
