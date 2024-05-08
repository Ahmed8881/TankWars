using GravityGameLibrary.BL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GravityGameLibrary.BL;
using GravityGameLibrary.DLInterfaces;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace GravityGameLibrary.BL
{
    public class BulletMovement : IMovementDL
    {
        public Point Move(Point location)
        {

            return new Point(location.X + 10, location.Y);
        }
    }
    public class BulletMovementBack : IMovementDL
    {
        public Point Move(Point location)
        {

            return new Point(location.X - 10, location.Y);
        }
    }
}