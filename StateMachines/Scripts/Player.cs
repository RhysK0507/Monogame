using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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

        public void GetLives(int lives)
        {
            return; 
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
