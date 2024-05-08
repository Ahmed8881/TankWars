
using GravityGameLibrary.BL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EZInput;
using GravityGameLibrary.BL;
using GravityGameLibrary.CollisionDetection;
using GravityGameLibrary;
using GravityGameLibrary.DLInterfaces;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Media;

namespace GravityGameLibrary
{


public class Game
{
    private bool spaceKeyPressed = false;
    private Form FormReference;
    private List<GameObject> GameObjects;
    private Image img;
    private List<CollisionHandler> Collisions;
    private static Game GameInstance;
    private static PictureBox background = new PictureBox();
    private DateTime lastFireTime;
    private TimeSpan fireCooldown = TimeSpan.FromSeconds(0.5);
    private int maxBullets = 100;
    private PictureBox explosion = new PictureBox();
    private Timer explosionTimer = new Timer();
    private Timer enemyFireTimer;
    private TimeSpan enemyFireInterval = TimeSpan.FromSeconds(5);
        private Image enemyBulletImg;
    private TimeSpan explosionDuration = TimeSpan.FromSeconds(0.5);
    private SoundPlayer explision=new SoundPlayer();
    private SoundPlayer bullet=new SoundPlayer();


        public void addExplisionSounds(SoundPlayer sound)
        {
            explision = sound;
        }
        public void addBulletSounds(SoundPlayer sound)
        {
            bullet= sound;
        }
        public List<GameObject> GetGameObjectList()
        {
            return GameObjects;
        }
        private Game(Form form)
        {
            this.FormReference = form;
            FormReference.Controls.Add(background);
            GameObjects = new List<GameObject>();
            Collisions= new List<CollisionHandler>();
            form.KeyDown += Form_KeyDown;
            form.KeyUp += Form_KeyUp;
            lastFireTime = DateTime.MinValue;
            enemyFireTimer = new Timer();
            enemyFireTimer.Interval = (int)enemyFireInterval.TotalMilliseconds;
            enemyFireTimer.Tick += EnemyFireTimer_Tick;
            explosionTimer.Interval = (int)explosionDuration.TotalMilliseconds;
            explosionTimer.Tick += ExplosionTimer_Tick;
            enemyFireTimer.Start();
            explosion.Visible = false;
        }

        public void Restart()
        {
            GameObjects.Clear();
            background.Controls.Clear();
            FormReference.Controls.Clear();
            spaceKeyPressed = false;
            explosion.Visible = false;

        }
        public void setForm(Form form)
        {
            this.FormReference = form;
            form.KeyDown += Form_KeyDown;
            form.KeyUp += Form_KeyUp;
        }
    public void GetExplosionPic(Image img)
        {
            explosion.Image = img;
            explosion.Size = img.Size;
            explosion.BackColor = Color.Transparent;
            background.Controls.Add(explosion);
        }
    public static Game GetGameInstance(Form form)
    {
        if (GameInstance == null)
        {
            GameInstance = new Game(form);
        }

        return GameInstance;
    }

        public void addCollision(CollisionHandler collision)
        {
            Collisions.Add(collision);
        }
        public void addGameObject(Image img, int Left, int Top, IMovementDL controller, PlayerType name)
        {
            if (GameObjects.Count < 10)
            {
                GameObject gameobject = new GameObject(img, Left, Top, controller, name);
                background.Controls.Add(gameobject.Pb);
                gameobject.Pb.Visible = true;
                GameObjects.Add(gameobject);
            }

        }


        public void addBulletImage(Image img)
    {
        this.img = img;
    }
        public void addenemyBulletImage(Image img)
        {
            this.enemyBulletImg = img;
        }
        public void addBullet(Image imgs, int Left, int Top, IMovementDL controller, PlayerType name)
        {
            var fire = GameObjects.Where(ob => ob.name ==  PlayerType.playerBullet).ToList();
            if(fire.Count<3)
            {
             
            Image img = imgs;
            GameObject bullet = new GameObject(img, Left, Top, controller, name);
            background.Controls.Add(bullet.Pb);
            bullet.Pb.Visible = true;
            bullet.GetHealth().Visible=false;
            GameObjects.Add(bullet);
            }

        }

