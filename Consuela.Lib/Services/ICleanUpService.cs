using Consuela.Entity;

namespace Consuela.Lib.Services
{
    public interface ICleanUpService
    {
        void CleanUp(IProfile profile, bool dryRun);
        string WildCardToRegex(string value);
    }
}