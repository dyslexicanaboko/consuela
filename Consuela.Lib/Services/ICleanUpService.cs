using Consuela.Entity;

namespace Consuela.Lib.Services
{
    public interface ICleanUpService
    {
        CleanUpResults CleanUp(IProfile profile, bool dryRun);
        
        string WildCardToRegex(string value);
    }
}