using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;


namespace StateMachines.Scripts
{
    internal class Player : Creature
    {
        private int CurrentLives;
        private int InitialLives;
        private int Score;
        private bool IsMovingRight = true;
        private Scenemanager Scenemanager;
        private Enemy Enemy;

        public Player(Vector2 pos,int lives, Rectangle rect, Level cLevel, int InputSpeed, Audio audio) : base(pos, rect, cLevel, InputSpeed, audio)
        {
            CurrentLives = lives;
            InitialLives = CurrentLives;
            Score = 0;
        }

        public int GetLives()
        {
            return CurrentLives;
        }

        public int GetSpriteHeight()
        {
            return frame.Height;
        }

        public bool GetMovingRight()
        {
            return IsMovingRight;
        }

        public int GetSpriteWidth()
        {
            return frame.Width;
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
                AddScore(10);
            }
            else if (currentLevel.IsPickUp((int)CurrentPos.X + frame.Width - 1, (int)CurrentPos.Y - 1))
            {
                currentLevel.RemoveItem((int)CurrentPos.X + frame.Width - 1, (int)CurrentPos.Y - 1);
                AddScore(10);
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
                AddScore(10);
            }
            else if (currentLevel.IsPickUp((int)CurrentPos.X + frame.Width - 1, (int)CurrentPos.Y + frame.Height))
            {
                currentLevel.RemoveItem((int)CurrentPos.X + frame.Width - 1, (int)CurrentPos.Y + frame.Height);
                AddScore(10);
            }
        }

        public override void LEFT(int index, float vol)
        {
            IsMovingRight = false;
            if (!currentLevel.IsWall((int)CurrentPos.X - 1, (int)CurrentPos.Y) &&
                !currentLevel.IsWall((int)CurrentPos.X - 1, (int)CurrentPos.Y + frame.Height - 1))
            {
                if (!isJumping)
                {
                    gameAudio.PlaySFX(vol, index);
                }
                CurrentPos.X -= 2;
            }

            if (currentLevel.IsPickUp((int)CurrentPos.X - 1, (int)CurrentPos.Y)) 
            {
                currentLevel.RemoveItem((int)CurrentPos.X - 1, (int)CurrentPos.Y);
                AddScore(10);
            }
            else if (currentLevel.IsPickUp((int)CurrentPos.X - 1, (int)CurrentPos.Y + frame.Height - 1))
            {
                currentLevel.RemoveItem((int)CurrentPos.X - 1, (int)CurrentPos.Y + frame.Height - 1);
                AddScore(10);
            }
        }

        public override void RIGHT(int index, float vol)
        {
            IsMovingRight = true;
            if (!currentLevel.IsWall((int)CurrentPos.X + frame.Width , (int)CurrentPos.Y + frame.Height - 1) &&
                !currentLevel.IsWall((int)CurrentPos.X + frame.Width, (int)CurrentPos.Y))
            {
                if (!isJumping)
                {
                    gameAudio.PlaySFX(vol, index);
                }
                CurrentPos.X += 2;
            }

            if (currentLevel.IsPickUp((int)CurrentPos.X + frame.Width, (int)CurrentPos.Y + frame.Height - 1))
            {
                currentLevel.RemoveItem((int)CurrentPos.X + frame.Width, (int)CurrentPos.Y + frame.Height - 1);
                AddScore(10);
            }
            else if (currentLevel.IsPickUp((int)CurrentPos.X + frame.Width, (int)CurrentPos.Y))
            {
                currentLevel.RemoveItem((int)CurrentPos.X + frame.Width, (int)CurrentPos.Y);
                AddScore(10);
            }
        }

        public void AddScore(int tempScore)
        {
            Score += tempScore;
            currentLevel.AddPickedup();
            if (currentLevel.GetMaxPickups() - currentLevel.GetNumPickups() == 0)
            {
                currentLevel.addLevel();
                currentLevel.BuildNewLevel();
                ResetLives();
                ResetPos();
                Enemy.ResetPos();
            }
        }
    }
}
