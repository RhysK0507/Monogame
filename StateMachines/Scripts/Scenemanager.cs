using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StateMachines.Scripts
{
    enum E_Gamestates {MENU, PLAY, GAMEOVER }
    internal class Scenemanager
    {
        private E_Gamestates E_States;
        private Menu GameMenu;
        private PlayGame play;
        private GameOver gameOver;

        public void SceneManager(Vector2 ScreenRect)
        {
            E_States = E_Gamestates.MENU;
            GameMenu = new Menu();
            play = new PlayGame();
            gameOver = new GameOver();
        }

        private void Update(Game1 game, GameTime time)
        {

        }

        private void Draw(GraphicsDevice graphics)
        {

        }

        private void SwitchState(E_Gamestates state)
        {
            e_scene = state;
        }
    }
    
}
