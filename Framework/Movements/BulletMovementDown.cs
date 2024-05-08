using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using GravityGameLibrary.BL;
using GravityGameLibrary.DLInterfaces;

namespace GravityGameLibrary.BL
{
    public class BulletMovementDown : IMovementDL
    {
        public Point Move(Point location)
        {

            return new Point(location.X, location.Y + 10);
        }
    }

}