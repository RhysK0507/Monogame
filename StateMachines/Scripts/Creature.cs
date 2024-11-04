using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Runtime.Loader;
using Microsoft.VisualBasic.ApplicationServices;
using System.Reflection.Metadata.Ecma335;
using System.Transactions;

namespace StateMachines.Scripts
{
    internal class Creature
    {
        protected Vector2 StartPos;
        protected Vector2 CurrentPos;
        protected Texture2D Sprite;
        public Level currentLevel;
        public Rectangle frame;
        protected bool isJumping;
        protected int maxHeight;
        protected int currentHeight;
        protected int Speed;

        public void LoadContent(ContentManager cm, string name)
        {
            Sprite = cm.Load<Texture2D>(name);
        }
        
        public void Draw(SpriteBatch spritebatch, Rectangle rect) 
        {
            spritebatch.Draw(Sprite, CurrentPos, rect, Color.White);
        }

        public Creature(Vector2 Pos, Rectangle rect, Level cLevel, int InputSpeed) 
        {
            frame = rect;
            currentLevel = cLevel;
            StartPos = Pos;
            CurrentPos = StartPos;
            isJumping = false;
            currentHeight = 0;
            maxHeight = 170;
            Speed = InputSpeed;
        }

        public void SetisJumping()
        {

        }

        public void JumpOrFall()
        {
            if (isJumping == true)
            {
                UP(Speed * 2);
                currentHeight += currentHeight + Speed;

                if (currentHeight >= maxHeight)
                {
                    isJumping = false;
                }
            } else
            {
                DOWN(Speed * 2);
            }

        }

        // Collision detection
        public virtual void UP(int speedVal)
        {
            if (!currentLevel.IsWall((int)CurrentPos.X, (int)CurrentPos.Y - speedVal) &&
                !currentLevel.IsWall((int)CurrentPos.X + frame.Width - speedVal, (int)CurrentPos.Y - speedVal))
            {
                CurrentPos.Y -= speedVal;
            }
            else if (isJumping)
            {
                isJumping = false;
            }
        }

        public virtual void DOWN(int speedVal)
        {
            if (!currentLevel.IsWall((int)CurrentPos.X, (int)CurrentPos.Y + frame.Height) &&
                !currentLevel.IsWall((int)CurrentPos.X + frame.Width - speedVal, (int)CurrentPos.Y + frame.Height))
            {
                CurrentPos.Y += speedVal;
            } 
        }


        public virtual void LEFT()
        {
            if (!currentLevel.IsWall((int)CurrentPos.X - Speed, (int)CurrentPos.Y) &&
                !currentLevel.IsWall((int)CurrentPos.X - Speed, (int)CurrentPos.Y + frame.Height - 1))
            {
                CurrentPos.X -= Speed;
            }
        }

        public virtual void RIGHT()
        {
            if (!currentLevel.IsWall((int)CurrentPos.X + frame.Width, (int)CurrentPos.Y + frame.Height - Speed) &&
                !currentLevel.IsWall((int)CurrentPos.X + frame.Width, (int)CurrentPos.Y))
                CurrentPos.X += Speed;
        }

        public void ResetPos() 
        {
            CurrentPos.X = StartPos.X;
            CurrentPos.Y = StartPos.Y;   
        }

        public Vector2 GetPos()
        {
            return CurrentPos;
        }

        private int GetSpriteWidth()
        {
            return frame.Width;
        }

        private int GetSpriteHeight()
        {
            return frame.Height;
        }

        public bool CollidesWith(Creature creature)
        {
            if (CurrentPos.X <= creature.CurrentPos.X + GetSpriteWidth() - 1
                && CurrentPos.X + GetSpriteWidth() - 1 >= creature.CurrentPos.X 
                && CurrentPos.Y <= creature.CurrentPos.Y + GetSpriteHeight() - 1 
                && CurrentPos.Y + GetSpriteHeight() - 1 >= creature.CurrentPos.Y)
            {
                return true;
            }
             else
            {
                return false;
            }
        }

        public bool ItemCollidesWith(Texture2D item)
        {
            if (CurrentPos.X <=  item.Bounds.X + GetSpriteWidth() - 1
                && CurrentPos.X + GetSpriteWidth() - 1 >= item.Bounds.X
                && CurrentPos.Y <= item.Bounds.Y + GetSpriteHeight() - 1
                && CurrentPos.Y + GetSpriteHeight() - 1 >= item.Bounds.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
