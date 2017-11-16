using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Boids
{
    public class Boid
    {
        // Movement
        const float SPEED = 14;        
        PointF velocity;

        // Steering 
        const int NOISE_INTENSITY = 1;
        const float COHESION_WEIGHT = 0.01f;
        const float AlIGNMENT_WEIGHT = 0.12f;

        public const float SIGHT_RANGE = 180;

        // Geometry
        const float SIZE = 12;
        const float ANGLE = (float)(Math.PI * 0.1);
        const int CORNERS = 4;

        // System
        Manager manager;
        Random rng;

        public PointF position { get; set; }
        Size world;

        // Graphics
        Graphics graphics;
        Pen pen;


        // === Constructor ===
        public Boid(Manager manager, Random rng, PointF position, Graphics graphics, Pen pen)
        {
            this.manager = manager;
            this.rng = rng;

            this.position = position;

            this.graphics = graphics;
            this.pen = pen;

            velocity = new PointF(0, 0);
        }

        
        // === Update ===
        public void Update()
        {
            Steer();
            Move();
            Draw();
        }


        // === Update direction ===
        void Steer()
        {
            Boid[] neighbors = manager.FindNeighbors(this).ToArray();

            // Velocity vectors
            PointF velAlignment = alignment(neighbors);
            PointF velCohesion = cohesion(neighbors);
            PointF velSeparation = separation(neighbors);
            PointF velNoise = noise();
            PointF velRepulsor = repulsor();


            // Add velocity vectors
            velocity.X = velocity.X + velAlignment.X + velCohesion.X + velSeparation.X + velNoise.X + velRepulsor.X;
            velocity.Y = velocity.Y + velAlignment.Y + velCohesion.Y + velSeparation.Y + velNoise.Y + velRepulsor.Y;

            // Limit speed          
            float speed = (float) Math.Sqrt(Math.Pow(velocity.X, 2) + Math.Pow(velocity.Y, 2));
            if(speed > SPEED)
            {
                // Set velocity vector to same direction with lower magnitude
                float a = (float) Math.Atan2(velocity.Y, velocity.X);

                velocity.X = (float)(Math.Cos(a) * SPEED);
                velocity.Y = (float)(Math.Sin(a) * SPEED);           
            }
        }


        // === Update position ===
        void Move()
        {          
            float x = position.X + velocity.X;
            float y = position.Y + velocity.Y;

            // Loop
            Size world = manager.WorldSize();
            
            if (x < 0 - SIZE)               x = world.Width + SIZE;
            if (x > world.Width + SIZE)     x = 0 - SIZE;
            if (y < 0 - SIZE)               y = world.Height + SIZE;
            if (y > world.Height + SIZE)    y = 0 - SIZE;

            position = new PointF(x, y); 
        }


        // === Draw Boid ===
        void Draw()
        {

            double direction = Math.Atan2(velocity.Y, velocity.X);

            // Calculate coordinates
            double x1 = Math.Cos(direction + ANGLE) * SIZE;
            double y1 = Math.Sin(direction + ANGLE) * SIZE;

            double x2 = Math.Cos(direction - ANGLE) * SIZE;
            double y2 = Math.Sin(direction - ANGLE) * SIZE;

            double xBack = Math.Cos(direction) * SIZE*0.7;
            double yBack = Math.Sin(direction) * SIZE*0.7;


            // Set PointFs
            PointF[] corners = new PointF[CORNERS];

            // Front
            corners[0] = position;

            // Back
            corners[2] = new PointF((float)(position.X - xBack), (float)(position.Y - yBack));

            // Sides
            corners[1] = new PointF((float)(position.X - x1), (float)(position.Y - y1));
            corners[3] = new PointF((float)(position.X - x2), (float)(position.Y - y2));


            // Convert velocity to value between 0 and 255
            float xAbs = 255f * ((SPEED + velocity.X) / (2f*SPEED));
            float yAbs = 255f * ((SPEED + velocity.Y) / (2f * SPEED));

            // Set color based on velocity
            pen.Color = Color.FromArgb((int)xAbs, (int)yAbs, 255);

            // Draw
            graphics.DrawPolygon(pen, corners);
        }


        // === Get Neighborhood area ===
        public Rectangle GetNeighborhood()
        {
            int x = (int)(position.X - SIGHT_RANGE);
            int y = (int)(position.Y - SIGHT_RANGE);

            int width = (int)SIGHT_RANGE * 2;
            int height = (int)SIGHT_RANGE * 2;

            Rectangle rect = new Rectangle(x, y, width, height);

            return rect;
        }



        // === Steering subfunctions ===

        // Boid try to match the velocity of neighbors
        PointF alignment(Boid[] neighbors)
        {
            PointF velAlignment = new PointF();

            // Ignore if no neighbors are found
            if (neighbors.Count() > 0)
            {
                // Sum velocity vector off all neighbors
                foreach (Boid item in neighbors)
                {
                    velAlignment.X += item.velocity.X;
                    velAlignment.Y += item.velocity.Y;
                }

                // Average the vector
                velAlignment.X /= neighbors.Count();
                velAlignment.Y /= neighbors.Count();
            }

            velAlignment.X = (int)(velAlignment.X * AlIGNMENT_WEIGHT);
            velAlignment.Y = (int)(velAlignment.Y * AlIGNMENT_WEIGHT);

            return velAlignment;
        }

        // Boids try to match the position of neighbors
        PointF cohesion(Boid[] neighbors)
        {
            PointF velCohesion = new PointF();

            
            if(neighbors.Count() > 0)
            {
                foreach (Boid item in neighbors)
                {
                    // Sum neighbors postion
                    velCohesion.X += item.position.X;
                    velCohesion.Y += item.position.Y;
                }

                // Divide by count to take average postion
                velCohesion.X /= neighbors.Count();
                velCohesion.Y /= neighbors.Count();

                // Cohesion velocity vector is difference between center of mass and current position
                velCohesion.X = (velCohesion.X - position.X);
                velCohesion.Y = (velCohesion.Y - position.Y);


                velCohesion.X = (velCohesion.X * COHESION_WEIGHT);
                velCohesion.Y = (velCohesion.Y * COHESION_WEIGHT);
            }

            return velCohesion;
        }

        // Boids don't get too close to neighbors
        PointF separation(Boid[] neighbors)
        {
            PointF velSeparate = new PointF();

            foreach (Boid item in neighbors)
            {
                // If other Boid is close to self
                if(Geometry.PointFDistance(position, item.position) < 0.5*SIZE)
                {
                    // Subtract difference in position from the velocity vector
                    velSeparate.X -= item.position.X - position.X;
                    velSeparate.Y -= item.position.Y - position.Y;
                }
              
            }

            return velSeparate;
        }

        // Random extra bit
        PointF noise()
        {
            PointF velNoise = new PointF();

            velNoise.X = (float)(rng.NextDouble() * (NOISE_INTENSITY * 2)) - NOISE_INTENSITY;
            velNoise.Y = (float)(rng.NextDouble() * (NOISE_INTENSITY * 2)) - NOISE_INTENSITY;

            return velNoise;
        }

        // Move away from the repulsor
        PointF repulsor()
        {
            PointF velRepulsor = new PointF();

            const float REPULSOR_RANGE = 300;
            const float REPULSOR_INTENSITY = 20;

            if (Geometry.PointFDistance(position, manager.repulsor) < REPULSOR_RANGE)
            {
                velRepulsor.X -= (REPULSOR_RANGE - Math.Abs(manager.repulsor.X - position.X)) * REPULSOR_INTENSITY;
                velRepulsor.Y -= (REPULSOR_RANGE - Math.Abs(manager.repulsor.Y - position.Y)) * REPULSOR_INTENSITY;
            }

            return velRepulsor;
        }

    }
}
