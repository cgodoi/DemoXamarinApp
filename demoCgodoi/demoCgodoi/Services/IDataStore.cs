using System.Collections.Generic;
using System.Threading.Tasks;

namespace demoCgodoi.Services
{
	public interface IDataStore<T>
	{
		Task<bool> AddItemAsync(T dispositivo);
		Task<bool> UpdateItemAsync(T dispositivo);
		Task<bool> DeleteItemAsync(T dispositivo);
		Task<T> GetItemAsync(string id);
		Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

		Task InitializeAsync();
		Task<bool> PullLatestAsync();
		Task<bool> SyncAsync();
	}
}
