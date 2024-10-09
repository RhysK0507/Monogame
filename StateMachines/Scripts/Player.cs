using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace StateMachines.Scripts
{
    internal class Player : Creature
    {
        private int CurrentLives;
        private int InitialLives;
        private int Score;

        public Player(Vector2 pos, int lives) : base(pos)
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
            CurrentPos.Y -= 2;
        }

        public override void DOWN()
        {
            CurrentPos.Y += 2;
        }

        public override void LEFT()
        {
            CurrentPos.X -= 2;
        }

        public override void RIGHT()
        {
            CurrentPos.X += 2;
        }

    }
}
