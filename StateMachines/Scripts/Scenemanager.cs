using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace StateMachines.Scripts
{
    enum E_Gamestates { MENU, PLAY, GAMEOVER }
    internal class Scenemanager
    {
        private E_Gamestates E_States;
        private Menu GameMenu;
        private PlayGame play;
        private GameOver gameOver;
        private SpriteFont font;
        private string text; 

        public void LoadContent(ContentManager cm, GraphicsDeviceManager graphics) 
        {
            font = cm.Load<SpriteFont>("File");
            play.LoadContent(cm, graphics);
            GameMenu = new Menu(play.GetScreenWH());
        }

        private void SetMessage(string str)
        {
            text = str;
        }

        public Scenemanager()
        {
            E_States = E_Gamestates.MENU;
            play = new PlayGame();
            gameOver = new GameOver();
        }

        public void Update(Game1 game, GameTime time)
        {
            double deltaTime = time.ElapsedGameTime.TotalSeconds;

            switch (E_States)
            {
                case E_Gamestates.MENU:
                    SwitchState(GameMenu.Update(game));
                   
                    break;
                case E_Gamestates.PLAY:
                    SwitchState(play.Update()); 
                    break;
                case E_Gamestates.GAMEOVER:
                    SwitchState(gameOver.Update(deltaTime));
                    break;
                    default: break;
            }

        }


        public void Draw(GraphicsDevice graphics, SpriteBatch sprite)
        {
            sprite.Begin();

            switch (E_States)
            {
                case E_Gamestates.MENU:
                    GameMenu.Draw(graphics);
                    break;
                case E_Gamestates.PLAY:
                    play.Draw(graphics, sprite);
                    SetMessage("This is the Game Pay screen. Level: " + play.GetLevelNumber());
                    sprite.DrawString(font, text, new Vector2((play.GetScreenWH().X / 2) - 500, 0), Color.Blue);
                    SetMessage("Lives: " + play.GetLives());
                    sprite.DrawString(font, text, new Vector2((play.GetScreenWH().X / 2) - 800, 0), Color.Red);
                    break;
                case E_Gamestates.GAMEOVER:
                    SetMessage("This is the Game Over screen. You lost!");
                    sprite.DrawString(font, text, new Vector2((play.GetScreenWH().X / 2) - 500, 0), Color.Red);
                    gameOver.Draw(graphics);
                    break;
                default: break;
            }

            sprite.End();
        }

        private void SwitchState(E_Gamestates state)
        {
            E_States = state;
        }
    }
    
}
