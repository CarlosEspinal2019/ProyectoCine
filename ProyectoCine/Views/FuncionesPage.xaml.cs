using ProyectoCine.Models;
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
		public FuncionesPage (Pelicula pelicula)
		{
			InitializeComponent ();
		}
	}
}