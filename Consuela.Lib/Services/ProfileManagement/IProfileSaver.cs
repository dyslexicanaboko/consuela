using Consuela.Entity;
using static Consuela.Lib.Services.ProfileManagement.ProfileSaver;

namespace Consuela.Lib.Services.ProfileManagement
{
    public interface IProfileSaver
    {
        void Load();
        
        void Save();

        void Save(IProfile profileChanges);

        IProfile Get();

        void SetDefaultsAsNeeded(IProfile profile);

        event ProfileChanged Changed;
    }
}