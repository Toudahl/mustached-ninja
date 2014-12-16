﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VisitRoskilde.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TreasureHuntSecondQuestionsPage : Page
    {
        public TreasureHuntSecondQuestionsPage()
        {
            this.InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            
            if (chckbox_A2.IsChecked == true) 
            {
                this.Frame.Navigate(typeof (HuntFinalPage));
            }
            else
            {                
                this.Frame.Navigate(typeof (TreasureHuntFailPage));

            }
            
        }

        private void btn_proceed1_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
              if ( chckbox_A1.IsChecked==true   && chckbox_A2.IsChecked==true && chckbox_A3.IsChecked==true || chckbox_A3.IsChecked==true && chckbox_A2.IsChecked==true || chckbox_A3.IsChecked==true && chckbox_A1.IsChecked==true || chckbox_A1.IsChecked==true && chckbox_A2.IsChecked==true)
            {
                var msg =new MessageDialog("You can only select one answer at a time!");
                msg.ShowAsync();
                btn_proceed1.Opacity = 30;
            }
            if (chckbox_A1.IsChecked==false && chckbox_A2.IsChecked==false&& chckbox_A3.IsChecked==false)
            {
                var msg2 = new MessageDialog("You must select one answer");
                msg2.ShowAsync();
            }
        }
    }
}
