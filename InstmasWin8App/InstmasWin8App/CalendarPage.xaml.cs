using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using InstmasWin8App.Common;
using InstmasWin8App.DataModel;
using InstmasWin8App.PictureService;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace InstmasWin8App
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class CalendarPage : LayoutAwarePage
    {
        private HttpClient _httpClient;

        public CalendarPage()
        {
            this.InitializeComponent();
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            var items = new List<Day>();
            for (int i = 1; i <= 24; i++)
            {
                items.Add(new Day {DayNumber = i});
            }
            this.DefaultViewModel["Items"] = items;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Helpers.CreateHttpClient(ref _httpClient);
            base.OnNavigatedTo(e);  
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Dispose();
            base.OnNavigatedFrom(e);
        }

        private void Dispose()
        {
            if (_httpClient != null)
            {
                _httpClient.Dispose();
                _httpClient = null;
            }
        }

        private void DayClick(object sender, ItemClickEventArgs e)
        {
            var itemId = ((Day)(e.ClickedItem)).DayNumber;
            Frame.Navigate(typeof(DayPage), itemId);
        }

        private async void GetPictures(object sender, RoutedEventArgs e)
        {
            try
            {
                // 'AddressField' is a disabled text box, so the value is considered trusted input. When enabling the
                // text box make sure to validate user input (e.g., by catching FormatException as shown in scenario 1).
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Api.GetAllPictures);

                // Do not buffer the response:
                HttpResponseMessage response = await _httpClient.SendAsync(request,
                    HttpCompletionOption.ResponseHeadersRead);

                NotificationTextBlock.Text = "Requesting pictures...";

                var result = await response.Content.ReadAsStringAsync();



                var messageDialog = new MessageDialog(result, "Svar mottatt");

                // Add commands and set their command ids
                messageDialog.Commands.Add(new UICommand("Ok", null, 0));

                // Set the command that will be invoked by default
                messageDialog.DefaultCommandIndex = 1;

                // Show the message dialog and get the event that was invoked via the async operator
                var commandChosen = await messageDialog.ShowAsync();


                var hello = "http://distilleryimage6.s3.amazonaws.com/a8256f8c25aa11e29fb41231380ffec7_6.jpg";

                NotificationTextBlock.Text = "Completed.";
            }
            catch (HttpRequestException hre)
            {
                //notify error
            }
            catch (TaskCanceledException)
            {
                // notify cancelled
            }
            finally
            {
                // tja
            }

        }
    }
}
