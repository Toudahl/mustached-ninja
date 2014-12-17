using System;

namespace VisitRoskilde.ImageHandelingModule
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
