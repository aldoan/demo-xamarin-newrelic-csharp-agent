using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestProjectCSharp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            NewRelic.Agent.onSleep();
        }

        protected override void OnResume()
        {
        }
    }
}
