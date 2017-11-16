using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace harrl7Fractals
{
    public partial class Form1 : Form
    {
        FractalMachine fractal;

        // Depth limit
        const int TRIANGLE_MAX_DEPTH = 10;
        const int TREE_MAX_DEPTH = 20;
        const int DRAGON_MAX_DEPTH = 18;
        const int SNOWFLAKE_MAX_DEPTH = 7;


        public Form1()
        {
            InitializeComponent();

            fractal = new FractalMachine(new Random(), panel.CreateGraphics());     
        }

        // Triangle
        private void button1_Click(object sender, EventArgs e)
        {
            fractal.Clear();

            Point[] corners = { new Point(0, panel.Height), new Point(panel.Width/2, 0), new Point(panel.Width, panel.Height) };
            int depth = (int)numDepth.Value;

            if(depth <= TRIANGLE_MAX_DEPTH)
            {
                fractal.Triangle((int)numDepth.Value, corners);
            }
            else
            {
                MessageBox.Show("Max depth value for Triangle is " + TRIANGLE_MAX_DEPTH);
            }
            
        }


        // Tree
        private void button2_Click(object sender, EventArgs e)
        {
            fractal.Clear();


            Point p1 = new Point(panel.Width / 2, panel.Height);       
            int depth = (int)numDepth.Value;

            if (depth <= TREE_MAX_DEPTH)
            {
                fractal.Tree(depth, p1, Math.PI * 0.5);
            }
            else
            {
                MessageBox.Show("Max depth value for Tree is " + TREE_MAX_DEPTH);
            }      
        }


        // Dragon
        private void button3_Click(object sender, EventArgs e)
        {
            fractal.Clear();

            Point p1 = new Point(panel.Width / 2, panel.Height / 2);
            int scale = dragonScaler.Value;
            int depth = (int)numDepth.Value;

            if (depth <= DRAGON_MAX_DEPTH)
            {
                fractal.DragonCurve(depth, "110", p1, scale);
            }
            else
            {
                MessageBox.Show("Max depth value for Dragon is " + DRAGON_MAX_DEPTH);
            }
        }     


        // Snowflake
        private void btnSnowflake_Click(object sender, EventArgs e)
        {
            fractal.Clear();

            Point p1 = new Point((int) (panel.Width*0.1), (int) (panel.Height*0.25));
            int depth = (int)numDepth.Value;

            if (depth <= SNOWFLAKE_MAX_DEPTH)
            {
                fractal.Snowflake(depth, "F--F--F", p1, 900);
            }
            else
            {
                MessageBox.Show("Max depth value for Snowflake is " + SNOWFLAKE_MAX_DEPTH);
            }
        }

    }
}
