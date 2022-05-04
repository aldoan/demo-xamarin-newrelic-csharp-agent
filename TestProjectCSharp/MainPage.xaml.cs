
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using NewRelic.logging;
using NewRelic.Services.network;
using Xamarin.Forms;

namespace TestProjectCSharp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            NewRelic.Agent.withApplicationToken("AAb299d3a6daac734c7b2dfe9779b0a3a54be75c58-NRMA").withLogLevel(AgentLog.AUDIT).start(Application.Current);
            InitializeComponent();
        }

        int count = 0;
        Dictionary<string, object> data = new Dictionary<string, object>() {
                { "Name", "John" },
                { "Lastname", "Doe"},
                { "Red", "#FF0000"},
                { "Blue", "#0000FF"}
            };

        void Handle_Custom_Event(object sender, System.EventArgs e)
        {
            count++;
            ((Button)sender).Text = $"You sent {count} events.";

            NewRelic.Agent.recordCustomEvent("DemoEventTriggered", data);

        }

        void Handle_Breadcrumb(object sender, System.EventArgs e)
        {
            ((Button)sender).Text = $"Breadcrumb sent";

            NewRelic.Agent.recordCustomEvent("MobileBreadcrumb", data);
        }


        void Handle_Exception(object sender, System.EventArgs e)
        {
            ((Button)sender).Text = $"Handled Exception sent";
            try
            {
                NewRelic.Agent.crashNow();
            }
            catch (Exception exception)
            {
                NewRelic.Agent.recordHandledException(exception, data);

            }


        }

        void Handle_Crash(object sender, System.EventArgs e)
        {
            ((Button)sender).Text = $"Crashing See You";
            //int[] array1 = new int[] { 1, 3, 5, 7, 9 };
            //int value = array1[8];
            NewRelic.Agent.crashNow();

        }

        void Handle_Launch(object sender, System.EventArgs e)
        {
            ((Button)sender).Text = $"App Launch Reported";
            NewRelic.Agent.recordAppLaunch();
        }


        void Send_Get_Request_To_Track(object sender, System.EventArgs e)
        {
            ((Button)sender).Text = $"Get Request Sent";
            _ = GetRequest();
        }

        void Send_Put_Request_To_Track(object sender, System.EventArgs e)
        {
            ((Button)sender).Text = $"Put Request Sent";
            _ = PutRequest();
        }

        void Send_Post_Request_To_Track(object sender, System.EventArgs e)
        {
            ((Button)sender).Text = $"Post Request Sent";
            _ = PostRequest();
        }

        void Send_Delete_Request_To_Track(object sender, System.EventArgs e)
        {
            ((Button)sender).Text = $"Delete Request Sent";
            _ = DeleteRequest();
        }

        static async Task<HttpResponseMessage> GetRequest()
        {
            NetworkRequest client = new NetworkRequest();
            HttpResponseMessage responseBody = await client.GetAsync("https://www.wigilabs.com/");
            return responseBody;
        }

        static async Task<HttpResponseMessage> PutRequest()
        {
            NetworkRequest client = new NetworkRequest();
            HttpContent content = new StringContent("");
            HttpResponseMessage responseBody = await client.PutAsync("https://www.wigilabs.com/", content);
            return responseBody;
        }

        static async Task<HttpResponseMessage> PostRequest()
        {
            NetworkRequest client = new NetworkRequest();
            HttpContent content = new StringContent("");
            HttpResponseMessage responseBody = await client.PostAsync("https://www.wigilabs.com/", content);
            return responseBody;
        }

        static async Task<HttpResponseMessage> DeleteRequest()
        {
            NetworkRequest client = new NetworkRequest();
            HttpResponseMessage responseBody = await client.DeleteAsync("https://www.wigilabs.com/");
            return responseBody;
        }

    }
}
