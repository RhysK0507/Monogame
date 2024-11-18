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


        private Texture2D background;
        private Texture2D background2;
        private Texture2D foreground;
        private Vector2 curPos;

        public PlayGame()
        {
            // Spawns the level, player and enemy.
            level = new Level();
            player = new Player(new Vector2(200, 200), 3, new Rectangle(52 * 3, 0, 52, 72), level, 2);
            enemy = new Enemy(new Vector2(900, 600), new Rectangle(52 * 3, 72 * 4, 52, 72), level, 1);  
            curPos = new Vector2(0, 0);
        }

        public void LoadContent(ContentManager cm, GraphicsDeviceManager graphics, GraphicsDevice GraphicsDevice)
        {
            player.LoadContent(cm, "Chara6");
            enemy.LoadContent(cm, "Orc2");
            level.LoadContent(cm, "Wall1", "Pellet", "hplat1");
            renderTarget = new RenderTarget2D(GraphicsDevice, (int)level.GetLevelSize().X, (int)level.GetLevelSize().Y);

            // Gets the level width and height
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();

            background = cm.Load<Texture2D>("backgroundCastlesbig");
            background2 = cm.Load<Texture2D>("castle_grey");
            foreground = cm.Load<Texture2D>("tree31");


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
            graphics.Clear(Color.Red);
            sb.Begin();

            for (int Bg = 0; Bg < 2; Bg++)
            {
               sb.Draw(background, new Vector2((background.Width * Bg) + curPos.X * 0.1f, 0), Color.White);
            }

            for (int Bg = 0; Bg < 11; Bg++)
            {
                sb.Draw(background2, new Vector2((1000 * Bg) + curPos.X * 0.25f, (float)(GetLevelWH().Y - background2.Height - level.GetWH().Y)), Color.White);
            }

            level.Draw(sb);
            player.Draw(sb);
            enemy.Draw(sb);

            for (int Bg = 0; Bg < 10; Bg++)
            {
                sb.Draw(foreground, new Vector2((500 * Bg) + curPos.X * 1.25f, (float)(GetLevelWH().Y - foreground.Height)), Color.White);
            }


            sb.End();
            graphics.SetRenderTarget(null);
        }

        public E_Gamestates Update(double deltaTime, GraphicsDevice GraphicsDevice, GraphicsDeviceManager deviceManager)
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
                if(player.GetScroll())
                {
                    if (player.GetPos().X < (int)GetLevelWH().X - deviceManager.PreferredBackBufferWidth / 2 &&
                        player.GetPos().X > deviceManager.PreferredBackBufferWidth / 2)
                    {
                        curPos.X += player.GetSpeed();
                    }
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
               
                player.setAnimationState(E_Gameanimations.RIGHT);
                player.RIGHT();
                if (player.GetPos().X < (int)GetLevelWH().X - deviceManager.PreferredBackBufferWidth / 2 &&
                    player.GetPos().X > deviceManager.PreferredBackBufferWidth / 2)
                {
                    curPos.X -= player.GetSpeed();
                }
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
                player.ResetPos();
                enemy.ResetPos();
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
            
            sprite.Begin();
            graphics.Clear(Color.LightBlue);
            int rectx = (int)player.GetPos().X - Device.PreferredBackBufferWidth / 2;
            if (rectx > (int)GetLevelWH().X - Device.PreferredBackBufferWidth)
            {
                rectx = (int)GetLevelWH().X - Device.PreferredBackBufferWidth;
            }
            else if (rectx < 0)
            {
                rectx = 0;
            }

            int recty = (int)player.GetPos().Y - Device.PreferredBackBufferHeight / 2;
            if (recty > (int)GetLevelWH().Y - Device.PreferredBackBufferHeight)
            {
                recty = (int)GetLevelWH().Y - Device.PreferredBackBufferHeight;
            }
            else if (recty < 0)
            {
                recty = 0;
            }

            sprite.Draw(renderTarget, new Vector2(0, 0), new Rectangle(rectx, recty, Device.PreferredBackBufferWidth, Device.PreferredBackBufferHeight), Color.White);
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
