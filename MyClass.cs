using System;
namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;
        public Vector(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
    public static class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
        }
        public static Vector Add(Vector firstVector, Vector secondVector)
        {
            return new Vector(firstVector.X + secondVector.X, firstVector.Y + secondVector.Y);
        }
    }
}
