using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GravityGame.UI
{
    public partial class Lost : Form
    {
        private Timer timer;
        public Lost()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            timer = new Timer();
            timer.Interval = 5000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void Timer_Tick(object sender, EventArgs e)
        {

            timer.Stop();
            Close();
            ShowMainMenu();
        }

        private void ShowMainMenu()
        {
            this.Close();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }
    }
}
