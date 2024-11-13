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
        private RenderTarget2D renderTarget;

        public PlayGame()
        {
            // Spawns the level, player and enemy.
            level = new Level();
            player = new Player(new Vector2(200, 200), 3, new Rectangle(52 * 3, 0, 52, 72), level, 2);
            enemy = new Enemy(new Vector2(900, 600), new Rectangle(52 * 3, 72 * 4, 52, 72), level, 1);  
        }

        public void LoadContent(ContentManager cm, GraphicsDeviceManager graphics, GraphicsDevice GraphicsDevice)
        {
            player.LoadContent(cm, "Chara6");
            enemy.LoadContent(cm, "Orc2");
            level.LoadContent(cm, "Wall1", "Pellet", "hplat1");
            renderTarget = new RenderTarget2D(GraphicsDevice, (int)level.GetLevelSize().X, (int)level.GetLevelSize().Y);

            // Gets the level width and height
            graphics.PreferredBackBufferWidth = (int)level.GetLevelSize().X;
            graphics.PreferredBackBufferHeight = (int)level.GetLevelSize().Y;
            graphics.ApplyChanges();

        }

        private void ResetAll(GraphicsDevice graphics)
        {
            enemy.ResetPos();
            player.ResetPos();
            player.ResetLives();
            level.ResetLevel();
            renderTarget = new RenderTarget2D(graphics, (int)level.GetLevelSize().X, (int)level.GetLevelSize().Y);
        }

        private void DrawRenderTarget(SpriteBatch sb, GraphicsDevice graphics, GraphicsDeviceManager deviceManager)
        {
            graphics.SetRenderTarget(renderTarget);
            graphics.Clear(Color.LightBlue);
            sb.Begin();
            level.Draw(sb);
            player.Draw(sb);
            enemy.Draw(sb);
            sb.End();

            graphics.SetRenderTarget(null);

            int rectx = (int)player.GetPos().X - deviceManager.PreferredBackBufferWidth / 2;
            if (rectx > (int)GetLevelWH().X - deviceManager.PreferredBackBufferWidth)
            {
                rectx = (int)GetLevelWH().X - deviceManager.PreferredBackBufferWidth;
            } else if (rectx < 0)
            {
                rectx = 0;
            }

            int recty = (int)player.GetPos().Y - deviceManager.PreferredBackBufferHeight / 2;
            if (recty > (int)GetLevelWH().Y - deviceManager.PreferredBackBufferHeight)
            {
                recty = (int)GetLevelWH().Y - deviceManager.PreferredBackBufferHeight;
            }
            else if (recty < 0)
            {
                recty = 0;
            }

            sb.Draw(renderTarget, new Vector2(0, 0), new Rectangle(rectx, recty, deviceManager.PreferredBackBufferWidth, deviceManager.PreferredBackBufferHeight), Color.White);
        }

        public E_Gamestates Update(double deltaTime, GraphicsDevice GraphicsDevice)
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
               
                player.setAnimationState(E_Gameanimations.LEFT);
                player.LEFT();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
               
                player.setAnimationState(E_Gameanimations.RIGHT);
                player.RIGHT();
            }
            player.setCurrentFrame(deltaTime);
            if (Keyboard.GetState().IsKeyUp(Keys.D) && (Keyboard.GetState().IsKeyUp(Keys.A)))
            {
                player.setAnimationState(E_Gameanimations.IDLE);
            }

            //if (Keyboard.GetState().IsKeyDown(Keys.S))
            //{
            //    player.DOWN();
            //}


            enemy.Chase(player);
            enemy.setCurrentFrame(deltaTime);

            if (enemy.CollidesWith(player))
            {
                player.ReduceLives();
                System.Console.WriteLine("Player lives" + player.GetLives());
            }

            

            if (player.GetLives() == 0)
            {
                System.Console.WriteLine("Player is dead");
                ResetAll(GraphicsDevice);
                return E_Gamestates.GAMEOVER;
            }


            return E_Gamestates.PLAY;

        }

        public void Draw(GraphicsDevice graphics, SpriteBatch sprite, GraphicsDeviceManager Device, HUD GameHud)
        {

            DrawRenderTarget(sprite, graphics, Device);
            graphics.Clear(Color.Red);
            sprite.Begin();


            GameHud.SetMessage("Level: " + GetLevelNumber());
            GameHud.DrawString(sprite, new Vector2(400, 0), Color.Blue);
            GameHud.SetMessage("Score:" + GetScore());
            GameHud.DrawString(sprite, new Vector2(Device.PreferredBackBufferWidth - 250, 0), Color.Green);
            GameHud.SetMessage("Lives: ");
            GameHud.DrawString(sprite, new Vector2(20, 0), Color.Red);
            GameHud.DrawLife(sprite, new Vector2(0, 10), GetLives());
            sprite.End();
        }

        public Vector2 GetLevelWH()
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
