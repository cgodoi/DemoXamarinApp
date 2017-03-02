using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demoCgodoi.Models;
using System.Net;
using System.IO;
using System.Json;
using Xamarin.Forms;


[assembly: Dependency(typeof(demoCgodoi.Services.MockDataStore))]
namespace demoCgodoi.Services
{
	public class MockDataStore : IDataStore<Dispositivo>
	{
		bool isInitialized;
		List<Dispositivo> dispositivos;

		public async Task<bool> AddItemAsync(Dispositivo item)
		{
			await InitializeAsync();

			dispositivos.Add(item);

			return await Task.FromResult(true);
		}

		public async Task<bool> UpdateItemAsync(Dispositivo item)
		{
			await InitializeAsync();

			var _item = dispositivos.Where((Dispositivo arg) => arg.Id == item.Id).FirstOrDefault();
            dispositivos.Remove(_item);
            dispositivos.Add(item);

			return await Task.FromResult(true);
		}

		public async Task<bool> DeleteItemAsync(Dispositivo item)
		{
			await InitializeAsync();

			var _item = dispositivos.Where((Dispositivo arg) => arg.Id == item.Id).FirstOrDefault();
            dispositivos.Remove(_item);

			return await Task.FromResult(true);
		}

		public async Task<Dispositivo> GetItemAsync(string id)
		{
			await InitializeAsync();

			return await Task.FromResult(dispositivos.FirstOrDefault(s => s.Id == id));
		}

		public async Task<IEnumerable<Dispositivo>> GetItemsAsync(bool forceRefresh = false)
		{
			await InitializeAsync();

			return await Task.FromResult(dispositivos);
		}

		public Task<bool> PullLatestAsync()
		{
			return Task.FromResult(true);
		}


		public Task<bool> SyncAsync()
		{
			return Task.FromResult(true);
		}

		public async Task InitializeAsync()
		{
			/*if (isInitialized)
				return;*/

            dispositivos = new List<Dispositivo>();
            JsonValue mJson = await new WebApiConection().consultaWsDemo(VariablesStatic.webApiDispositivo);
            InterpretaJsonDispositivos(mJson, dispositivos);

            isInitialized = true;


        }

 
        private void InterpretaJsonDispositivos(JsonValue json,  List<Dispositivo> dispositivos)
           {
            // Extract the array of name/value results for the field name "weatherObservation". 
            foreach (JsonValue dispositivoJson in json)
            {
                if (dispositivoJson["id_usuario"] == VariablesStatic.UsuarioID)
                {
                    dispositivos.Add(new Dispositivo { Id = dispositivoJson["Id"], Nombre = dispositivoJson["Nombre"], Descripcion = dispositivoJson["Descripcion"] });
                }
            }
            
        }
    }
}
