using BDM.Data.Client.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDM.Data.Client.Mock
{
    /// <summary>
    /// Mock de IAccessIsolatedStorage utilisant un dictionnaire en mémoire.
    /// </summary>
    public class MockAccessIsolatedStorage : IAccessIsolatedStorage
    {
        private Dictionary<string, MockFile> _mockFiles;

        public MockAccessIsolatedStorage()
        {
            _mockFiles = new Dictionary<string, MockFile>();
        }

        public async Task DeleteFileAsync(string fileName)
        {
            _mockFiles.Remove(fileName);
        }

        public async Task<bool> FileExistsAsync(string fileName)
        {
            return _mockFiles.ContainsKey(fileName);
        }

        public async Task<DateTimeOffset?> GetCreationDateAsync(string fileName)
        {
            MockFile file;
            return _mockFiles.TryGetValue(fileName, out file) ? file.ModificationDate : (DateTimeOffset?)null;
        }

        public async Task<Dictionary<string, DateTimeOffset>> GetCreationDatesAsync()
        {
            return _mockFiles.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.CreationDate);
        }

        public async Task<DateTimeOffset?> GetModificationDateAsync(string fileName)
        {
            MockFile file;
            return _mockFiles.TryGetValue(fileName, out file) ? file.ModificationDate : (DateTimeOffset?)null;
        }

        public async Task<Dictionary<string, DateTimeOffset>> GetModificationDatesAsync()
        {
            return _mockFiles.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ModificationDate);
        }

        public async Task<string> ReadIsolatedStorageAsync(string fileName)
        {
            MockFile file;
            return _mockFiles.TryGetValue(fileName, out file) ? file.Content : null;
        }

        public async Task WriteIsolatedStorageAsync(string fileName, string content)
        {
            _mockFiles[fileName] = new MockFile() { Content = content, CreationDate = DateTimeOffset.Now, ModificationDate = DateTimeOffset.Now };
        }

        private class MockFile
        {
            public string Content { get; set; }

            public DateTimeOffset CreationDate { get; set; }

            public DateTimeOffset ModificationDate { get; set; }
        }
    }

    
}
