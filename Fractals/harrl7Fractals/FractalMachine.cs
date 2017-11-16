using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace harrl7Fractals
{
    class FractalMachine
    {
        private enum eTriCorners { LEFT, TOP, RIGHT };

        public const int LEFT = -1;
        public const int RIGHT = 1;

        private const int TREE_SIZE = 5;
        public int SNOWFLAKE_SIZE = 100;

        Random rng;

        Graphics g;
        Brush brush;
        Pen pen;

        public FractalMachine(Random rng, Graphics g)
        {
            this.rng = rng;
            this.g = g;

            brush = new SolidBrush(Color.Red);
            pen = new Pen(Color.Red);
        }

        public void Clear()
        {
            g.Clear(Color.Black);
        }


        /*
         * == Triangle ==
         * Take 3 points that mark a triangle
         * Split the triangle into 4 equall triangles
         * For each of the 3 outer triangles
         * use it's corners as the 3 starting points for the next generation
         */
        public void Triangle(int depth, Point[] corners)
        {
            if(depth.Equals(1))
            {
                g.FillPolygon(brush, corners); // Draw triangle
            }   
            else
            {

                // Grid with 4 x increment, and 2 y increment
                // For plotting corners of triangles
                int[] x = new int[5];
                int[] y = new int[3];

                for (int i=0; i<x.Length; i++) x[i] = (int)(corners[(int)eTriCorners.LEFT].X + ((corners[(int)eTriCorners.RIGHT].X-corners[(int)eTriCorners.LEFT].X) * i*0.25));
                for (int i=0; i<y.Length; i++) y[i] = (int)(corners[(int)eTriCorners.TOP].Y + ((corners[(int)eTriCorners.RIGHT].Y - corners[(int)eTriCorners.TOP].Y) * i * 0.5));

                // Corners
                Point left;
                Point top;
                Point right;

                // Left triangle
                left = new Point(x[0], y[2]);
                top = new Point(x[1], y[1]);
                right = new Point(x[2], y[2]);

                Point[] cornersLeft = { left, top, right };
                Triangle(depth-1, cornersLeft);


                // Top triangle
                left = new Point(x[1], y[1]);
                top = new Point(x[2], y[0]);
                right = new Point(x[3], y[1]);

                Point[] cornersTop = { left, top, right };
                Triangle(depth-1, cornersTop);


                // Right triangle
                left = new Point(x[2], y[2]);
                top = new Point(x[3], y[1]);
                right = new Point(x[4], y[2]);

                Point[] cornersRight = { left, top, right };
                Triangle(depth-1, cornersRight);
            }     
            
        }


        /*
         * == Tree ==
         * For each level of depth take a point and an angle and draw a line
         * Use the end of the line as the starting point to draw 2 more line at vertically mirrored angles
         * Each generation decrease the length of the lines
         */ 
        public void Tree(int depth, Point p1, double angle)
        {
            if(depth.Equals(1)) // Base case
            {
                // Draw leaves as green
                pen.Color = Color.Green;

                int x = (int)(Math.Cos(angle) * (depth * TREE_SIZE));
                int y = (int)(Math.Sin(angle) * (depth * TREE_SIZE));
                Point p2 = new Point(p1.X + x, p1.Y - y);

                g.DrawLine(pen, p1, p2); // Draw
            }
            else
            {
                // Draw more branches
                pen.Color = Color.Brown;

                int x = (int) (Math.Cos(angle) * (depth*TREE_SIZE));
                int y = (int) (Math.Sin(angle) * (depth*TREE_SIZE));
                Point p2 = new Point(p1.X+x, p1.Y-y);

                g.DrawLine(pen, p1, p2); // Draw

                // Recur
                p1 = p2;

                Tree(depth-1, p1, angle + 0.1*Math.PI);
                Tree(depth-1, p1, angle - 0.1 * Math.PI);
            }
        }


        /*
         * == Dragon Curve ==
         * At the first level of depth mark of line to the left
         * For each subsequent level of depth: append a left hand bend, then repeat the preceding sequence of turns with the middle turn reversed 
         */
        public void DragonCurve(int depth, string turns, Point start, int scale)
        {
            if(depth.Equals(1)) // Draw
            {
                double dir = 0;
                Point p1 = start;
                char[] turnArray = turns.ToCharArray();

                double alpha = 255;

                // Draw each line segment
                foreach (char turn in turnArray)
                {
                    // Calculate cordinates of line segment
                    int x = (int)(Math.Cos(dir) * scale);
                    int y = (int)(Math.Sin(dir) * scale);
                    Point p2 = new Point(p1.X+x, p1.Y-y);

                    // Shift color of line
                    Color col = Color.FromArgb((int)alpha, 255, 111, 111);
                    pen.Color = col;
                    g.DrawLine(pen, p1, p2);
                    alpha = alpha * 0.99999;

                    // Set angle of next line segment
                    p1 = p2;
                    if (turn.Equals('0')) dir -= 0.5 * Math.PI;
                    else dir += 0.5 * Math.PI;

                } 
            }
            else
            {
                string appendedTurns;
                char[] turnArray = turns.ToCharArray();
                int midIndex = (turnArray.Length-1) / 2;

                // Flip middle char
                if (turnArray[midIndex].Equals('1')) turnArray[midIndex] = '0';
                else turnArray[midIndex] = '1';

                new string(turnArray);

                // Appends 1
                // Append turns with middle item flipped
                DragonCurve(--depth, turns + "1" + new string(turnArray), start, scale);

            }
        }


        /*
        * == Koch snowflake ==
        * On the first level of depth draw a triangle
        * for each subsequent level of depth: From the center ⅓ of each edge extend an equilateral triangle
        */
        public void Snowflake(int depth, string turns, Point p, double scale)
        {
            if (depth.Equals(1)) // Draw
            {
                // Default angle
                double dir = 0;
                char[] turnArray = turns.ToCharArray();


                // Draw each line segment
                foreach (char turn in turnArray)
                {
                    
                    if (turn.Equals('-')) dir -= 1/3f * Math.PI;        // Turn left
                    else if (turn.Equals('+')) dir += 1/3f * Math.PI;  // Turn Right
                    else if(turn.Equals('F'))               // Draw line
                    {
                        // Calculate cordinates of line segment
                        int x = (int)(Math.Cos(dir) * scale);
                        int y = (int)(Math.Sin(dir) * scale);
                        Point p2 = new Point(p.X + x, p.Y - y);

                        // Shift color of line
                        Color col = Color.FromArgb(255, 111, 111);
                        pen.Color = col;
                        g.DrawLine(pen, p, p2);

                        // Set angle of next line segment
                        p = p2;
                    }
                }

            }
            else
            {

                // 'F' move forward
                // '-' turn left
                // '+' turn right

                string appendedTurns;
                char[] turnArray = turns.ToCharArray();
                Stack<int> indexStack = new Stack<int>();

                // Record the index of each 'F' in the array
                for (int i = 0; i < turnArray.Length; i++) if (turnArray[i].Equals('F')) indexStack.Push(i);

                // Replace each 'F' in string
                int initCount = indexStack.Count;
                for(int i=0; i<initCount; i++)
                {
                    int index = indexStack.Pop();

                    // Make new string
                    string start = turns.Substring(0, index);
                    string end = turns.Substring(index+1, turns.Length-(index+1));

                    turns = start + "F+F--F+F" + end;
                }

                // Recur
                Snowflake(--depth, turns, p, scale/3);
            }
        }


    }
}
