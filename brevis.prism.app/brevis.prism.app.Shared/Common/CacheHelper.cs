using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using brevis.prism.app.Infrastructure;
using Windows.Storage;

namespace brevis.prism.app.Common
{
    public static class CacheHelper
    {
        public static async Task ClearCacheAsync()
        {
            var items = await GetCachedItemsAsync();
            var storageFolder = ApplicationData.Current.LocalFolder;
            foreach (var i in items)
            {
                var file = await storageFolder.GetFileAsync(i.Key);
                if (file != null)
                {
                    await file.DeleteAsync();
                }
            }
        }

        public static async Task AddToCache(JsonCacheItem item, string key)
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            await EnsureClearedKeyAsync(key);
            await storageFolder.CreateFileAsync(key, CreationCollisionOption.ReplaceExisting);
            var storageFile = await storageFolder.GetFileAsync(key);
            await FileIO.WriteTextAsync(storageFile, item.JsonResult);
        }

        public static async Task<string> ReadFromCache(string key, int expirationSeconds)
        {
            if (await NeedsRefreshAsync(key, expirationSeconds))
            {
                return string.Empty;
            }            

            var storageFolder = ApplicationData.Current.LocalFolder;
            var storageFile = await storageFolder.GetFileAsync(key);
            return await FileIO.ReadTextAsync(storageFile);
        }

        public static async Task<Dictionary<string, DateTime>> GetCachedItemsAsync()
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            var items = await storageFolder.GetItemsAsync();

            return items.Where(i => i.Name.Contains(".cache"))
                .ToDictionary(i => i.Name, i => i.DateCreated.DateTime);
        }

        private static async Task<bool> NeedsRefreshAsync(string key, int expiredSeconds)
        {
            var cachedItems = await GetCachedItemsAsync();

            var ret = !cachedItems
                .Any(i => i.Key == key && i.Value.AddSeconds(expiredSeconds) >= DateTime.Now);

            return ret;
        }
        private static async Task EnsureClearedKeyAsync(string key)
        {
            var items = await GetCachedItemsAsync();
            if (!items.Any(i => i.Key == key))
            {
                return;
            }

            var storageFolder = ApplicationData.Current.LocalFolder;
            var file = await storageFolder.GetFileAsync(key);
            if (file != null)
            {
                await file.DeleteAsync();
            }
        }
    }
}
