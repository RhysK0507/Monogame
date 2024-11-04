using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using SharpDX.Direct3D9;


namespace StateMachines.Scripts
{
    internal class Player : Creature
    {
        private int CurrentLives;
        private int InitialLives;
        private int Score;

        public Player(Vector2 pos,int lives, Rectangle rect, Level cLevel, int InputSpeed) : base(pos, rect, cLevel, InputSpeed)
        {
            CurrentLives = lives;
            InitialLives = CurrentLives;
            Score = 0;
        }

        public int GetLives()
        {
            return CurrentLives; 
        }

        public void ReduceLives()
        {
            CurrentLives--;
        }

        public int GetScore()
        {
            return Score;
        }

        public void ResetLives()
        {
            if (CurrentLives == 0)
            {
                CurrentLives = InitialLives;
            }
        }

        public override void UP(int speedVal)
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

            if (currentLevel.IsPickUp((int)CurrentPos.X, (int)CurrentPos.Y - 1))
            {
                currentLevel.RemoveItem((int)CurrentPos.X, (int)CurrentPos.Y - 1);
                Score += 10;
            }
            else if (currentLevel.IsPickUp((int)CurrentPos.X + frame.Width - 1, (int)CurrentPos.Y - 1))
            {
                currentLevel.RemoveItem((int)CurrentPos.X + frame.Width - 1, (int)CurrentPos.Y - 1);
                Score += 10;
            }
        }

        public override void DOWN(int speedVal)
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

            if (currentLevel.IsPickUp((int)CurrentPos.X, (int)CurrentPos.Y + frame.Height)) 
            {
                currentLevel.RemoveItem((int)CurrentPos.X, (int)CurrentPos.Y + frame.Height);
                Score += 10;
            }
             else if (currentLevel.IsPickUp((int)CurrentPos.X + frame.Width - 1, (int)CurrentPos.Y + frame.Height))
            {
                currentLevel.RemoveItem((int)CurrentPos.X + frame.Width - 1, (int)CurrentPos.Y + frame.Height);
                Score += 10;
            }
        }

        public override void LEFT()
        {
            if (!currentLevel.IsWall((int)CurrentPos.X - 1, (int)CurrentPos.Y) &&
                !currentLevel.IsWall((int)CurrentPos.X - 1, (int)CurrentPos.Y + frame.Height - 1))
            {
                CurrentPos.X -= 2;
            }

            if (currentLevel.IsPickUp((int)CurrentPos.X - 1, (int)CurrentPos.Y)) 
            {
                currentLevel.RemoveItem((int)CurrentPos.X - 1, (int)CurrentPos.Y);
                Score += 10;
            }
            else if (currentLevel.IsPickUp((int)CurrentPos.X - 1, (int)CurrentPos.Y + frame.Height - 1))
            {
                currentLevel.RemoveItem((int)CurrentPos.X - 1, (int)CurrentPos.Y + frame.Height - 1);
                Score += 10;
            }
        }

        public override void RIGHT()
        {
            if (!currentLevel.IsWall((int)CurrentPos.X + frame.Width , (int)CurrentPos.Y + frame.Height - 1) &&
                !currentLevel.IsWall((int)CurrentPos.X + frame.Width, (int)CurrentPos.Y))
            {
                CurrentPos.X += 2;
            }

            if (currentLevel.IsPickUp((int)CurrentPos.X + frame.Width, (int)CurrentPos.Y + frame.Height - 1))
            {
                currentLevel.RemoveItem((int)CurrentPos.X + frame.Width, (int)CurrentPos.Y + frame.Height - 1);
                Score += 10;
            }
             else if (currentLevel.IsPickUp((int)CurrentPos.X + frame.Width, (int)CurrentPos.Y))
            {
                currentLevel.RemoveItem((int)CurrentPos.X + frame.Width, (int)CurrentPos.Y);
                Score += 10;
            }
        }
    }
}
