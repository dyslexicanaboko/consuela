using Consuela.Entity;
using Consuela.Lib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Consuela.Service.Pages
{
    public class IndexModel : PageModel
    {
        public string NextRun { get; set; }
        
        public string LastRun { get; set; }
        
        public string AuditFolder { get; set; }

        private readonly ISchedulingService _schedulingService;
        
        private readonly IProfile _profile;

        public IndexModel(ISchedulingService schedulingService, IProfile profile)
        {
            _schedulingService = schedulingService;
            _profile = profile;

            NextRun = _schedulingService.GetEndDate().ToString("MM/dd/yyyy");
            
            LastRun = GetLastRun(_schedulingService.GetLastExecution());

            AuditFolder = _profile.Audit.Path;
        }

        private string GetLastRun(DateTime? dateTime)
        {
            if(dateTime.HasValue) return dateTime.Value.ToString("MM/dd/yyyy");

            return "Not run yet";
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            //Feedback needed? Not sure yet.
            //Would be nice to produce the execution results.
            _schedulingService.TryExecuteAction();

            return RedirectToPage("Index");
        }
    }
}