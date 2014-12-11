using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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

        
        public EntryPage()
        {
            this.InitializeComponent();
            double screenHeight = Window.Current.Bounds.Height;
            double screenWidth = Window.Current.Bounds.Width;
         

            double hypotenuse = Math.Sqrt(Math.Pow(screenHeight, 2) + Math.Pow(screenWidth, 2));

            Canvas testCanvas = new Canvas();
            testCanvas.Width = Math.Round(hypotenuse);

            #region Top triangle
            topTriangle = new Polygon();
            Point topTrianglePoint1 = new Point(0, 0); // Top left
            Point topTrianglePoint2 = new Point(screenWidth, 0); // Top right
            Point topTrianglePoint3 = new Point(0, screenHeight);


            topTriangle.Points.Add(topTrianglePoint1);
            topTriangle.Points.Add(topTrianglePoint2);
            topTriangle.Points.Add(topTrianglePoint3);

            topTriangle.Fill = new SolidColorBrush(Colors.DarkRed);

            topTriangle.PointerEntered += TopTriangleOnPointerEntered;
            topTriangle.PointerExited += TopTriangleOnPointerExited;

            grid_main.Children.Add(topTriangle);
            #endregion




            #region Bottom triangle
            bottomTriangle = new Polygon();
            Point bottomTrianglePoint1 = new Point(screenWidth, screenHeight); // Bottom right
            Point bottomTrianglePoint2 = new Point(screenWidth, 0); // Top right
            Point bottomTrianglePoint3 = new Point(0, screenHeight);


            bottomTriangle.Points.Add(bottomTrianglePoint1);
            bottomTriangle.Points.Add(bottomTrianglePoint2);
            bottomTriangle.Points.Add(bottomTrianglePoint3);


            bottomTriangle.Fill = new SolidColorBrush(Colors.DarkGreen);

            bottomTriangle.PointerEntered += BottomTriangleOnPointerEntered;
            bottomTriangle.PointerExited += BottomTriangleOnPointerExited;

            grid_main.Children.Add(bottomTriangle);
            

            #endregion


        }

        private void TopTriangleOnPointerExited(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            topTriangle.Fill = new SolidColorBrush(Colors.DarkRed);
        }

        private void TopTriangleOnPointerEntered(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            topTriangle.Fill = new SolidColorBrush(Colors.Red);
        }

        private void BottomTriangleOnPointerExited(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            bottomTriangle.Fill = new SolidColorBrush(Colors.DarkGreen);
        }

        private void BottomTriangleOnPointerEntered(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            bottomTriangle.Fill = new SolidColorBrush(Colors.Green);
        }
    }
}
