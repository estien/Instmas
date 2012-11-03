﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InstmasWin8App.Common;
using InstmasWin8App.DataModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        public CalendarPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            var items = new List<Day>();
            for (int i = 1; i <= 24; i++)
            {
                items.Add(new Day {DayNumber = i});
            }
            this.DefaultViewModel["Items"] = items;
        }

        private void DayClick(object sender, ItemClickEventArgs e)
        {
            var itemId = ((Day) (e.ClickedItem)).DayNumber;
            Frame.Navigate(typeof (DayPage), itemId);
        }
    }
}