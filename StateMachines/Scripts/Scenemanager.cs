﻿using Microsoft.Xna.Framework;
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

        public void LoadContent(ContentManager cm, GraphicsDeviceManager graphics) 
        {
            //font = cm.Load<SpriteFont>("File");
            HUD.LoadContent(cm);
            play.LoadContent(cm, graphics);
            GameMenu = new Menu(play.GetScreenWH());
        }

        public Scenemanager()
        {
            HUD = new HUD();
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
                    HUD.SetMessage("This is the menu");

                    break;
                case E_Gamestates.PLAY:
                    SwitchState(play.Update(deltaTime)); 
                    break;
                case E_Gamestates.GAMEOVER:
                    SwitchState(gameOver.Update(deltaTime));
                    HUD.SetMessage("Gameover, you lost");
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
                    HUD.SetMessage("This is the menu");
                    HUD.DrawString(sprite, new Vector2(150, 0), Color.LightGreen);
                    break;
                case E_Gamestates.PLAY:
                    play.Draw(graphics, sprite);
                    HUD.SetMessage("Level: " + play.GetLevelNumber());
                    HUD.DrawString(sprite, new Vector2(400, 0), Color.Blue);
                    HUD.SetMessage("Score:" + play.GetScore());
                    HUD.DrawString(sprite, new Vector2(play.GetScreenWH().X - 250, 0), Color.Green);
                    HUD.SetMessage("Lives: ");
                    HUD.DrawString(sprite, new Vector2(20, 0), Color.Red);
                    HUD.DrawLife(sprite, new Vector2(0, 10), play.GetLives());
                    break;
                case E_Gamestates.GAMEOVER:
                    HUD.SetMessage("Gameover, you lost");
                    HUD.DrawString(sprite, new Vector2(400, 0), Color.Red);
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
