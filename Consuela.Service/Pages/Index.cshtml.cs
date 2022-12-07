using Consuela.Lib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Consuela.Service.Pages
{
    public class IndexModel : PageModel
    {
        public string NextRun { get; set; }
        public string LastRun { get; set; }

        private readonly ISchedulingService _schedulingService;

        public IndexModel(ISchedulingService schedulingService)
        {
            _schedulingService = schedulingService;

            NextRun = _schedulingService.GetEndDate().ToString("MM/dd/yyyy");
            
            LastRun = GetLastRun(_schedulingService.GetLastExecution());
        }

        private string GetLastRun(DateTime? dateTime)
        {
            if(dateTime.HasValue) return dateTime.Value.ToString("MM/dd/yyyy");

            return "Not run yet";
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostSubmit()
        {
            //Feedback needed? Not sure yet.
            //Would be nice to produce the execution results.
            _schedulingService.TryExecuteAction();

            return RedirectToPage("Index");
        }
    }
}