using System;

namespace VisitRoskilde.ImageHandelingModule
{
    class ImageComparison
    {
        private static ImageComparison obj;

        private ImageComparison()
        {
            
        }

        public static ImageComparison GetInstance()
        {
            if (obj == null)
            {
                obj = new ImageComparison();
            }
            return obj;
        }


        public void Compare()
        {
            throw new NotImplementedException();
        }
    }
}
