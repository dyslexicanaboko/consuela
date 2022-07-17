using Microsoft.Extensions.Configuration;

namespace Consuela.Lib.Services
{
    public class AppSettingsService : IAppSettingsService
    {
        private readonly IConfiguration _configuration;
        private IConfigurationSection _sectionUrl;

        public AppSettingsService(IConfiguration configuration)
        {
            _configuration = configuration;

            _sectionUrl = _configuration
                .GetSection("Kestrel")
                .GetSection("EndPoints")
                .GetSection("Http")
                .GetSection("Url");
        }

        public string HostUrl
        {
            get => _sectionUrl.Value;
            set => _sectionUrl.Value = value;
        }
    }
}
