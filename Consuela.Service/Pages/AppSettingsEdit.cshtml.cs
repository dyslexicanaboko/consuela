using Consuela.Lib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Consuela.Service.Pages
{
    public class AppSettingsEditModel : PageModel
    {
        [BindProperty]
        public string SiteHost { get; set; }

        [BindProperty]
        public int SitePort { get; set; }

        public readonly IAppSettingsService _service;

        public AppSettingsEditModel(IAppSettingsService service)
        {
            _service = service;
        }

        public void OnGet()
        {
            var uri = new Uri(_service.HostUrl);

            SiteHost = uri.Host;
            SitePort = uri.Port;
        }

        public IActionResult OnPostSubmit()
        {
            var uri = new UriBuilder(_service.HostUrl);

            uri.Host = SiteHost;
            uri.Port = SitePort;

            _service.HostUrl = uri.ToString();
            _service.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
