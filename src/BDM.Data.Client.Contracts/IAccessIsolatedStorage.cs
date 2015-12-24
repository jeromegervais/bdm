using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDM.Data.Client.Contracts
{
    public interface IAccessIsolatedStorage
    {
        Task<bool> FileExistsAsync(string fileName);

        Task<string> ReadIsolatedStorageAsync(string fileName);

        Task WriteIsolatedStorageAsync(string fileName, string content);

        Task<DateTimeOffset?> GetCreationDateAsync(string fileName);

        Task<Dictionary<string, DateTimeOffset>> GetCreationDatesAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>null si le fichier n'existe pas.</returns>
		Task<DateTimeOffset?> GetModificationDateAsync(string fileName);

        Task<Dictionary<string, DateTimeOffset>> GetModificationDatesAsync();

        Task DeleteFileAsync(string fileName);
    }
}
