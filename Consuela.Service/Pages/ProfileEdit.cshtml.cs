using Consuela.Entity;
using Consuela.Lib.Services.ProfileManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Consuela.Service.Pages
{
    public class ProfileEditModel : PageModel
    {
        private readonly IProfileSaver _profileSaver;

        /// <summary>Existing profile values only for loading the page.</summary>
        public IProfile Profile => _profileSaver.Get();

        /// <summary>Changed profile values read from the page except for lists.</summary>
        [BindProperty]
        public Profile Edited { get; set; }

        /// <summary>Stand-in property for the Edited.Delete.Schedule.Frequency. This is for POST only.</summary>
        [BindProperty]
        public string FrequencyEnum { get; set; }

        /// <summary>Represents Profile.Ignore.Files property from table on page</summary>
        [BindProperty]
        public List<string> IgnoreFiles { get; set; }
        
        /// <summary>Represents Profile.Ignore.Directories property from table on page</summary>
        [BindProperty]
        public List<string> IgnoreDirectories { get; set; }

        /// <summary>Represents Profile.Delete.Paths property from table on page</summary>
        [BindProperty]
        public List<PathAndPattern> DeletePaths { get; set; }

        public string ExeDirectory => AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Profile saver contains the existing Profile that is saved on disk.
        /// </summary>
        /// <param name="profileSaver">Profile saver that manages the profile</param>
        public ProfileEditModel(IProfileSaver profileSaver)
        {
            _profileSaver = profileSaver;
        }

        public void OnGet()
        {
            //For the checkbox only so it has an initial value
            Edited = new Profile();
            Edited.Audit.Disable = Profile.Audit.Disable;
        }

        public IActionResult OnPostSubmit()
        {
            if (ArePathsEqual(AppDomain.CurrentDomain.BaseDirectory, Edited.Audit.Path))
            {
                return new ObjectResult(new { Message = "Audit path cannot be where the executable resides. Choose another location please." })
                {
                    StatusCode = StatusCodes.Status405MethodNotAllowed
                };
            }

            //Take the Edited profile and combine it together with the lists
            Edited.Ignore.AddFile(IgnoreFiles);
            Edited.Ignore.AddDirectory(IgnoreDirectories);
            Edited.Delete.AddPath(DeletePaths);
            Edited.Delete.Schedule.Frequency = Enum.Parse<ScheduleFrequency>(FrequencyEnum);

            //Writes the profile changes to disk.
            _profileSaver.Save(Edited);

            return RedirectToPage("ProfileView");
        }

        private bool ArePathsEqual(string left, string right)
        {
            //Remove trailing slashes for full comparison
            var diLeft = left.TrimEnd('\\');
            var diRight = right.TrimEnd('\\');

            var areEqual = diLeft == diRight;

            return areEqual;
        }
    }
}
