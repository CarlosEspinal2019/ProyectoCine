﻿using ProyectoCine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoCine.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FuncionesPage : ContentPage
	{
		private Pelicula pelicula;
		public FuncionesPage (Pelicula pelicula)
		{
			InitializeComponent ();
			this.pelicula = pelicula;
			BindingContext = pelicula;
		}

		private async void Funcion_Selected (object sender, SelectedItemChangedEventArgs e)
		{
			string cantidad = CantidadBoletos.Text;
			if (string.IsNullOrEmpty(cantidad))
			{
				await DisplayAlert("Validacion", "Debe ingresar la cantidad de boletos", "OK");
				return;
			}

			int cant = Convert.ToInt32(cantidad);
			var funcion = (Funcion)e.SelectedItem;

			await Navigation.PushAsync(new ResumenPage(pelicula, funcion, cant));
		}
	}
}