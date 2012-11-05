using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Instmas.Data.Models;
using InstmasWin8App.Common;
using InstmasWin8App.DataModel;
using InstmasWin8App.Services;
using InstmasWin8App.Settings;
using Windows.UI.Xaml.Controls;

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


        protected override async void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            DefaultViewModel[ViewModelKeys.CalenderWindows] = _settings.CalendarWindows;

            var pictures = await PictureService.Current.GetPicturesAsync();
            ReplaceRetrievedCalendarWindows(pictures);
        }

        private void ReplaceRetrievedCalendarWindows(IEnumerable<Picture> pictures)
        {
            var calendarWindows = _settings.CalendarWindows;
            var picturesList = pictures.ToList();
            var length = picturesList.Count;
            for (var i = 0; i < length; i++)
            {
                if (picturesList[i].IsNull) continue;
                var existing = calendarWindows[i];
                calendarWindows.RemoveAt(i);
                calendarWindows.Insert(i, new CalendarWindow { Picture = picturesList[i], DayNumber = i + 1, WindowOpened = existing.WindowOpened});
            }
            _settings.Save();
        }

        private void DayClick(object sender, ItemClickEventArgs e)
        {
            var calendarWindow = (CalendarWindow)(e.ClickedItem);
            Frame.Navigate(typeof(DayPage), calendarWindow.DayNumber);
        }


        private void DesignModeViewModel()
        {
            DefaultViewModel[ViewModelKeys.CalenderWindows] = new ObservableCollection<CalendarWindow>
                                           {
                                               new CalendarWindow {DayNumber = 1},
                                               new CalendarWindow {DayNumber = 2},
                                               new CalendarWindow {DayNumber = 3}
                                           };
        }
    }
}
