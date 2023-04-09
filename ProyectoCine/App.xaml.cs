using ProyectoCine.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoCine
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new CarteleraPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
