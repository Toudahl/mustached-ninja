using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace VisitRoskilde.ImageHandeling
{
    class ImageCapture
    {
        private static ImageCapture obj;

        private ImageCapture()
        {
            
        }

        public static ImageCapture GetInstance()
        {
            if (obj == null)
            {
                obj = new ImageCapture();
            }
            return obj;
        }

        public void SelectImage()
        {
            throw new NotImplementedException();
        }
    }
}
