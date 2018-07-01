using CGeometry.WPF.Graphics;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using CGeometry.SweapLine;
using MathNet.Spatial.Euclidean;
using CGeometry.Interfaces;

namespace CGeometry.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GeometryEngine GeometryEngine { get; set; }
        public List<Point2D> Points { get; set; }
        public IEnumerable<ILine> GLines { get; set; }
        public MainWindow()
        {

            InitializeComponent();

            GeometryEngine = new GeometryEngine();
            GeometryEngine.GCanvas.Canvas = cnvs_drawing;
            GeometryEngine.Shapes.Add("ClosestPair", new List<GShape>());
        }

        private void btn_point_Click(object sender, RoutedEventArgs e)
        {
            Points = CGenerator.Points(10, 0, 600, 0, 400);
            var circles = Points.ToCircles(GeometryEngine.GCanvas);
            GeometryEngine.Shapes["Points"].AddRange(circles);
            GeometryEngine.Render("Points");

        }
        private void btn_Line_Click(object sender, RoutedEventArgs e)
        {
            Points = CGenerator.Points(10, 0, 600, 0, 400);
            GLines = Points.ToLines(GeometryEngine.GCanvas);
            GeometryEngine.Shapes["Lines"].AddRange((List<GLine>)GLines);
            GeometryEngine.Render("Lines");

        }

        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {


            GeometryEngine.RemoveAll();
        }

        private void btn_closestPair_Click(object sender, RoutedEventArgs e)
        {
            ClosestPair closestPair = new ClosestPair(Points);
            closestPair.Step += ClosestPair_Step;
            closestPair.Run();
        }

        private void ClosestPair_Step(object sender, EventArgs e)
        {
            var myClosestPair = (ClosestPair)sender;
       
            GeometryEngine.Remove("ClosestPair");
            var clostPair = myClosestPair.Result.ToCircles(GeometryEngine.GCanvas, Brushes.Green, 10);
            GeometryEngine.Shapes["ClosestPair"].AddRange(clostPair);
            GeometryEngine.Render("ClosestPair");
            GeometryEngine.GCanvas.Canvas.UpdateLayout();
        }

        private void btn_SegIntersect_Click(object sender, RoutedEventArgs e)
        {
           
           SegIntersect Segments = new SegIntersect(GLines);
           Segments.Run();
            var mySegments = (SegIntersect)sender;

            GeometryEngine.Remove("ClosestPair");
           var clostPair = mySegments.Intersected.ToCircles(GeometryEngine.GCanvas, Brushes.Green, 10);
           GeometryEngine.Shapes["ClosestPair"].AddRange(clostPair);
           GeometryEngine.Render("ClosestPair");

        }

    }
}
