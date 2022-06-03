using Consuela.Entity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Consuela.Service.Pages
{
    public class ProfileViewModel : PageModel
    {
        public IProfile Profile { get; set; }

        public ProfileViewModel(IProfile profile)
        {
            Profile = profile;
        }

        public void OnGet()
        {

        }
    }
}
