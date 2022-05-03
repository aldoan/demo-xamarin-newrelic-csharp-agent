
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using NewRelic.logging;
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

        //void Handle_Launch(object sender, System.EventArgs e)
        //{
        //    ((Button)sender).Text = $"App Launch Reported";
        //    NewRelic.Agent.recordAppLaunch();
        //}

        void Send_Request_To_Track(object sender, System.EventArgs e)
        {
            ((Button)sender).Text = $"Request Sent";

            _ = HttpRequest();

        }

        static async Task HttpRequest()
        {
            HttpClient client = new HttpClient();
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                string responseBody = await client.GetStringAsync(new Uri("https://www.wigilabs.com/"));
                NewRelic.Agent.noticeHttpTransaction("https://www.wigilabs.com/", "GET", 200, (long)100, (long)500, (long)150, (long)800);

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

    }
}
