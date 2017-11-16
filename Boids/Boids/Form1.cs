using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * The steering algorithm that controls the Boids calculates 3 velocity vectors derived from different sub-algorithms. 
 * Then sums these vectors with the current velocity to determine the true velocity of the Boid.
 *
 *  Separation: Steer away from other Boids that within are tight radius
 *  Cohesion: Match the average position of other Boids within your line of sight
 *  Alignment: Match the average velocity of other Boids within your line of sight
 *
 *  In addition to this I added some random noise to the final velocity to make the Boids move more naturally.
 *  
 */

namespace Boids
{
    public partial class Form1 : Form
    {

        Graphics graphics;
        Bitmap bitmap;


        Manager manager;


        public Form1()
        {
            InitializeComponent();          
        }


        // === Init ===
        private void Form1_Load(object sender, EventArgs e)
        {
            graphics = panel.CreateGraphics();
            bitmap = new Bitmap(panel.Width, panel.Height);
            Graphics bufferGraphics = Graphics.FromImage(bitmap);


            manager = new Manager(bufferGraphics, panel, new Random());
        }


        // === Update ===
        private void timer_Tick(object sender, EventArgs e)
        {
           
            manager.Update();

            graphics.DrawImage(bitmap, new Point(0,0));
        }
   
    }
}
