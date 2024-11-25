using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct2D1;

namespace StateMachines.Scripts
{
    internal class Menu
    {
        private Audio audio;
        private bool IsPlaying;
        Vector2 ScreenWH;

        public Menu(Vector2 dimensions, Audio GA)
        {
            ScreenWH = dimensions;
            audio = GA;
            IsPlaying = false;
        }

        public E_Gamestates Update(Game1 game) 
        {
            if (!IsPlaying == false)
            {
                audio.PlaySong(0, true);
                IsPlaying = true; // Makes it so the track is only played once
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && Mouse.GetState().X >= 0 && Mouse.GetState().X <= ScreenWH.X - 1 && Mouse.GetState().Y >= 0 && Mouse.GetState().Y <= ScreenWH.Y - 1)
            {
                if (MediaPlayer.State == MediaState.Playing)
                {
                    MediaPlayer.Stop();
                }
                // If left mouse button pressed play game.
                return E_Gamestates.PLAY;

            } else if (Mouse.GetState().RightButton == ButtonState.Pressed && Mouse.GetState().X >= 0 && Mouse.GetState().X <= ScreenWH.X - 1 && Mouse.GetState().Y >= 0 && Mouse.GetState().Y <= ScreenWH.Y - 1)

            {
                // If right mouse button pressed end and close the game.
                game.Exit();
            }

            return E_Gamestates.MENU;


        }

        public void Draw(GraphicsDevice graphics)
        {
            graphics.Clear(Color.White);
        }
    }


}
