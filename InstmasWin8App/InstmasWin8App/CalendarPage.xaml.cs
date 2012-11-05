using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using InstmasWin8App.Common;
using InstmasWin8App.DataModel;
using InstmasWin8App.PictureService;
using InstmasWin8App.Settings;
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
        private InstmasSettings _settings;

        public CalendarPage()
        {
            _settings = InstmasSettings.Current;
            this.InitializeComponent();

            if(Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                DesignModeViewModel();
            }
        }

        private void DesignModeViewModel()
        {
            DefaultViewModel["CalendarWindows"] = new ObservableCollection<CalendarWindow>
                                           {
                                               new CalendarWindow {DayNumber = 1},
                                               new CalendarWindow {DayNumber = 2},
                                               new CalendarWindow {DayNumber = 3}
                                           };
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            var settings = InstmasSettings.Current;
            DefaultViewModel["CalendarWindows"] = settings.CalendarWindows;
        }
        
        private void DayClick(object sender, ItemClickEventArgs e)
        {
            var calendarWindow = (CalendarWindow)(e.ClickedItem);
            Frame.Navigate(typeof(DayPage), calendarWindow.DayNumber);
        }
    }
}
