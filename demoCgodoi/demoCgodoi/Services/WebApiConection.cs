using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Json;
using System.Threading.Tasks;
using demoCgodoi.Models;
using Newtonsoft.Json;

namespace demoCgodoi.Services
{
    public class WebApiConection
    {
        public async Task<JsonValue> consultaWsDemo(string collection)
        {
            string url = VariablesStatic.WebApiUrl + collection;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (WebResponse response = await request.GetResponseAsync())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));

                    //Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());
                    return jsonDoc;
                }
            }
        }

        public async Task<bool> WebApiGuardaDispositivo(Dispositivo dispositivo, bool isNewItem = false)
        {
            string url = VariablesStatic.WebApiUrl + VariablesStatic.webApiDispositivo;
            HttpClient client = new HttpClient();


            DispositivoWebApi dispositivoWebApi = new DispositivoWebApi();
            HttpResponseMessage response;
            try
            {
                dispositivoWebApi.Id = dispositivo.Id;
                dispositivoWebApi.id_usuario = dispositivo.Id_Usuario;
                dispositivoWebApi.Nombre = dispositivo.Nombre;
                dispositivoWebApi.Descripcion = dispositivo.Descripcion;

                if (isNewItem)
                {
                    dispositivoWebApi.Id = string.Empty;
                }
                var data = JsonConvert.SerializeObject(dispositivoWebApi);
                var content = new StringContent(data, Encoding.UTF8, "application/json");


                if (isNewItem)
                {
                    response = await client.PostAsync(url, content);
                }
                else
                {

                    response = await client.PutAsync(url, content);
                }

                var result = response.Content.ReadAsStringAsync().Result;

                Console.Out.WriteLine(result.ToString());
                return true;


            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message.ToString());
                return false;
            }
          


        }
        public async Task<bool> WebApiEliminaDispositivo(string id)
        {
            string url = VariablesStatic.WebApiUrl + VariablesStatic.webApiDispositivo + "/" + id;
            HttpClient client = new HttpClient();


            DispositivoWebApi dispositivoWebApi = new DispositivoWebApi();
            try
            {
                await client.DeleteAsync(url);
                return true;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message.ToString());
                return false;
            }

        }

    }

  

    public class DispositivoWebApi
    {
        public string Id { get; set; }
        public string id_usuario { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

    }
}
