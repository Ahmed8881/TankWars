using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GravityGameLibrary.DLInterfaces;

namespace GravityGameLibrary
{
    public class HorizontalMovement : IMovementDL
    {
        private int Speed;
        private Point Boundary;
        private HorizontalDirection Direction;
        private int OffSet=70;
        public HorizontalMovement(int speed, Point boundary, HorizontalDirection direction)
        {
            this.Speed = speed;
            this.Boundary = boundary;
            this.Direction = direction;
        }

        public Point Move(Point location)
        {
            if ((location.X + OffSet) >= Boundary.X)
            {
                Direction = HorizontalDirection.Left;
            }
            
else if (location.X-OffSet <= Boundary.X / 2)
            {
                Direction = HorizontalDirection.Right;
            }
            if (Direction == HorizontalDirection.Left)
            {
                location.X -= Speed;
            }
            else if(Direction == HorizontalDirection.Right)
            location.X += Speed;
            return location;
        }
    }
}
