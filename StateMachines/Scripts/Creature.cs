using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Runtime.Loader;

namespace StateMachines.Scripts
{
    internal class Creature
    {
        protected Vector2 StartPos;
        protected Vector2 CurrentPos;
        protected Texture2D Sprite;

        public void LoadContent(ContentManager cm, string name)
        {
            Sprite = cm.Load<Texture2D>(name);
        }
        
        public void Draw(SpriteBatch spritebatch, Rectangle rect) 
        {
            spritebatch.Draw(Sprite, CurrentPos, rect, Color.White);
        }

        public Creature(Vector2 Pos) 
        {
            StartPos = Pos;
            CurrentPos = StartPos;

        }

        public virtual void UP()
        {
            CurrentPos.Y -= 1;
        }

        public virtual void DOWN()
        {
            CurrentPos.Y += 1;
        }

        public virtual void LEFT()
        {
            CurrentPos.X -= 1;
        }

        public virtual void RIGHT()
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
