using GravityGameLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GravityGameLibrary.DLInterfaces;
namespace GravityGameLibrary
{
    public class ZigZagMovement : IMovementDL
    {
        private int speed;
        private Point boundry;
        private HorizontalDirection direction;
        private int count;
        private int offset = 90;
        public ZigZagMovement(int speed, Point boundry)
        {
            this.speed = speed;
            this.boundry = boundry;
            this.direction = HorizontalDirection.Right;
            count = 0;
        }

        public Point Move(Point currentLocation)
        {
            int diagonalSpeed = (int)(speed / Math.Sqrt(2)); 
            if (direction == HorizontalDirection.Right)
            {
                if (count < 5)
                {
                    currentLocation.X += diagonalSpeed; 
                    currentLocation.Y -= diagonalSpeed; 
                }
                else
                {
                    currentLocation.X += diagonalSpeed; 
                    currentLocation.Y += diagonalSpeed;
                }
            }
            else if (direction == HorizontalDirection.Left)
            {
                if (count < 5)
                {
                    currentLocation.X -= diagonalSpeed;
                    currentLocation.Y += diagonalSpeed; 
                }
                else
                {
                    currentLocation.X -= diagonalSpeed; 
                    currentLocation.Y -= diagonalSpeed; 
                }
            }
            if ((currentLocation.X + offset) >= boundry.X)
            {
                direction = HorizontalDirection.Left;
            }
            else if ((currentLocation.X - offset) <= 0)
            {
                direction = HorizontalDirection.Right;
            }
            if (count == 10)
            {
                count = 0;
            }
            count++;
            return currentLocation;
        }



    }
}
