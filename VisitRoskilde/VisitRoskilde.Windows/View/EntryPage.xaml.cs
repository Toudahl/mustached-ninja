using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
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

            BottomTriangle();
            TopTriangle();


        }

        private void BottomTriangle()
        {
            bottomTriangle = new Polygon();

            bottomTriangle.Points.Add(new Point(screenWidth, screenHeight));
            bottomTriangle.Points.Add(new Point(screenWidth, 0));
            bottomTriangle.Points.Add(new Point(0, screenHeight));

            bottomTriangle.Fill = new SolidColorBrush(Colors.DarkOrange);

            bottomTriangle.PointerEntered += thePointerEntered;
            bottomTriangle.PointerExited += thePointerExited;

            grid_main.Children.Add(bottomTriangle);

        }

        private void TopTriangle()
        {
            topTriangle = new Polygon();

            topTriangle.Points.Add(new Point(0, 0));
            topTriangle.Points.Add(new Point(screenWidth, 0));
            topTriangle.Points.Add(new Point(0, screenHeight));

            topTriangle.Fill = new SolidColorBrush(Colors.DarkOrange);

            topTriangle.PointerEntered += thePointerEntered;
            topTriangle.PointerExited += thePointerExited;
            grid_main.Children.Add(topTriangle);
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
            TopTriangle();
            BottomTriangle();
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

    }
}
