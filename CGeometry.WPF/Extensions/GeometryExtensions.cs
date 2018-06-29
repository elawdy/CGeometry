using CGeometry.WPF.Graphics;
using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CGeometry.WPF
{
    public static class GeometryExtensions
    {
        public static List<GCircle> ToCircles(this List<Point2D> points, GCanvas gCanvas,Brush fill=null,double radius=5)
        {
            fill = fill ?? Brushes.Red;
         
            var result = new List<GCircle>();
            foreach (var p in points)
            {
                result.Add(new GCircle(gCanvas, new Point(p.X, p.Y), radius)
                {
                    Fill = fill,
                    Stroke = Brushes.Black

                });
            }
            return result;
        }
        public static List<GLine> ToLines(this List<Point2D> points, GCanvas gCanvas)
        {
            var result = new List<GLine>();
            for (int i = 0; i < points.Count - 1; i += 2)
            {
                if (i == points.Count - 1)
                {
                    return result;
                }

                result.Add(new GLine(gCanvas, new Point(points[i].X, points[i].Y), new Point(points[i + 1].X, points[i + 1].Y))
                {
                    Fill = Brushes.Red,
                    Stroke = Brushes.Red

                });



            }
            return result;

        }

    }
}
