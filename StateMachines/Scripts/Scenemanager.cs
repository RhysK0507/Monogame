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
        private HUD HUD;
        //private SpriteFont font;
        //private string text; 

        public void LoadContent(ContentManager cm, GraphicsDeviceManager graphics, GraphicsDevice GraphicsDevice) 
        {
            //font = cm.Load<SpriteFont>("File");
            HUD.LoadContent(cm);
            play.LoadContent(cm, graphics, GraphicsDevice);
            GameMenu = new Menu(new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
        }

        public Scenemanager()
        {
            HUD = new HUD();
            E_States = E_Gamestates.MENU;
            play = new PlayGame();
            gameOver = new GameOver();
        }

        public void Update(Game1 game, GameTime time, GraphicsDevice GraphicsDevice)
        {
            double deltaTime = time.ElapsedGameTime.TotalSeconds;

            switch (E_States)
            {
                case E_Gamestates.MENU:
                    SwitchState(GameMenu.Update(game));
                    HUD.SetMessage("This is the menu");

                    break;
                case E_Gamestates.PLAY:
                    SwitchState(play.Update(deltaTime, GraphicsDevice)); 
                    break;
                case E_Gamestates.GAMEOVER:
                    SwitchState(gameOver.Update(deltaTime));
                    HUD.SetMessage("Gameover, you lost");
                    break;
                    default: break;
            }

        }


        public void Draw(GraphicsDevice graphics, SpriteBatch sprite, GraphicsDeviceManager Device)
        {

            switch (E_States)
            {
                case E_Gamestates.MENU:
                    sprite.Begin();
                    GameMenu.Draw(graphics);
                    HUD.SetMessage("This is the menu");
                    HUD.DrawString(sprite, new Vector2((Device.PreferredBackBufferWidth / 2) - 256, Device.PreferredBackBufferHeight / 2), Color.LightGreen);
                    sprite.End();
                    break;
                case E_Gamestates.PLAY:
                    play.Draw(graphics, sprite, Device,HUD);                   
                    break;
                case E_Gamestates.GAMEOVER:
                    sprite.Begin();
                    HUD.SetMessage("Gameover, you lost");
                    HUD.DrawString(sprite, new Vector2((Device.PreferredBackBufferWidth / 2) - 256, Device.PreferredBackBufferHeight / 2), Color.Red);
                    gameOver.Draw(graphics);
                    sprite.End();
                    break;
                default: break;
            }

        }

        private void SwitchState(E_Gamestates state)
        {
            E_States = state;
        }
    }
    
}
