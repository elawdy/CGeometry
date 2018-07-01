using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using MathNet.Spatial.Euclidean;
using CGeometry.Interfaces;

namespace CGeometry.WPF.Graphics
{
    public class GLine : GShape, ILine
    {
        private double scale = 1;
        public Point2D startPoint;
        public Point2D endPoint;


        public Line Line { get; set; }
        public double Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        public Point2D StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }
        public Point2D EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }




        public GLine(GCanvas gCanvas, Point2D startPoint, Point2D endPoint) : base(gCanvas)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            Shape = Line = new Line();
            Line.X1 = StartPoint.X /*Scale*/;
            Line.Y1 = StartPoint.Y;
            //
            Line.X2 = EndPoint.X /*Scale*/;
            Line.Y2 = EndPoint.Y;
            //Add this Shape to Canvas
            Line.RenderTransform = new TransformGroup();
        }
        public override void SetScale(double value)
        {
            ((TransformGroup)Line.RenderTransform).Children.Add(
                new ScaleTransform(value, value, Line.X1, Line.Y1));
        }
        public override void SetTranslate(double valueX, double valueY)
        {

            ((TransformGroup)Line.RenderTransform).Children.Add(
                        new TranslateTransform(valueX, valueY));
        }
    }
}
