using System;

namespace VisitRoskilde.ImageHandelingModule
{
    class ImageHandeling
    {
        private ImageCapture capture;
        private ImageComparison compare;

        public ImageHandeling()
        {
            capture = ImageCapture.GetInstance();
            compare = ImageComparison.GetInstance();
        }

        // Use ImageCapture Object to get an image.
        // Maybe have a field to contain the image?
        public void GetImage()
        {
            capture.SelectImage();
        }

        // Use ImageComparison Object to check if the image is the correct one. 
        public bool CompareImage()
        {
            compare.Compare();
            throw new NotImplementedException();
        }
    }
}
