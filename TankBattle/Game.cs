using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GravityGameLibrary;
using GravityGameLibrary.Enum;
using GravityGame.UI;
using GravityGameLibrary.CollisionDetection;
namespace GravityGame
{
    public partial class Game : Form
    {
        GravityGameLibrary.Game game;
        public Game()
        {
            InitializeComponent();
            this.Size=new Size(1240,950);
            this.DoubleBuffered = true;
            GameLoop.Enabled=true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            game = GravityGameLibrary.Game.GetGameInstance(this);
            game.setForm(this);
            game.GetExplosionPic(Properties.Resources.blast);
            game.GameBg(Properties.Resources.BGG);
            game.addGameObject(Properties.Resources.p_, 140, 120, new KeyBoardMovement(5, new Point(this.Width, this.Height)),PlayerType.Player);
            game.addGameObject(Properties.Resources._1, 1200, 40, new HorizontalMovement(5,new Point(1200,this.Height),HorizontalDirection.Left),PlayerType.Enemy);
            game.addGameObject(Properties.Resources._2, 1100, 340, new ZigZagMovement(5, new Point(this.Width, this.Height)), PlayerType.Enemy);
            game.addGameObject(Properties.Resources._3, 1100, 540, new VerticalMovement(5, new Point(this.Width, this.Height),VerticalDirection.Down), PlayerType.Enemy);
            game.addBulletImage(Properties.Resources.mplayer);
            game.addenemyBulletImage(Properties.Resources.emissile);
            CollisionHandler collisionDetection1 = new CollisionHandler(PlayerType.Player, PlayerType.Enemy, CollisionType.PlayerHitsEnemy);
            CollisionHandler collisionDetection2 = new CollisionHandler(PlayerType.playerBullet,PlayerType.Enemy, CollisionType.Shoot);
            CollisionHandler collisionDetection3 = new CollisionHandler(PlayerType.enemyBullet, PlayerType.Player, CollisionType.EnemyBulletHitsPlayer);
            CollisionHandler collisionDetection4 = new CollisionHandler(PlayerType.enemyBullet, PlayerType.playerBullet, CollisionType.Blast);
            game.addCollision(collisionDetection1);
            game.addCollision(collisionDetection2);
            game.addCollision(collisionDetection3);
            game.addCollision(collisionDetection4);
             game.addExplisionSounds(new System.Media.SoundPlayer(Properties.Resources.explosion));
            game.addBulletSounds(new System.Media.SoundPlayer(Properties.Resources.cannonball));
        }

        private void GameLoop_Tick(object sender, EventArgs e)
        {
            game.update();
           List<string> objects= game.GetGameObjectNames();
            if (!objects.Any(obj => obj == "Enemy"))
            {
                GameLoop.Enabled = false;
                game.Restart();
                this.Close();
                Win s = new Win();
                s.Show();
            }
            else if (!objects.Any(obj => obj == "Player"))
            {

                GameLoop.Enabled = false;
                game.Restart();
                this.Close();
                Lost s = new Lost();
                s.Show();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
