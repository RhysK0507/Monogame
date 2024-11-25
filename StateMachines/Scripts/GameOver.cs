using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace StateMachines.Scripts
{
    internal class GameOver
    {
        private double timeLimit = 5.0f;
        private double totalTime = 0.0f;
        private Audio audio;
        private bool IsPlaying;

        public GameOver(Audio GA) 
        {
            audio = GA;
            IsPlaying = false;
        }


        public E_Gamestates Update(double deltaTime)
        {
            if (!IsPlaying)
            {
                audio.PlaySong(2, true);
                IsPlaying = true;
            }

            totalTime += deltaTime;

            if (totalTime >= timeLimit)
            {
                MediaPlayer.Stop();
                IsPlaying = false;
                totalTime = 0.0f;
                return E_Gamestates.MENU;
            }



            return E_Gamestates.GAMEOVER;
        }

        public void Draw(GraphicsDevice graphics)
        {
            graphics.Clear(Color.Yellow);
            
        }

    }
}
