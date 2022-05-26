using System;

namespace Consuela.Lib.Services.ProfileManagement
{
    public class ProfileManager
        : IProfileManager
    {
        public delegate void SaveHandler(object sender, EventArgs e);

        private event SaveHandler Save;

        public ProfileWatcher Profile { get; set; }

        public void RegisterSaveDelegate(SaveHandler saveHandler)
        {
            Save += saveHandler;

            Profile.Save += RaiseSaveEvent;
        }

        private void RaiseSaveEvent(object sender, EventArgs e)
        {
            Save?.Invoke(this, new EventArgs());
        }
    }
}
