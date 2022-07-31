using Consuela.Lib.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Consuela.Service.Pages
{
    public class IndexModel : PageModel
    {
        public string NextRun { get; set; }

        private readonly ISchedulingService _schedulingService;

        public IndexModel(ISchedulingService schedulingService)
        {
            _schedulingService = schedulingService;

            NextRun = _schedulingService.GetEndDate().ToString("MM/dd/yyyy");
        }

        public void OnGet()
        {

        }
    }
}