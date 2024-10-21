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

        public Player(Vector2 pos,int lives, Rectangle rect, Level cLevel) : base(pos, rect, cLevel)
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

        public void ResetLives()
        {
            if (CurrentLives == 0)
            {
                CurrentLives = InitialLives;
            }
        }

        public override void UP()
        {
            if (!currentLevel.IsWall((int)CurrentPos.X, (int)CurrentPos.Y) ||
                !currentLevel.IsWall((int)CurrentPos.X + frame.Width - 1, (int)CurrentPos.Y))
            {
                CurrentPos.Y -= 2;
            }
        }

        public override void DOWN()
        {
            if (!currentLevel.IsWall((int)CurrentPos.X, (int)CurrentPos.Y + frame.Height - 1) ||
                !currentLevel.IsWall((int)CurrentPos.X + frame.Width - 1, (int)CurrentPos.Y + frame.Height - 1))
            {
                CurrentPos.Y += 2;
            }
        }

        public override void LEFT()
        {
            if (!currentLevel.IsWall((int)CurrentPos.X, (int)CurrentPos.Y) ||
                !currentLevel.IsWall((int)CurrentPos.X, (int)CurrentPos.Y + frame.Height - 1))
            {
                CurrentPos.X -= 2;
            }
        }

        public override void RIGHT()
        {
            if (!currentLevel.IsWall((int)CurrentPos.X + frame.Width - 1, (int)CurrentPos.Y) ||
                !currentLevel.IsWall((int)CurrentPos.X + frame.Width - 1, (int)CurrentPos.Y + frame.Height - 1))
            {
                CurrentPos.X += 2;
            }
        }

    }
}
