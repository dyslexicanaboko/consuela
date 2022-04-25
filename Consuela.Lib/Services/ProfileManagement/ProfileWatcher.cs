using Consuela.Entity;
using Consuela.Entity.ProfileParts;
using System;

namespace Consuela.Lib.Services.ProfileManagement
{
    public class ProfileWatcher
        : IProfile
    {
        public delegate void SaveHandler(object sender, EventArgs e);
        
        public event SaveHandler Save;

        public Ignore Ignore { get; set; }

        public Logging Logging { get; set; }

        public Delete Delete { get; set; }

        private void RaiseSaveEvent() => Save?.Invoke(this, new EventArgs());
    }
}
