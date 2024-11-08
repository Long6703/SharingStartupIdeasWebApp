using Newtonsoft.Json;
using SSI.Services.IService;
using Microsoft.AspNetCore.Http;

namespace SSI.Services.Service
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor HttpContextAccessor)
        {
            _httpContextAccessor = HttpContextAccessor;
        }

        public T GetSession<T>(string key)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var jsonData = session.GetString(key);

            return jsonData != null ? JsonConvert.DeserializeObject<T>(jsonData) : default;
        }

        public void RemoveSession(string key)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.Remove(key);
        }

        public void SetSession<T>(string key, T value)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var jsonData = JsonConvert.SerializeObject(value);
            session.SetString(key, jsonData);
        }
    }
}