        public void GameBg(Image img)
    {
        background.Image = img;
        background.Dock = DockStyle.Fill;
        background.SizeMode = PictureBoxSizeMode.StretchImage;
        FormReference.Controls.Add(background);
        background.SendToBack();
    }

        public void update()
        {
            foreach (CollisionHandler collision in Collisions)
            {
                CollisionHandler collisionHandler = new CollisionHandler(collision.GetObj1(), collision.GetObj2(), collision.GetAction());
                collisionHandler.HandleCollision(this,GameObjects);

            }
            foreach (GameObject gameobject in GameObjects)
            {
           //     if (gameobject.name == PlayerType.Player || gameobject.name == PlayerType.Enemy)
           //     {
           //
            gameobject.HealthProgressBar.Value = Math.Max(0, gameobject.Health); 
           //     }
                gameobject.Update();
            }
        
            List<GameObject> removedBullets = GameObjects.Where(bullet => !background.ClientRectangle.Contains(bullet.Pb.Bounds) && !bullet.Pb.Visible).ToList();
            foreach (var bullet in removedBullets)
            {
                DestroyObject(bullet);
            }


            if (spaceKeyPressed && CanFire())
            {
                PlayerFire();
            }
            CheckHealth();
        }

        private void CheckHealth()
        {
            for (int i = GameObjects.Count - 1; i >= 0; i--)
            {
                GameObject obj = GameObjects[i];
                if (obj.Health <= 0 || obj.Pb.Location.X >FormReference.Width || obj.Pb.Location.Y > FormReference.Height)
                {

                    DestroyObject(obj);
                }
            }
        }


        private void DestroyObject(GameObject obj)
        {
            background.Controls.Remove(obj.Pb);
            GameObjects.Remove(obj);
        }
        private bool CanFire()
    {
        return DateTime.Now - lastFireTime >= fireCooldown && GameObjects.Count < maxBullets;
    }

    public void PlayerFire()
    {
        GameObject player = GameObjects.FirstOrDefault(obj => obj.name == PlayerType.Player);
        if (player != null)
        {
            addBullet(img, player.Pb.Left + 200, player.Pb.Top, new BulletMovement(), PlayerType.playerBullet);
                lastFireTime = DateTime.Now;
                if (bullet != null)
                {
                    bullet.Play();
                }
            }
    }

        private void Form_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Space)
        {
            spaceKeyPressed = false;
        }
    }
    private void Form_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Space)
        {
            spaceKeyPressed = true;
        }
    }
        internal void ShowExplosion(System.Drawing.Point position)
        {
            explosion.Location = new System.Drawing.Point(position.X - explosion.Width / 2, position.Y - explosion.Height / 2); // Center explosion at collision position
            explosion.Visible = true;
            explosionTimer.Start();
            if (explision != null)
            {
                explision.Play();
            }
        }

        private void ExplosionTimer_Tick(object sender, EventArgs e)
        {
            explosion.Visible = false;
            explosionTimer.Stop();
        }


    public List<string> GetGameObjectNames()
    {
        List<string> names = new List<string>();
        foreach (GameObject obj in GameObjects)
        {
            names.Add(obj.name.ToString());
        }
        return names;
    }

    private void EnemyFireTimer_Tick(object sender, EventArgs e)
    {
            List<GameObject> g = GameObjects.Where(obj => obj.name == PlayerType.Enemy).ToList();
            foreach (GameObject enemy in g)
        {
                addBullet(enemyBulletImg, enemy.Pb.Left - 50, enemy.Pb.Top, new BulletMovementBack(), PlayerType.enemyBullet);
                if (bullet != null)
                {
                    bullet.Play();
                }
            }
    }
}

}