using Newtonsoft.Json;
using ProyectoCine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace ProyectoCine.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroPage : ContentPage
    {
        private Login login;
        public RegistroPage()
        {
            InitializeComponent();
            
        }

        //public async void Crea_Clicked(object sender, EventArgs e) 
        //{
        //    var usuario = email.Text;
        //    var password = pass.Text;
        //    //var nombre = nombre.Text;
        //    //var apellido =apellido.Text;

        //    //var httpClientHandler = new HttpClientHandler();

        //    //httpClientHandler.ServerCertificateCustomValidationCallback =
        //    //(message, cert, chain, errors) => { return true; };
        //    //HttpClient client = new HttpClient(httpClientHandler);
        //    //client.BaseAddress = new Uri("https://54.162.134.64:443");

        //    Login log = new Login
        //    {
        //        Usuario = email.Text,
        //        Password = pass.Text
        //    };
        //    Uri RequestUri = new Uri("https://54.162.134.64/apiPrueba/seguridad.php");
        //    var httpClientHandler = new HttpClientHandler();

        //    httpClientHandler.ServerCertificateCustomValidationCallback =
        //    (message, cert, chain, errors) => { return true; };
        //    //HttpClient client = new HttpClient(httpClientHandler);
        //    var client = new HttpClient(httpClientHandler);
        //    var json = JsonConvert.SerializeObject(log);
        //    var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
        //    var response = await client.PostAsync(RequestUri, contentJson);
        //    if (response.StatusCode == HttpStatusCode.OK)
        //    {
        //        //await Navigation.PushAsync(new CarteleraPage());
        //        await Navigation.PushAsync(new LoginPage());
        //    }
        //    else
        //    {
        //        await DisplayAlert("Mensaje", "Datos invalidos", "OK");
        //    }

        //}

        public async void Registro_Clicker(object sender, EventArgs e)
        {
            var usuario = email.Text;
            var password = pass.Text;
            // Crear un objeto con los datos a enviar
            var datos = new Dictionary<string, string>
             {
                { "Usuario", usuario },
                { "password", password }
             };

            // Convertir el objeto a JSON
            string json = JsonConvert.SerializeObject(datos);

            // Crear una instancia de HttpClient
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            var client = new HttpClient(httpClientHandler);
            // Establecer la URL de la API PHP
            string url = "https://54.162.134.64/seguridad.php";

            // Crear el contenido de la solicitud con el JSON como contenido
            StringContent contenido = new StringContent(json, Encoding.UTF8, "application/json");

            // Realizar la solicitud POST
            HttpResponseMessage respuesta = await client.PostAsync(url, contenido);

            // Procesar la respuesta
            if (respuesta.IsSuccessStatusCode)
            {
                await Navigation.PushAsync(new LoginPage());
                // La solicitud se realizó con éxito
                // Puedes manejar la respuesta aquí si es necesario
            }
            else
            {
                await DisplayAlert("Mensaje", "Datos invalidos", "OK");
                // La solicitud falló
                // Puedes manejar el error aquí si es necesario
            }
        }
    }
}