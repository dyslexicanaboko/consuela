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

        [BindProperty]
        public List<string> IgnoreFiles { get; set; }
        
        [BindProperty]
        public List<string> IgnoreDirectories { get; set; }

        [BindProperty]
        public List<PathAndPattern> DeletePaths { get; set; }

        //Incoming model is the profile that is loaded from the container
        public ProfileEditModel(IProfile profile)
        {
            Profile = profile;
        }

        public void OnGet()
        {

        }

        public void OnPostSubmit()
        {
            //Take the Edited profile and combine it together with the lists
            Edited.Ignore.AddFile(IgnoreFiles);
            Edited.Ignore.AddDirectory(IgnoreDirectories);
            Edited.Delete.AddPath(DeletePaths);
        }
    }
}
