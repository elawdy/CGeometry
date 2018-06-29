using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CGeometry.SweapLine
{
    public class ClosestPair
    {
        public List<Point2D> Points { get; set; }
        public List<Point2D> Lx { get; set; }
        public List<Point2D> Ly { get; set; }
        public double Distance { get; set; }
        public Point2D CurrentPoint { get; set; }
        public List<Point2D> Result { get; set; }
        private event EventHandler step;



        public event EventHandler Step { remove { step -= value; } add { step += value; } }
        public ClosestPair(List<Point2D> points)
        {
            // to handle if the user didn't wire any external function
            Step += OnStep;
            //initialization
            Points = points;
            Lx = points.OrderBy(e => e.X).ToList();
            Ly = points.OrderBy(e => e.Y).ToList();
            Result = new List<Point2D>()
            {
                new Point2D(0,0),
                 new Point2D(0,0)
            };
            Distance = double.PositiveInfinity;


        }
        public void Run()
        {
            for (int i = 1; i < Lx.Count; i++)
            {
                //get d;
                CurrentPoint = Lx[i];
                Ly = Lx.Take(i).ToList();
                Ly = Ly.OrderBy(e => e.Y).ToList();
                // width of Active Window
                for (int j = 0; j < Ly.Count; j++)
                {
                    if (Lx[i].X - Ly[j].X > Distance)
                    {
                        Ly.RemoveAt(j);
                    }

                }
                // height of Active Index
                var upper = CurrentPoint.Y + Distance;
                var lower = CurrentPoint.Y - Distance;
                for (int k = 0; k < Ly.Count; k++)
                {
                    if (Ly[k].Y > upper || Ly[k].Y < lower)
                    {
                        Ly.RemoveAt(k);
                    }

                }

                //get the new distance
                for (int c = 0; c < Ly.Count; c++)
                {
                    double tempDist = Math.Sqrt(
                        Math.Pow(Ly[c].X - Lx[i].X, 2) +
                        Math.Pow(Ly[c].Y - Lx[i].Y, 2)
                        );
                    
                    if (tempDist < Distance)
                    {

                        Distance = tempDist;
                        Result[0] = Lx[i];
                        Result[1] = Ly[c];
                        //FIRE THE STEP EVENT
                        step(this, new EventArgs());
                    }
                    
                }
            }

        }


        public void OnStep(object sender, EventArgs e)
        {


        }

    }
}
