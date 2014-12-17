using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(chckbox_A2.IsChecked == true ? typeof (HuntFinalPage) : typeof (TreasureHuntFailPage));
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
