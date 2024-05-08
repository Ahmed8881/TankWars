using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GravityGameLibrary.DLInterfaces;
namespace GravityGameLibrary
{
    public class VerticalMovement:IMovementDL
    {
        private int Speed;
        private Point Boundary;
        private VerticalDirection Direction;
        private int OffSet = 70;
        public VerticalMovement(int speed, Point boundary, VerticalDirection direction)
        {
            this.Speed = speed;
            this.Boundary = boundary;
            this.Direction = direction;
        }

        public Point Move(Point location)
        {
            if ((location.Y + OffSet) >= Boundary.Y)
            {
                Direction = VerticalDirection.Up;
            }

            else if (location.Y - OffSet <= 0)
            {
                Direction = VerticalDirection.Down;
            }
            if (Direction == VerticalDirection.Up)
            {
                location.Y -= Speed;
            }
            else if (Direction == VerticalDirection.Down)
            {
                location.Y += Speed;
            }
            return location;
        }
    }
}
