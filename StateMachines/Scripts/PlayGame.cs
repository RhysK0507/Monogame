using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace StateMachines.Scripts
{
    internal class PlayGame
    {
        public E_Gamestates Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return E_Gamestates.MENU;
            }

            if (Keyboard.GetState ().IsKeyDown(Keys.Space)) 
            {
                return E_Gamestates.GAMEOVER;
            }
            
            return E_Gamestates.PLAY;

        }

        public void Draw(GraphicsDevice graphics)
        {
            graphics.Clear(Color.Red);

        }
    }
}
