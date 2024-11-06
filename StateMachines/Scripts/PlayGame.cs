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
        private bool JumpIsPressed = false;

        public PlayGame()
        {
            // Spawns the level, player and enemy.
            level = new Level();
            player = new Player(new Vector2(200, 200), 3, new Rectangle(52 * 3, 0, 52, 72), level, 2);
            enemy = new Enemy(new Vector2(900, 600), new Rectangle(52 * 3, 72 * 4, 52, 72), level, 1);           
        }

        public void LoadContent(ContentManager cm, GraphicsDeviceManager graphics)
        {
            player.LoadContent(cm, "Chara6");
            enemy.LoadContent(cm, "Orc2");
            level.LoadContent(cm, "Wall1", "Pellet", "hplat1");
            // Gets the level width and height
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
            player.JumpOrFall();

            if (Keyboard.GetState().IsKeyDown(Keys.W)) 
            {
                if (JumpIsPressed == false)
                {
                    JumpIsPressed = true;
                    player.SetisJumping();
                }
               
            }
            if (Keyboard.GetState().IsKeyUp(Keys.W))
            {
                JumpIsPressed = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                player.LEFT();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                player.RIGHT();
            }

            //if (Keyboard.GetState().IsKeyDown(Keys.S))
            //{
            //    player.DOWN();
            //}


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


            player.Draw(sprite);
            enemy.Draw(sprite);
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
