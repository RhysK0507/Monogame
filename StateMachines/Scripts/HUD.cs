using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Automation;
using static System.Net.Mime.MediaTypeNames;

namespace StateMachines.Scripts
{
    internal class HUD
    {
        private SpriteFont font;
        private string message;
        private Texture2D hud_heartFull;

        public void LoadContent(ContentManager cm)
        {
            hud_heartFull = cm.Load<Texture2D>("hud_heartFull");
            font = cm.Load<SpriteFont>("File");
        }

        public void SetMessage(string str)
        {
            message = str;
        }

        public void DrawString(SpriteBatch sprite, Vector2 pos, Color colour)
        {
            sprite.DrawString(font, message, pos, colour);
        }

        public void DrawLife(SpriteBatch sprite, Vector2 position, int life)
        {
            for (int lives = 0; lives < life; lives++)
            {
                sprite.Draw(hud_heartFull,
                new Vector2(position.X + font.MeasureString(message).X + lives * hud_heartFull.Width, position.Y),
                new Rectangle(0, 0, hud_heartFull.Width, hud_heartFull.Height), Color.White);
            }
            
        }
    }
}
