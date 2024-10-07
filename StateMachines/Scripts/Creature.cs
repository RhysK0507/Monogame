using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StateMachines.Scripts
{
    internal class Creature
    {
        protected Vector2 StartPos;
        protected Vector2 CurrentPos;

        public void Start(Vector2 startPos, Vector2 currentPos) 
        {
            StartPos = startPos;
            CurrentPos = currentPos;

        }

        public void UP()
        {
            CurrentPos.Y += 1;
        }

        public void DOWN()
        {
            CurrentPos.Y -= 1;
        }

        public void LEFT()
        {
            CurrentPos.X -= 1;
        }

        public void RIGHT()
        {
            CurrentPos.X += 1;
        }

        public void ResetPos() 
        {
            CurrentPos.X = StartPos.X;
            CurrentPos.Y = StartPos.Y;   
        }

    }
}
