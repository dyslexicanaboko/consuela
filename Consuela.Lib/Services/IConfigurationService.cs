namespace Consuela.Lib.Services
{
    //Not sure if I will need any of this right now or at all
    public interface IConfigurationService
    {
        string CachingUri { get; }
        
        int CacheExpirationSeconds { get; }
        
        string MessageQueueUri { get; }
        
        int PartitionWatcherMilliseconds { get; }
    }
}