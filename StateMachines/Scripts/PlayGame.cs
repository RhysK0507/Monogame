using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Threading;


namespace StateMachines.Scripts
{
    internal class PlayGame
    {
        private Player player;
        private Enemy enemy;

        public PlayGame()
        {
            player = new Player(new Vector2 (200,200), 3);
            System.Console.WriteLine("Player position = " + player.GetPos());
            enemy = new Enemy(new Vector2(50, 50));
            System.Console.WriteLine("Enemy position = " + enemy.GetPos());
        }

        public void LoadContent(ContentManager cm)
        {
            player.LoadContent(cm, "Chara6");
            enemy.LoadContent(cm, "Orc2");
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
            System.Console.WriteLine("Player position = " + player.GetPos() + "Player lives" + player.GetLives());
            System.Console.WriteLine("Enemy position = " + enemy.GetPos());

            if (enemy.Caught(player))
            {
                player.ReduceLives();
                System.Console.WriteLine("Player position = " + player.GetPos() + "Player lives" + player.GetLives());
                player.ResetPos();
                enemy.ResetPos();
            }

            if (player.GetLives() == 0)
            {
                System.Console.WriteLine("Player is dead");
                player.ResetLives();
                return E_Gamestates.GAMEOVER;
            }

            return E_Gamestates.PLAY;

        }

        public void Draw(GraphicsDevice graphics, SpriteBatch sprite)
        {
            graphics.Clear(Color.Red);


            player.Draw(sprite, new Rectangle(0, 0, 52, 72));
            enemy.Draw(sprite, new Rectangle(0, 0, 52, 72));


        }
    }
}
