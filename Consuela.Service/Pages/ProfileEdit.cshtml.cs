using Consuela.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Consuela.Service.Pages
{
    public class ProfileEditModel : PageModel
    {
        public IProfile Profile { get; set; }

        public ProfileEditModel(IProfile profile)
        {
            Profile = profile;
        }

        public void OnGet()
        {

        }
    }
}
