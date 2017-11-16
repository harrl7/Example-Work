using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boids
{
    static class Geometry
    {
        public static float PointFDistance(PointF p1, PointF p2)
        {
            return (float) Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
        
    
        
    }
}
