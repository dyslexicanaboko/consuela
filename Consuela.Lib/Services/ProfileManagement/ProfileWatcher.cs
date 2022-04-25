using Consuela.Entity;
using Consuela.Entity.ProfileParts;
using System;

namespace Consuela.Lib.Services.ProfileManagement
{
    /// <summary>
    /// Watches the properties of the <see cref="IProfile"/> that is loaded into memory to determine when to save changes.
    /// </summary>
    public class ProfileWatcher
        : IProfile
    {
        public delegate void SaveHandler(object sender, EventArgs e);
        
        public event SaveHandler Save;

        /// <inheritdoc/>
        public Ignore Ignore { get; set; }

        /// <inheritdoc/>
        public Logging Logging { get; set; }

        /// <inheritdoc/>
        public Delete Delete { get; set; }

        private void RaiseSaveEvent() => Save?.Invoke(this, new EventArgs());
    }
}
