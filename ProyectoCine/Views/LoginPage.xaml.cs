using Newtonsoft.Json;
using ProyectoCine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoCine.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

		private async void Ingresar_Click(object sender, EventArgs e) 
		{
			var usuario = Usuario.Text;
			var password = Password.Text;

			if (string.IsNullOrEmpty(usuario))
			{
				await DisplayAlert("Validacion","Ingrese el usuario", "Acepatar");
				Usuario.Focus();
				return;
			}

            if (string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Validacion", "Ingrese el password", "Acepatar");
                Usuario.Focus();
                return;
            }

            if (Password.Text.Length < 8)
            {
                await this.DisplayAlert("Validacion", "La contraseña debe ser mayor o igual a 8 dígitos. ", "Aceptar");
                return;
            }

            bool isEmail = Regex.IsMatch(Usuario.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (!isEmail)
            {
                await this.DisplayAlert("Validacion", "El formato del correo electrónico es incorrecto, revíselo e intente de nuevo.", "Aceptar");
                return;
            }

            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            HttpClient client = new HttpClient(httpClientHandler);
            client.BaseAddress = new Uri("https://54.162.134.64:443");

            var autenticacion = new Autentication
            {
                Usuario = usuario,
                Password = password
            };

            string json = JsonConvert.SerializeObject(autenticacion);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = await client.PostAsync("/apiPrueba/seguridad.php", content);
            if (request.IsSuccessStatusCode)
            {
                var responseJson = await request.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<Respuesta>(responseJson);
                
                if(respuesta.EsPermitido) 
                {
                   await Navigation.PushAsync(new CarteleraPage());
                }
                else
                {
                    await DisplayAlert("Lo sentimos", respuesta.Mensaje, "Aceptar");
                }

            }
            else
            {
                await DisplayAlert("Lo sentimos", "Ha ocurrido un problema", "ok");
            }
        }

        private void btn_contraseña_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ResetContasenaPage());
        }

        private void btn_registrate_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistroPage());
        }
    }
}