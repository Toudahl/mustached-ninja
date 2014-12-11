using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class EntryPage : Page
    {
        public EntryPage()
        {
            this.InitializeComponent();
            //double screenHeight = Window.Current.Bounds.Height;
            //double screenWidth = Window.Current.Bounds.Width;


            //double diagonalPow2 = Math.Pow(screenHeight,2) + Math.Pow(screenWidth,2);



            //double hypotenuse = Math.Sqrt(diagonalPow2);

            //Canvas testCanvas = new Canvas();
            //testCanvas.Width = Math.Round(hypotenuse);
            //text_debug.Text = Math.Pow(Math.Sin(hypotenuse / screenHeight),-1).ToString();

            //Polygon testShape = new Polygon();
            //Point testPoint = new Point(0,0);
            //Point testPoint2 = new Point(0,screenWidth);
            //Point testPoint3 = new Point(screenHeight,screenWidth);


            //testShape.Points.Add(testPoint);
            //testShape.Points.Add(testPoint2);
            //testShape.Points.Add(testPoint3);

        }
    }
}
