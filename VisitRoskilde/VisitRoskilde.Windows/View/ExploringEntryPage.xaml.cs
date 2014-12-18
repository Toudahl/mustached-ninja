using System;
using System.Collections.Generic;
using Windows.Devices.Geolocation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Bing.Maps;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using VisitRoskilde.FacebookIntegrationModule;

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
                userLocationPushPin.Background = new SolidColorBrush(Colors.Red);
                roskildeStationLocationPushPin.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
                BingMap.SetView(roskildeStationLocation, 14.0f);
                MapLayer.SetPosition(roskildeStationLocationPushPin, roskildeStationLocation);
                roskildeStationLocationPushPin.Text = "Roskilde Station";
                roskildeStationLocationPushPin.Background = new SolidColorBrush(Colors.Red);
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
            if (selectedLocationCategory.Text == null || selectedLocationCategory.Text == "")
            {
                selectedLocationCategory.Visibility = Visibility.Collapsed;
            }
            else
            {
                selectedLocationCategory.Visibility = Visibility.Visible;
            }

            if (selectedLocationStreet.Text == null || selectedLocationStreet.Text == "")
            {
                selectedLocationStreet.Visibility = Visibility.Collapsed;
            }
            else
            {
                selectedLocationStreet.Visibility = Visibility.Visible;
            }
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

        private void SelectAllLocations_OnClick(object sender, RoutedEventArgs e)
        {
            int counter1 = 0;
            selectMultipleLocations.IsChecked = true;
            LocationsListView.SelectedIndex = 0;
            //TODO: Make a count() in the viewmodel and bind that to a textblock and get that to count
            while (counter1 < 30)
            {
                try
                {
                    LocationsListView.SelectedIndex++;
                }
                catch (Exception)
                {
                }
                Location pushAllLocation = new Location(Convert.ToDouble(selectedLocationLatitude.Text), Convert.ToDouble(selectedLocationLongtitude.Text));
                AddPushpin(selectedLocationName.Text, pushAllLocation);
                counter1++;
            }
            selectMultipleLocations.IsChecked = false;
        }
    }
}
