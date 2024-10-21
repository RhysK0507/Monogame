using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Runtime.Loader;
using Microsoft.VisualBasic.ApplicationServices;

namespace StateMachines.Scripts
{
    internal class Creature
    {
        protected Vector2 StartPos;
        protected Vector2 CurrentPos;
        protected Texture2D Sprite;
        public Level currentLevel;
        public Rectangle frame;

        public void LoadContent(ContentManager cm, string name)
        {
            Sprite = cm.Load<Texture2D>(name);
        }
        
        public void Draw(SpriteBatch spritebatch, Rectangle rect) 
        {
            spritebatch.Draw(Sprite, CurrentPos, rect, Color.White);
        }

        public Creature(Vector2 Pos, Rectangle rect, Level cLevel) 
        {
            frame = rect;
            currentLevel = cLevel;
            StartPos = Pos;
            CurrentPos = StartPos;

        }

        public virtual void UP()
        {
            if (!currentLevel.IsWall((int)CurrentPos.X, (int)CurrentPos.Y) || 
                !currentLevel.IsWall((int)CurrentPos.X + frame.Width - 1, (int)CurrentPos.Y))
             {
                CurrentPos.Y -= 1;
            }
        }

        public virtual void DOWN()
        {
            if (!currentLevel.IsWall((int)CurrentPos.X, (int)CurrentPos.Y + frame.Height - 1) || 
                !currentLevel.IsWall((int)CurrentPos.X + frame.Width - 1, (int)CurrentPos.Y + frame.Height - 1))
            {
                CurrentPos.Y += 1;
            }
        }

        public virtual void LEFT()
        {
            if (!currentLevel.IsWall((int)CurrentPos.X, (int)CurrentPos.Y) ||
                !currentLevel.IsWall((int)CurrentPos.X, (int)CurrentPos.Y + frame.Height - 1))
            {
                CurrentPos.X -= 1;
            }
        }

        public virtual void RIGHT()
        {
            if (!currentLevel.IsWall((int)CurrentPos.X + frame.Width - 1, (int)CurrentPos.Y) ||
                !currentLevel.IsWall((int)CurrentPos.X + frame.Width - 1, (int)CurrentPos.Y + frame.Height - 1))
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
