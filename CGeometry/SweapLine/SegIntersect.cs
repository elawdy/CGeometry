using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;
using CGeometry.Interfaces;

namespace CGeometry.SweapLine
{
    public class SegIntersect
    {
        public List<Line2D> Lines = new List<Line2D>();
        public List<Line2D> Lx  = new List<Line2D>();
        public List<Line2D> Ly  = new List<Line2D>();
        public List<Event> Events = new List<Event>();
        public List<Line2D> ActiveSet = new List<Line2D>();
        public List<Point2D> Intersected = new List<Point2D>();

        public SegIntersect(IEnumerable<ILine> lines)
        {
            foreach (var line in lines)
            {
                double sx = line.StartPoint.X;
                double sy = line.StartPoint.Y;
                double ex = line.EndPoint.X;
                double ey = line.EndPoint.Y;
                Lines.Add(new Line2D(new Point2D(sx, sy), new Point2D(ex, ey)));
            }
            Lx = Lines.OrderBy(l => l.StartPoint.X).ToList();
            Ly = Lines.OrderBy(l => l.StartPoint.Y).ToList();

            Line2D first =  Lx.FirstOrDefault();
            ActiveSet.Add(first);

            #region Create Events of the <line>List
            foreach (var line in Lx)
            {
                Event eStart = new Event { X = line.StartPoint.X, IsStart = true };
                Event eEnd = new Event { X = line.EndPoint.X, IsStart = false };
                Events.Add(eStart);
                Events.Add(eEnd);
            }
            Events = Events.OrderBy(e => e.X).ToList();
            #endregion


        }
        public  void Run()
        {
            foreach (var e in Events)
            {
                if (e.IsStart)
                {
                    Line2D current = Lx.FirstOrDefault(l => l.StartPoint.X == e.X);
                    //create Vl Sweep-line
                    //Line2D Sweepline = new Line2D(new Point2D(e.X, 0), new Point2D(e.X, 600));
                    //1-get above and below
                    List<Line2D> Active = Ly.Where(l =>
                    l.StartPoint.X <= e.X && 
                    l.EndPoint.X >= e.X &&
                    (l.StartPoint.Y!=current.StartPoint.Y && l.EndPoint.Y != current.EndPoint.Y) ).
                    ToList();
                    if (Active.Count !=0)
                    {
                        Line2D above = GetAbove(Active, e.X);
                        Line2D below = GetBelow(Active, e.X);
                        //2-instersect them with active
                        if (above != null)
                        {
                            Point2D? p1 = current.IntersectWith(above);
                            if (p1 != null)
                            {
                                Intersected.Add((Point2D)p1);
                            }
                        }
                        if (below != null)
                        {
                            Point2D? p2 = current.IntersectWith(below);
                            if (p2 != null)
                            {
                                Intersected.Add((Point2D)p2);
                            }

                        }

                    }

                    //3-insert active
                    ActiveSet.Add(current);

                }
                else
                {
                    Line2D current = Lx.FirstOrDefault(l => l.StartPoint.X == e.X);
                    //create Vl Sweep-line
                    //Line2D Sweepline = new Line2D(new Point2D(e.X, 0), new Point2D(e.X, 600));
                    //1-get above and below
                    List<Line2D> Active = Ly.Where(l => l.StartPoint.X <= e.X && l.StartPoint.X >= e.X).ToList();
                    if (Active.Count!=0)
                    {
                        Line2D above = GetAbove(Active, e.X);
                        Line2D below = GetBelow(Active, e.X);
                        //2-instersect them with each other
                        Point2D p1;
                        if (above != null && below != null)
                        {
                            p1 = (Point2D)above.IntersectWith(below);
                            Intersected.Add(p1);
                        }

                    }
                    //3-remove active
                    ActiveSet.Remove(current);

                }


            }
        }

        private  double GetY(Line2D line, double x)
        {   
            var m = (line.EndPoint.Y - line.StartPoint.Y) / (line.EndPoint.X - line.StartPoint.X);
            var b = line.StartPoint.Y - (m * line.StartPoint.X);
            return m * x + b;
        }
        private Line2D GetAbove(List<Line2D> active , double eX)
        {
            double min= double.MaxValue;
            Line2D result = new Line2D();
            foreach (var line in active)
            {
                double Current_Y = GetY(line, eX);
                if (Current_Y <= min)
                {
                    min = Current_Y;
                    result = line;
                }
            }
            return result;

        }
        private Line2D GetBelow(List<Line2D> active , double eX)
        {
            double max = double.MinValue;
            Line2D result = new Line2D();
            foreach (var line in active)
            {
                double Current_Y = GetY(line, eX);
                if (Current_Y >= max)
                {
                    max = Current_Y;
                    result = line;
                }
            }
            return result;
        }

    }
}
