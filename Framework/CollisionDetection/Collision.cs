using GravityGameLibrary.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GravityGameLibrary.Enum;
using System.Windows.Forms;

namespace GravityGameLibrary.CollisionDetection
{
    public class CollisionHandler
    {
        private PlayerType Type1;
        private PlayerType Type2;
        private CollisionType Action;

        public CollisionHandler(PlayerType type1, PlayerType type2, CollisionType action)
        {
            this.Type1 = type1;
            this.Type2 = type2;
            this.Action = action;
        }
        public  void HandleCollision(Game game,List<GameObject> gameobjects)
        {
            foreach(GameObject g in gameobjects)
            {
                if (g.name == Type1) {
                    foreach(GameObject g2 in gameobjects)
                    {
                        if(g2.name== Type2)
                        {
                            if (g.Pb.Bounds.IntersectsWith(g2.Pb.Bounds))
                            {
                            HandleCollision(game, g,g2);
                            }

                        }
                    }
                }
            }
        }
        public PlayerType GetObj1()
        {
            return Type1;
        }
        public PlayerType GetObj2()
        {
            return Type2;
        }
        public CollisionType GetAction()
        {
            return Action;
        }
        private  void HandleCollision(Game game, GameObject obj1, GameObject obj2)
        {
            if (Action == CollisionType.PlayerHitsEnemy)
            {
                obj1.Health -= 10;
                obj2.Health -= 20;
                game.ShowExplosion(obj1.Pb.Location);
            }
            else if (Action == CollisionType.Shoot)
            {
                obj2.Health -= 20;
                obj1.Health = 0;
                game.ShowExplosion(obj1.Pb.Location);
            }
            else if (Action == CollisionType.EnemyBulletHitsPlayer)
            {
                obj1.Health = 0;
                obj2.Health -= 20;
                game.ShowExplosion(obj1.Pb.Location);
            }
            else if (Action == CollisionType.Blast)
            {
                obj1.Health = 0;
                obj2.Health = 0;
                game.ShowExplosion(obj1.Pb.Location);
            }
            }
    }
}
