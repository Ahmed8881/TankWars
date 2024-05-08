using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GravityGame.UI
{
    public partial class MainMenu : Form
    {

       //  SoundPlayer Sound = new SoundPlayer(Properties.Resources.GameAudio);
        public MainMenu()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
         // Sound.Play();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Game gam = new Game();
            gam.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
