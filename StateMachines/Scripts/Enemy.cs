using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

            if (player.GetPos().Y < CurrentPos.Y)
            {
                UP();
            }

            if (player.GetPos().Y > CurrentPos.Y)
            {
                DOWN();
            }
        }

        public bool Caught(Player player)
        {
            if (player.GetPos() == this.GetPos())
            {
                return true;
            }

            return false;
        }
    } 
}
