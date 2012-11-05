using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

                
                var container = !localSettings.Containers.ContainsKey(ContainerKey)
                                                         ? localSettings.CreateContainer(ContainerKey, ApplicationDataCreateDisposition.Always)
                                                         : localSettings.Containers[ContainerKey];

                if(!container.Values.ContainsKey(SettingsKey))
                {
                    _current = new InstmasSettings();
                    var settings = JsonConvert.SerializeObject(_current);
                    container.Values[SettingsKey] = settings;
                }
                else
                {
                    _current = JsonConvert.DeserializeObject<InstmasSettings>(container.Values[SettingsKey] as string);
                    _current.CalendarWindows = new ObservableCollection<CalendarWindow>(_current.CalendarWindowsSerializeable);
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

        
        [XmlIgnore]
        public ObservableCollection<CalendarWindow> CalendarWindows { get; set; }

        public List<CalendarWindow> CalendarWindowsSerializeable { get { return CalendarWindows.ToList(); } }

        public void Save()
        {
            var cals = CalendarWindows;
            var settings = JsonConvert.SerializeObject(this);
            var existingSettings = JsonConvert.DeserializeObject<InstmasSettings>(ApplicationData.Current.LocalSettings.Containers[ContainerKey].Values[SettingsKey] as string);
            ApplicationData.Current.LocalSettings.Containers[ContainerKey].Values[SettingsKey] = settings;
        }
    }
}
