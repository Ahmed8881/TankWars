using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZInput;
using GravityGameLibrary.DLInterfaces;
namespace GravityGameLibrary
{
    public class KeyBoardMovement:IMovementDL
    {
        private int Speed;
        private System.Drawing.Point Boundary;
        private int OffSet = 70;

        public KeyBoardMovement(int speed, System.Drawing.Point boundary)
        {
            this.Speed = speed;
            this.Boundary = boundary;
        }
        public System.Drawing.Point Move(System.Drawing.Point location)
        {
            if ((location.Y + OffSet) <= Boundary.Y&&Keyboard.IsKeyPressed(Key.DownArrow))
            {
                location.Y += Speed;
            }

            else if (location.Y - 10 >= 0 && Keyboard.IsKeyPressed(Key.UpArrow))
            {
                location.Y -= Speed;
            }
            else if (location.X - 10 >= 0 && Keyboard.IsKeyPressed(Key.LeftArrow))
            {
                location.X -= Speed;
            }
            if ((location.X + OffSet) <= Boundary.X && Keyboard.IsKeyPressed(Key.RightArrow))
            {
                location.X+= Speed;
            }
            return location;
        }
    }
}
