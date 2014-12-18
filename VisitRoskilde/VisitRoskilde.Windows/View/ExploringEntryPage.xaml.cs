using System;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Bing.Maps;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VisitRoskilde.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExploringEntryPage : Page
    {
        public ExploringEntryPage()
        {
            this.InitializeComponent();
        }

        private void Appbutton_goToExploring_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ExploringEntryPage));
        }

        private void Appbutton_goToTreasureHunt_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TreasureHuntEntryPage));
        }

        private void Appbutton_entryPage_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EntryPage));
        }

        private async void FbLoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            FbLoginButton.Visibility = Visibility.Collapsed;
            LocationInfoStackPanel.Visibility = Visibility.Visible;
            Geolocator userLocator = new Geolocator();
            Location roskildeStationLocation = new Location();
            try
            {
                Geoposition pos = await userLocator.GetGeopositionAsync();
                Location location = new Location(pos.Coordinate.Latitude, pos.Coordinate.Longitude);
                BingMap.SetView(location, 15.0f);
                MapLayer.SetPosition(userLocationPushPin, location);
                userLocationPushPin.Text = "You are here!";
                roskildeStationLocationPushPin.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
                BingMap.SetView(roskildeStationLocation, 14.0f);
                MapLayer.SetPosition(roskildeStationLocationPushPin, roskildeStationLocation);
                roskildeStationLocationPushPin.Text = "Roskilde Station";
                userLocationPushPin.Visibility = Visibility.Collapsed;
                throw;
            }
            //Center map view on current location.            
            
            BingMap.Visibility = Visibility.Visible;
        }

        private void AddPushpin(string LocationName, Location pushLocator)
        {
            Pushpin pushpin = new Pushpin();
            pushpin.Text = LocationName;
            MapLayer.SetPosition(pushpin, pushLocator);
            BingMap.Children.Add(pushpin);
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectMultipleLocations.IsChecked == false)
            {
                while (BingMap.Children.Count > 1)
                {
                    BingMap.Children.RemoveAt(1);
                }
            }
            Location pushLocation = new Location(Convert.ToDouble(selectedLocationLatitude.Text), Convert.ToDouble(selectedLocationLongtitude.Text));
            AddPushpin(selectedLocationName.Text, pushLocation);
            BingMap.SetView(pushLocation, 15.0f, 200);
        }
    }
}
