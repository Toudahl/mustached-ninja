using System;
using System.Collections.Generic;
using System.Text;

namespace VisitRoskilde.ImageHandeling
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
