Registers Application Insights for logging and telemetry information.

Add the assembly name:
    Tiny.Framework.Web.Redis

to your
    participating_assemblies.json

file found in the assets folder of your application.

Add the the contents of:

"StorageCache": {
    "ConnectionString": "your redis connection string",
    "ItemLifetimeInMinutes": 5
}

to your app settings file.

you will need to reference this pacakge in order to comnsume the services offered, which are:

   public interface IProvideCacheStorage
    {
        Task<TCacheItem> GetItemFor<TCacheItem>(string cacheKey);
        Task SetItemFor<TCacheItem>(string cacheKey, TCacheItem cacheItem);
        Task ClearItemFor(string cacheKey);
    }
