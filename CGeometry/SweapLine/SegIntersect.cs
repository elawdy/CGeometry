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
        public List<Line2D> Lx = new List<Line2D>();
        public List<Line2D> Ly = new List<Line2D>();
        public List<Event> Events = new List<Event>();
        public List<Line2D> ActiveSet = new List<Line2D>();
        public List<Point2D> Intersected = new List<Point2D>();
        private event EventHandler step;

        public event EventHandler Step { remove { step -= value; } add { step += value; } }

        public SegIntersect(IEnumerable<ILine> lines)
        {
            // to handle if the user didn't wire any external function
            Step += OnStep;
            //initialization
            foreach (var line in lines)
            {
                double sx = line.StartPoint.X;
                double sy = line.StartPoint.Y;
                double ex = line.EndPoint.X;
                double ey = line.EndPoint.Y;
                if (sx < ex)
                {
                    Lines.Add(new Line2D(new Point2D(sx, sy), new Point2D(ex, ey)));
                }
                else
                {
                    Lines.Add(new Line2D(new Point2D(ex, ey), new Point2D(sx, sy)));
                }
            }
            Lx = Lines.OrderBy(l => l.StartPoint.X).ToList();
            Ly = Lines.OrderBy(l => l.StartPoint.Y).ToList();


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



        public void Run()
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
                    (l.StartPoint.Y != current.StartPoint.Y && l.EndPoint.Y != current.EndPoint.Y)).
                    ToList();
                    if (Active.Count != 0)
                    {
                        List<Line2D> above = GetAbove(Active, current, e.X);
                        List<Line2D> below = GetBelow(Active, current, e.X);
                        //2-instersect them with active
                        foreach (var line in above)
                        {
                            if (IsRealLine(line))
                            {
                                Point2D? p1 = current.IntersectWith(line);
                                //every no paraller lines must be intersect (real intersection is to intersect through line(start:end)
                                if (IsRealIntersect(p1, current, line))
                                {
                                    Intersected.Add((Point2D)p1);
                                }
                            }

                        }
                        foreach (var line in below)
                        {
                            if (IsRealLine(line))
                            {
                                Point2D? p1 = current.IntersectWith(line);
                                //every no paraller lines must be intersect (real intersection is to intersect through line(start:end)
                                if (IsRealIntersect(p1, current, line))
                                {
                                    Intersected.Add((Point2D)p1);
                                }
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
                    if (Active.Count != 0)
                    {
                        Line2D above = GetLineAbove(Active, current, e.X);
                        Line2D below = GetLineBelow(Active, current, e.X);
                        //2-instersect them with each other
                        if (IsRealLine(above) && IsRealLine(below))
                        {
                            Point2D? p1 = above.IntersectWith(below);
                            if (IsRealIntersect(p1, above, below))
                            {
                                Intersected.Add((Point2D)p1);
                            }
                        }

                    }
                    //3-remove active
                    ActiveSet.Remove(current);

                }


            }
        }

        private bool IsRealPoint(Point2D? p)
        {
            if (p.Value.X == 0 && p.Value.Y == 0)
            {
                return false;
            }
            return true;
        }

        private bool IsRealLine(Line2D line)
        {
            return (IsRealPoint(line.StartPoint) && IsRealPoint(line.EndPoint));
        }
        private bool IsRealIntersect(Point2D? p, Line2D line1, Line2D line2)
        {
            bool check1 = p.Value.X >= line1.StartPoint.X && p.Value.X <= line1.EndPoint.X;
            bool check2 = p.Value.X >= line2.StartPoint.X && p.Value.X <= line2.EndPoint.X;
            return check1 && check2;

        }

        private double GetY(Line2D line, double x)
        {
            var m = (line.EndPoint.Y - line.StartPoint.Y) / (line.EndPoint.X - line.StartPoint.X);
            var b = line.StartPoint.Y - (m * line.StartPoint.X);
            return m * x + b;
        }
        private List<Line2D> GetAbove(List<Line2D> active, Line2D current, double eX)
        {
            List<Line2D> result = new List<Line2D>();
            double Current_Y = GetY(current, eX);
            foreach (var line in active)
            {
                double active_Y = GetY(line, eX);
                double New_min = Current_Y - active_Y;
                if (New_min >= 0)
                {
                    result.Add(line);
                }
            }
            return result;

        }
        private Line2D GetLineAbove(List<Line2D> active, Line2D current, double eX)
        {
            double min = double.MaxValue;
            Line2D result = new Line2D();
            double Current_Y = GetY(current, eX);
            foreach (var line in active)
            {
                double active_Y = GetY(line, eX);
                double New_min = Current_Y - active_Y;
                if (New_min <= min && New_min >= 0)
                {
                    min = New_min;
                    result = line;
                }
            }

            return result;
        }

        private List<Line2D> GetBelow(List<Line2D> active, Line2D current, double eX)
        {
            List<Line2D> result = new List<Line2D>();
            double Current_Y = GetY(current, eX);
            foreach (var line in active)
            {
                double active_Y = GetY(line, eX);
                double New_min = active_Y - Current_Y;
                if (New_min >= 0)
                {
                    result.Add(line);
                }
            }
            return result;

        }
        private Line2D GetLineBelow(List<Line2D> active, Line2D current, double eX)
        {
            double min = double.MaxValue;
            Line2D result = new Line2D();
            double Current_Y = GetY(current, eX);
            foreach (var line in active)
            {
                double active_Y = GetY(line, eX);
                double New_min = active_Y - Current_Y;
                if (New_min <= min && New_min >= 0)
                {
                    min = New_min;
                    result = line;
                }
            }

            return result;
        }


        private void OnStep(object sender, EventArgs e)
        {
        }

    }
}
