using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StateMachines.Scripts
{
    internal class Enemy : Creature
    {
        public Enemy(Vector2 pos, Rectangle rect, Level cLevel, int InputSpeed) : base(pos, rect, cLevel, InputSpeed) 
        {
              
        }

        // Moves towards the player
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
                UP(Speed);
            }

            if (player.GetPos().Y > CurrentPos.Y)
            {
                DOWN(Speed);
            }
        }

        //public bool Caught(Player player)
        //{
        //    if (player.GetPos() == this.GetPos())
        //    {
        //        return true;
        //    }

        //    return false;
        //}
    } 
}
