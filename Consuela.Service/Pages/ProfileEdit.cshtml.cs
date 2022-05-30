using Consuela.Entity;
using Consuela.Lib.Services.ProfileManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Consuela.Service.Pages
{
    public class ProfileEditModel : PageModel
    {
        public IProfile Profile { get; set; }

        [BindProperty]
        public ProfileWatcher Edited { get; set; }

        public ProfileEditModel(IProfile profile)
        {
            Profile = profile;
        }

        public void OnGet()
        {

        }

        public void OnPostSubmit()
        {
            var tbody = Request.Form["tbodyIgnoreFiles"];

            if (true) ;
        }
    }
}
