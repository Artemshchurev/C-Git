using System;
using GeometryPainting;
namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;
        public Vector()
        {
        }
        public double GetLength()
        {
            return Geometry.GetLength(this);
        }
        public Vector Add(Vector vector)
        {
            return Geometry.Add(this, vector);
        }
        public Boolean Belongs(Segment segment)
        {
            return Geometry.IsVectorInSegment(this, segment);
        }
    }
    public static class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }
        public static double GetLength(Segment segment)
        {
            var vector = new Vector();
            vector.X = segment.End.X - segment.Begin.X;
            vector.Y = segment.End.Y - segment.Begin.Y;
            return GetLength(vector);
        }
        public static Boolean IsVectorInSegment(Vector vector, Segment segment)
        {
            //(x-x1)(y2-y1)-(y-y1)(x2-x1) = 0
            return Equals((vector.X - segment.Begin.X) * (segment.End.Y - segment.Begin.Y),
                          (vector.Y - segment.Begin.Y) * (segment.End.X - segment.Begin.X)) &&
                vector.X >= segment.Begin.X && vector.X <= segment.End.X &&
                      vector.Y >= segment.Begin.Y && vector.Y <= segment.End.Y;
        }
        public static Vector Add(Vector firstVector, Vector secondVector)
        {
            var vector = new Vector();
            vector.X = firstVector.X + secondVector.X;
            vector.Y = firstVector.Y + secondVector.Y;
            return vector;
        }
    }
    public class Segment
    {
        public Vector Begin;
        public Vector End;
        public Segment()
        {

        }
        public double GetLength()
        {
            return Geometry.GetLength(this);
        }
        public Boolean Contains(Vector vector)
        {
            return Geometry.IsVectorInSegment(vector, this);
        }
    }
}
