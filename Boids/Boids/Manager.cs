using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Boids
{
    public class Manager
    {
        const int ARRAY_SIZE = 1000;
        const int SPAWN_COUNT = 180;

        Boid[] population;
        int count;

        // Graphics
        Graphics graphics;
        Pen pen;
        Pen penRepulsor;

        Panel panel;
        Random rng;

        // Movement
        public PointF repulsor { get; set; }
        int repulsorTick;
        const int REPULSOR_DURATION = 20;


        // === Constructor ===
        public Manager(Graphics graphics, Panel panel, Random rng)
        {
            this.graphics = graphics;
            this.panel = panel;
            this.rng = rng;

            pen = new Pen(Color.White);
            penRepulsor = new Pen(Color.White);

            resetRepulsor();

            population = new Boid[ARRAY_SIZE];
            int count = 0;          

            for(int i = 0; i < SPAWN_COUNT; i++)
            {
                Add();
            }          
        }


        // === Update ===
        public void Update()
        {
            graphics.Clear(Color.Black);

            // Repulsor
            repulsorTick++;
            if (repulsorTick > REPULSOR_DURATION) resetRepulsor();

            graphics.DrawEllipse(penRepulsor, repulsor.X, repulsor.Y, 20, 20);

            for (int i = 0; i < count; i++)
            {
                population[i].Update();
            }
        }


        // === Add new Boid to population ===
        public void Add()
        {
            population[count] = new Boid(this, rng, new Point(rng.Next(panel.Width), rng.Next(panel.Height)), graphics, pen);
            count++;
        }

        
        // === Get Items in neighborhood ===
        public List<Boid> FindNeighbors(Boid self)
        {
            List<Boid> neighbors = new List<Boid>();
            Rectangle neighborhood = self.GetNeighborhood();

            // Compare each item in population
            for (int other = 0; other < count; other++)
            {

                // Ignore self
                if(self != population[other])
                {

                    // Check if other is in neighboorhood                    
                    if (Geometry.PointFDistance(self.position, population[other].position) <  Boid.SIGHT_RANGE)
                    {
                        neighbors.Add(population[other]);
                    }
                }
            }                   
            

            return neighbors;
        }


        // === Get world size ===
        public Size WorldSize()
        {
            Size world = new Size();

            world.Width = panel.Width;
            world.Height = panel.Height;

            return world;
        }
    

        public void resetRepulsor()
        {
            repulsor = new PointF((float)rng.NextDouble() * panel.Width, (float)rng.NextDouble() * panel.Height);
            repulsorTick = 0;
        }


    }
}
