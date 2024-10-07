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

        public Creature(Vector2 Pos) 
        {
            StartPos = Pos;
            CurrentPos = StartPos;

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

        public Vector2 GetPos()
        {
            return CurrentPos;
        }

    }
}
