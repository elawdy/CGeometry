using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGeometry
{
    public class CGenerator
    {

        public static List<Point2D> Points(int count, int XMin, int XMax, int YMin, int YMax)
        {
            var r = new Random();
            List<Point2D> result = new List<Point2D>();

            for (int i = 0; i < count; i++)
            {
                double x = r.Next(XMin, XMax);
                double y = r.Next(YMin, YMax);
                result.Add(new Point2D(x, y));
            }

            return result;

        }




    }
}
