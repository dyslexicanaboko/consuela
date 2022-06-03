using Consuela.Entity;

namespace Consuela.Lib.Services.ProfileManagement
{
    public interface IProfileSaver
    {
        void Load();
        
        void Save();

        void Save(IProfile profileChanges);

        IProfile Get();

        void SetDefaultsAsNeeded(IProfile profile);
    }
}