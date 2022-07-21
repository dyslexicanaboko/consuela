namespace Consuela.Lib.Services
{
    public interface IAppSettingsService
    {
        string HostUrl { get; set; }

        void SaveChanges();
    }
}