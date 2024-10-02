using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace StateMachines.Scripts
{
    internal class GameOver
    {
        private double timeLimit;
        private double totalTime;


        public E_Gamestates Update(double deltaTime)
        {
            totalTime += 0.0f;
            timeLimit += 5.0f;

            totalTime += deltaTime;

            if (totalTime >= timeLimit)
            {
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
