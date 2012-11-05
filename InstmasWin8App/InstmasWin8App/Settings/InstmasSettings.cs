using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using InstmasWin8App.DataModel;
using Windows.Storage;
using Newtonsoft.Json;

namespace InstmasWin8App.Settings
{
    public class InstmasSettings
    {
        private const string SettingsKey = "Settings";
        private const string ContainerKey = "DefaultContainer";

        private static InstmasSettings _current;
        public static InstmasSettings Current 
        {
            get
            {
                if (_current != null)
                    return _current;

                var localSettings = ApplicationData.Current.LocalSettings;

                _current = new InstmasSettings();
                var settings = JsonConvert.SerializeObject(_current);
                var container = !localSettings.Containers.ContainsKey(ContainerKey)
                                                         ? localSettings.CreateContainer(ContainerKey, ApplicationDataCreateDisposition.Always)
                                                         : localSettings.Containers[ContainerKey];

                if(!container.Values.ContainsKey(SettingsKey))
                {
                    container.Values[SettingsKey] = settings;
                }
                else
                {
                    _current = JsonConvert.DeserializeObject<InstmasSettings>(container.Values[SettingsKey] as string);
                }
                
                return _current;
            }
        }

        
        private InstmasSettings()
        {
            CalendarWindows = new ObservableCollection<CalendarWindow>();
            for (var i = 1; i <= 24; i++)
            {
                CalendarWindows.Add(new CalendarWindow {DayNumber = i, WindowOpened = false, Picture = null});
            }
        }

        
        
        public ObservableCollection<CalendarWindow> CalendarWindows { get; set; }




        public void Save()
        {
            var settings = JsonConvert.SerializeObject(this);
            ApplicationData.Current.LocalSettings.Containers[ContainerKey].Values[SettingsKey] = settings;
        }
    }
}
