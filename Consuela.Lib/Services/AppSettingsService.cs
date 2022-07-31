using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

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

        //Instead of trying to deserialize the whole appsettings file into classes
        //I am only going to focus on the properties that need to be configurable
        public void SaveChanges()
        {
            //I'm not crazy about doing it like this, but whatever works for now

            //Get the file path for the appsettings.json
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

            //Read the file's contents
            var content = File.ReadAllText(file);

            //Parse it as JSON
            var node = JsonNode.Parse(content);

            //Set the target node's value
            node!["Kestrel"]!["EndPoints"]!["Http"]!["Url"] = _sectionUrl.Value;

            //Serialize back into a formatted string
            content = node!.ToJsonString(new JsonSerializerOptions { WriteIndented = true });

            //Write the contents back to the file
            File.WriteAllText(file, content);
        }
    }
}
