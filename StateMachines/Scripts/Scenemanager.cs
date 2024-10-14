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

        public void LoadContent(ContentManager cm, GraphicsDeviceManager graphics) 
        { 
            play.LoadContent(cm, graphics);
            GameMenu = new Menu(play.GetScreenWH());
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
                    break;
                case E_Gamestates.GAMEOVER:
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
