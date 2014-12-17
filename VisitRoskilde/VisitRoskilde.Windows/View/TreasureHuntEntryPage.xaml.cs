using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VisitRoskilde.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TreasureHuntEntryPage : Page
    {

        public TreasureHuntEntryPage()
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
            //TreasureHuntEntryPageViewModel.SelectedTopic = (HuntTopicModel) ((Button) sender).Content;
            this.Frame.Navigate(typeof(TreasureHuntQuestionsPage));
        }
    }
}
