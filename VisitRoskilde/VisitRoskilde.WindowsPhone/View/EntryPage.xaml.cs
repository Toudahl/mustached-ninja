﻿using Windows.Foundation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556
using Windows.UI.Xaml.Shapes;

namespace VisitRoskilde.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EntryPage : Page
    {
        private Polygon topTriangle;
        private Polygon bottomTriangle;
        double screenHeight;
        double screenWidth;


        public EntryPage()
        {
            this.InitializeComponent();

            GetScreenSize();

            CoreWindow.GetForCurrentThread().SizeChanged += OnScreenSizeChanged;

            TreasureTriangle();
            ExploringTriangle();

        }

        private void TreasureTriangle()
        {

            bottomTriangle = new Polygon();

            bottomTriangle.Points.Add(new Point(screenWidth, screenHeight));
            bottomTriangle.Points.Add(new Point(screenWidth, 0));
            bottomTriangle.Points.Add(new Point(0, screenHeight));

            bottomTriangle.Fill = new SolidColorBrush(Colors.DarkOrange);

            bottomTriangle.PointerEntered += thePointerEntered;
            bottomTriangle.PointerExited += thePointerExited;
            bottomTriangle.PointerPressed += GoToTreasureHunting;

            grid_main.Children.Add(bottomTriangle);

        }

        private void GoToTreasureHunting(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            var pointer = PointerPoint.GetCurrentPoint(pointerRoutedEventArgs.Pointer.PointerId).Properties.IsLeftButtonPressed;
            if (pointer)
            {
                this.Frame.Navigate(typeof(TreasureHuntEntryPage));
            }
        }

        private void ExploringTriangle()
        {
            topTriangle = new Polygon();

            topTriangle.Points.Add(new Point(0, 0));
            topTriangle.Points.Add(new Point(screenWidth, 0));
            topTriangle.Points.Add(new Point(0, screenHeight));

            topTriangle.Fill = new SolidColorBrush(Colors.DarkOrange);

            topTriangle.PointerEntered += thePointerEntered;
            topTriangle.PointerExited += thePointerExited;
            topTriangle.PointerPressed += GoToExploring;

            grid_main.Children.Add(topTriangle);
        }

        private void GoToExploring(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            var pointer = PointerPoint.GetCurrentPoint(pointerRoutedEventArgs.Pointer.PointerId).Properties.IsLeftButtonPressed;
            if (pointer)
            {
                this.Frame.Navigate(typeof(ExploringEntryPage));
            }
        }

        private void GetScreenSize()
        {
            screenWidth = CoreWindow.GetForCurrentThread().Bounds.Width;
            screenHeight = CoreWindow.GetForCurrentThread().Bounds.Height;
        }

        private void OnScreenSizeChanged(CoreWindow sender, WindowSizeChangedEventArgs args)
        {
            GetScreenSize();
            grid_main.Children.Clear();
            ExploringTriangle();
            TreasureTriangle();
        }

        private void thePointerExited(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            var triangle = sender as Polygon;
            triangle.Fill = new SolidColorBrush(Colors.DarkOrange);
        }

        private void thePointerEntered(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            var triangle = sender as Polygon;
            triangle.Fill = new SolidColorBrush(Colors.Orange);
        }

        private void Appbutton_goToExploring_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ExploringEntryPage));
        }

        private void Appbutton_goToTreasureHunt_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TreasureHuntEntryPage));
        }


        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
