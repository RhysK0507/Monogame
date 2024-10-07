using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StateMachines.Scripts
{
    internal class Enemy : Creature
    {
        public Enemy(Vector2 pos) : base(pos) 
        {
              
        }

        public void Chase()
        {

        }

        public void Caught()
        {
            return;
        }
    } 
}
