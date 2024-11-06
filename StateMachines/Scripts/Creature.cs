using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Runtime.Loader;
using Microsoft.VisualBasic.ApplicationServices;
using System.Reflection.Metadata.Ecma335;
using System.Transactions;
using System.Drawing.Printing;

namespace StateMachines.Scripts
{
    enum E_Gameanimations { LEFT, RIGHT, UP, DOWN, IDLE }
    internal class Creature
    {
        public E_Gameanimations E_anim;
        protected Vector2 StartPos;
        protected Vector2 CurrentPos;
        protected Texture2D Sprite;
        public Level currentLevel;
        public Rectangle frame;
        protected bool isJumping;
        protected int maxHeight;
        protected int currentHeight;
        protected int Speed;
        private int frameIndex;
        private double currentFrameTime;
        private double frameTimeLimit; 

        public void LoadContent(ContentManager cm, string name)
        {
            Sprite = cm.Load<Texture2D>(name);
        }
        
        public void Draw(SpriteBatch spritebatch) 
        {
            switch(E_anim)
            {
                case E_Gameanimations.LEFT:
                    spritebatch.Draw(Sprite, CurrentPos, new Rectangle(frame.X + frameIndex * frame.Width, frame.Y + frame.Height, frame.Width, frame.Height), Color.White);
                    break;
                case E_Gameanimations.RIGHT:
                    spritebatch.Draw(Sprite, CurrentPos, new Rectangle(frame.X + frameIndex * frame.Width, frame.Y + frame.Height * 2, frame.Width, frame.Height), Color.White);
                    break;
                case E_Gameanimations.UP:
                    spritebatch.Draw(Sprite, CurrentPos, new Rectangle(frame.X + frameIndex * frame.Width, frame.Y + frame.Height * 3, frame.Width, frame.Height), Color.White);
                    break;
                case E_Gameanimations.DOWN:
                    spritebatch.Draw(Sprite, CurrentPos, new Rectangle(frame.X + frameIndex * frame.Width, frame.Y + frame.X, frame.Width, frame.Height), Color.White);
                    break;
                case E_Gameanimations.IDLE:
                    spritebatch.Draw(Sprite, CurrentPos, frame, Color.White);
                    break;
                    default: break;
            }

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
            frameIndex = 0;
            currentFrameTime = 0.0f;
            frameTimeLimit = 0.05f;
            E_anim = E_Gameanimations.IDLE;
        }

        public void setAnimationState(E_Gameanimations state) 
        {
            E_anim = state;        
        }

        public void setCurrentFrame(double deltaTime)
        {
            currentFrameTime += deltaTime;
            if (currentFrameTime >= frameTimeLimit)
            {
                frameIndex++;
                if (frameIndex == 3)
                {
                    frameIndex = 0;
                }
                currentFrameTime = 0.0f; 
            }
        }

        public void JumpOrFall()
        {
            if (isJumping == true)
            {
                UP(Speed * 2);
                currentHeight += Speed*2;

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
            bool OnFloor = currentLevel.IsWall((int)CurrentPos.X, (int)CurrentPos.Y + (frame.Height - 1) + speedVal) ||
                           currentLevel.IsWall((int)CurrentPos.X + (frame.Width - 1), (int)CurrentPos.Y + (frame.Height - 1) + speedVal);

            bool OnPlatform = currentLevel.IsPlatform((int)CurrentPos.X, (int)CurrentPos.Y + (frame.Height - 1) + speedVal) ||
                              currentLevel.IsPlatform((int)CurrentPos.X + (frame.Width - 1), (int)CurrentPos.Y + (frame.Height - 1) + speedVal);

            bool SameRow = currentLevel.IsInSameRow((int)CurrentPos.Y + (frame.Height - 1), (int)CurrentPos.Y + (frame.Height - 1) + speedVal);

            if (!OnFloor && !(OnPlatform && !SameRow))
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

        public void SetisJumping()
        {
            bool OnFloor = currentLevel.IsWall((int)CurrentPos.X, (int)CurrentPos.Y + (frame.Height)) ||
                           currentLevel.IsWall((int)CurrentPos.X + (frame.Width - 1), (int)CurrentPos.Y + (frame.Height));

            bool OnPlatform = currentLevel.IsPlatform((int)CurrentPos.X, (int)CurrentPos.Y + (frame.Height)) ||
                              currentLevel.IsPlatform((int)CurrentPos.X + (frame.Width - 1), (int)CurrentPos.Y + (frame.Height));

            bool SameRow = currentLevel.IsInSameRow((int)CurrentPos.Y + (frame.Height - 1), (int)CurrentPos.Y + (frame.Height - 1) + 1);

            if (OnFloor || (OnPlatform && !SameRow))
            {
                isJumping = true;
                currentHeight = 0;
            }

        }

    }
}
