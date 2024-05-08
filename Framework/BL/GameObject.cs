using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GravityGameLibrary.DLInterfaces;
using GravityGameLibrary;
namespace GravityGameLibrary
{
    public class GameObject
    {
        internal IMovementDL MovementController;
        internal PictureBox Pb;
        internal PlayerType name;
        public int Health;
        public ProgressBar HealthProgressBar;

        public GameObject(Image img, int left, int top, IMovementDL controller, PlayerType name)
        {
            Pb = new PictureBox();
            Pb.SizeMode = PictureBoxSizeMode.StretchImage;
            Pb.BackColor = Color.Transparent;
            Pb.Image = img;
            Pb.Width = img.Width;
            Pb.Height = img.Height;
            this.name = name;
            Pb.Left = left;
            Pb.Top = top;
            MovementController = controller;
                int health = 100;
                Health = health;
                HealthProgressBar = new ProgressBar();
                HealthProgressBar.Minimum = 0;
                HealthProgressBar.Maximum = health;
                HealthProgressBar.Value = health;
                HealthProgressBar.Width =  50;
                HealthProgressBar.Height = 5; 
                HealthProgressBar.Top =  10; 
                HealthProgressBar.Left = 10; 
                HealthProgressBar.BringToFront();
                Pb.Controls.Add(HealthProgressBar);
        }

        public void Update()
        {
            Pb.Location = MovementController.Move(Pb.Location);
        }
        public ProgressBar GetHealth()
        {
            return HealthProgressBar;
        }
        public void UpdateHealth(int newHealth)
        {
            Health = newHealth;
            HealthProgressBar.Value = Math.Max(0, Health);
        }
    }
}
