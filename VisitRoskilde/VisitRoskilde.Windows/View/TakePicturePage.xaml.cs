﻿using System;
using System.IO;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VisitRoskilde.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TakePicturePage : Page
    {
        public TakePicturePage()
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


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker OpenPicker = new FileOpenPicker();
            OpenPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            OpenPicker.ViewMode = PickerViewMode.Thumbnail;

            OpenPicker.FileTypeFilter.Clear();
            OpenPicker.FileTypeFilter.Add(".jpg");
            OpenPicker.FileTypeFilter.Add(".png");
            OpenPicker.FileTypeFilter.Add(".jpeg");

            Windows.Storage.StorageFile file = await OpenPicker.PickSingleFileAsync();

            if (file != null)
            {
                Windows.Storage.Streams.IRandomAccessStream filestream =
                    await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.SetSource(filestream);
                DisplayImage.Source = bitmapImage;

                this.DataContext = file;
                string w = Path.GetFileNameWithoutExtension(file.Path);
                txt_test.Text = w;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string d = Path.GetFileNameWithoutExtension(@"/Assets/Roskilde_Hall.jpg");


            txt_Result.Text = txt_test.Text == d ? "Found match "  : "no match";
            if (txt_test.Text == String.Empty)
            {
                txt_Result.Text = " select first";
            }
            if (txt_Result.Text=="Found match ")
            {
                                btn_Proceed.Visibility = Visibility.Visible;

            }
        }

        private void btn_Proceed_Click(object sender, RoutedEventArgs e)
        {
            

                this.Frame.Navigate(typeof (HuntTaskCompletePage));
            
        }
    }
}