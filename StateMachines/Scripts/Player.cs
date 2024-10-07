using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

    }
}
