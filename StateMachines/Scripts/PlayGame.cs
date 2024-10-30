using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Threading;
using System.Windows.Forms.Automation;


namespace StateMachines.Scripts
{
    internal class PlayGame
    {
        private Player player;
        private Enemy enemy;
        private Level level;

        public PlayGame()
        {
            level = new Level();
            player = new Player(new Vector2(200, 200), 3, new Rectangle(0, 0, 52, 72), level);
            enemy = new Enemy(new Vector2(900, 600), new Rectangle(0, 0, 52, 72), level);           
        }

        public void LoadContent(ContentManager cm, GraphicsDeviceManager graphics)
        {
            player.LoadContent(cm, "Chara6");
            enemy.LoadContent(cm, "Orc2");
            level.LoadContent(cm, "Wall1", cm, "Pellet");
            //level.pickUp();
            graphics.PreferredBackBufferWidth = (int)level.GetLevelSize().X;
            graphics.PreferredBackBufferHeight = (int)level.GetLevelSize().Y;
            graphics.ApplyChanges();
        }

        public E_Gamestates Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return E_Gamestates.MENU;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W)) 
            {
                player.UP();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.LEFT();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                player.RIGHT();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                player.DOWN();
            }


            enemy.Chase(player);

            if (enemy.CollidesWith(player))
            {
                player.ReduceLives();
                System.Console.WriteLine("Player lives" + player.GetLives());
                player.ResetPos();
                enemy.ResetPos();
            }

            

            if (player.GetLives() == 0)
            {
                System.Console.WriteLine("Player is dead");
                player.ResetLives();
                level.ResetLevel();
                return E_Gamestates.GAMEOVER;
            }


            return E_Gamestates.PLAY;

        }

        public void Draw(GraphicsDevice graphics, SpriteBatch sprite)
        {
            graphics.Clear(Color.Red);

            level.Draw(sprite);


            player.Draw(sprite, new Rectangle(0, 0, 52, 72));
            enemy.Draw(sprite, new Rectangle(0, 0, 52, 72));
        }

        public Vector2 GetScreenWH()
        {
            return level.GetLevelSize();
        }
         
        public int GetLevelNumber()
        {
            return level.returnLevel();
        }

        public int GetScore()
        {
            return player.GetScore();
        }

        public int GetLives()
        {
            return player.GetLives();
        }
    }
}
