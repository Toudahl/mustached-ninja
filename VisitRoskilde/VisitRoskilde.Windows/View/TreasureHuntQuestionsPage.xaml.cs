using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VisitRoskilde.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TreasureHuntQuestionsPage : Page
    {
        public TreasureHuntQuestionsPage()
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
            this.Frame.Navigate(typeof (TakePicturePage));
        }

       

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
              var msg = new MessageDialog("It is somwehere in Roskilde");
              msg.ShowAsync();
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
