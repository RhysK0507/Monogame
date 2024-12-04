﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StateMachines.Scripts
{
    internal class Enemy : Creature
    {
        public Enemy(Vector2 pos, Rectangle rect, Level cLevel, int InputSpeed, Audio audio) : base(pos, rect, cLevel, InputSpeed, audio) 
        {
              
        }

        // Moves towards the player
        public void Chase(Player player)
        {
            if (player.GetPos().X > CurrentPos.X)
            {
                E_anim = E_Gameanimations.RIGHT;
                RIGHT(2, 0.1f);
            }

            if (player.GetPos().X < CurrentPos.X)
            {
                E_anim = E_Gameanimations.LEFT;
                LEFT(2, 0.1f);
            }

            if (player.GetPos().Y < CurrentPos.Y)
            {
                if (player.GetPos().X < CurrentPos.X)
                {
                    E_anim = E_Gameanimations.LEFT ;
                }
                else if (player.GetPos().X > CurrentPos.X)
                {
                    E_anim = E_Gameanimations.RIGHT;
                }
                else
                {
                    E_anim = E_Gameanimations.UP;
                }
                UP(Speed);
            }

            if (player.GetPos().Y > CurrentPos.Y)
            {
                if (player.GetPos().X > CurrentPos.X)
                {
                    E_anim = E_Gameanimations.RIGHT;
                }
                else if (player.GetPos().X < CurrentPos.X)
                {
                    E_anim = E_Gameanimations.LEFT;
                }
                else
                {
                    E_anim = E_Gameanimations.DOWN;
                }
                DOWN(Speed);
            }
        }

        public int GetSpriteHeight()
        {
            return frame.Height;
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
