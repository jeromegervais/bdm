using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using BDM.Data.Client.Contracts;

namespace BDM.App.UniversalApp
{
    public class AccessIsolatedStorage : IAccessIsolatedStorage
    {
        public StorageFolder IsolatedStorage => ApplicationData.Current.TemporaryFolder;

        public async Task<bool> FileExistsAsync(string fileName)
        {
            return await GetFileAsync(fileName) != null;
        }

        public async Task<string> ReadIsolatedStorageAsync(string fileName)
        {
            if (await FileExistsAsync(fileName))
            {
                StorageFile file = await IsolatedStorage.GetFileAsync(fileName);
                string contentFile = await FileIO.ReadTextAsync(file);
                return contentFile;
            }

            return string.Empty;
        }

        public async Task WriteIsolatedStorageAsync(string fileName, string content)
        {
            StorageFile file = await IsolatedStorage.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, content);
        }

        private async Task<IStorageItem> GetFileAsync(string fileName)
        {
            return await IsolatedStorage.TryGetItemAsync(fileName);
        }

        public async Task<DateTimeOffset?> GetCreationDateAsync(string fileName)
        {
            IStorageItem file = await GetFileAsync(fileName);
            return file != null ? file.DateCreated : (DateTimeOffset?)null;
        }

        public async Task<Dictionary<string, DateTimeOffset>> GetCreationDatesAsync()
        {
            IReadOnlyList<StorageFile> files = await IsolatedStorage.GetFilesAsync();
            return files.ToDictionary(f => f.Name, f => f.DateCreated);
        }

        public async Task<DateTimeOffset?> GetModificationDateAsync(string fileName)
        {
            IStorageItem file = await GetFileAsync(fileName);
            if (file != null)
            {
                var props = await file.GetBasicPropertiesAsync();
                return props.DateModified;
            }
            return null;
        }

        public async Task<Dictionary<string, DateTimeOffset>> GetModificationDatesAsync()
        {
            IReadOnlyList<StorageFile> files = await IsolatedStorage.GetFilesAsync();
            var results = await Task.WhenAll(files.Select(async f => new { f.Name, Properties = await f.GetBasicPropertiesAsync() }));
            return results.ToDictionary(f => f.Name, f => f.Properties.DateModified);
        }

        public async Task DeleteFileAsync(string fileName)
        {
            StorageFile file = await IsolatedStorage.GetFileAsync(fileName);
            await file.DeleteAsync();
        }

        public async Task<IStorageFile> CreateFileAsync(string fileName)
        {
            return await IsolatedStorage.CreateFileAsync(fileName);
        }

        //public async Task<StorageFolder> CreateFolderAsync(string folderName)
        //{
        //    return await IsolatedStorage.CreateFolderAsync(folderName);
        //}
    }
}
