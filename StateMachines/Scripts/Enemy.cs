using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StateMachines.Scripts
{
    internal class Enemy : Creature
    {
        public Enemy(Vector2 pos) : base(pos) 
        {
              
        }

        public void Chase(Player player)
        {
            if (player.GetPos().X > CurrentPos.X)
            {
                RIGHT();
            }

            if (player.GetPos().X < CurrentPos.X)
            {
                LEFT();
            }

            if (player.GetPos().Y > CurrentPos.Y)
            {
                UP();
            }

            if (player.GetPos().Y > CurrentPos.Y)
            {
                DOWN();
            }
        }

        public void Caught(Player player, Enemy enemy)
        {
            if (player.GetPos() == enemy.GetPos())
            {
                return;
            }

            return;
        }
    } 
}
