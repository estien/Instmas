using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InstmasWin8App.Common;
using InstmasWin8App.DataModel;
using InstmasWin8App.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace InstmasWin8App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DayPage : LayoutAwarePage
    {
        private HttpClient _httpClient;


        public DayPage()
        {
            this.InitializeComponent();
        }

        protected override void LoadState(object navigationParameter, Dictionary<string, object> pageState)
        {
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DefaultViewModel["CalendarWindow"] = new CalendarWindow{ DayNumber = (int) e.Parameter };
            Helpers.CreateHttpClient(ref _httpClient);
            base.OnNavigatedTo(e);
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


                var result = await response.Content.ReadAsStringAsync();



                var messageDialog = new MessageDialog(result, "Svar mottatt");

                // Add commands and set their command ids
                messageDialog.Commands.Add(new UICommand("Ok", null, 0));

                // Set the command that will be invoked by default
                messageDialog.DefaultCommandIndex = 1;

                // Show the message dialog and get the event that was invoked via the async operator
                var commandChosen = await messageDialog.ShowAsync();


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
    }

}
