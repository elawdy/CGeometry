using MathNet.Spatial.Euclidean;


namespace CGeometry.SweapLine
{
    public class Event
    {
        private double x;
        private bool isStart;
        public double X
        {
            get { return x; }
            set { x = value; }
        }
        public bool IsStart
        {
            get { return isStart; }
            set { isStart = value; }
        }



    }
}