using CGeometry.WPF.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CGeometry;
using CGeometry.SweapLine;
using MathNet.Spatial.Euclidean;
using System.Threading;

namespace CGeometry.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GeometryEngine GeometryEngine { get; set; }
        public List<Point2D> Points { get; set; }
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
            //var lines = points.ToLines(GeometryEngine.GCanvas);
            GeometryEngine.Shapes["Points"].AddRange(circles);
           
            //GeometryEngine.Shapes["Lines"].AddRange(lines);

            GeometryEngine.Render("Points");
            //GeometryEngine.Render("Lines");

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
    }
}
