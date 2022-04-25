using Consuela.Entity;
using System;

namespace Consuela.Lib.Services.ProfileManagement
{
    public interface IProfileSaver
    {
        ProfileManager Load();
        
        void Save();

        void SetDefaultsAsNeeded(IProfile profile);

        void SaveHandler(object sender, EventArgs e);
    }
}