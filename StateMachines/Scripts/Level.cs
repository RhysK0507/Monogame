using Microsoft.Win32;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.IO;
using System.Linq;
using System.Security.Policy;

namespace StateMachines.Scripts
{
    internal class Level
    {
        private Texture2D wall;
        private Vector2 wh;
        private string[] levelfile;
        private int currentLevel;
        
        public Level()
        {
            currentLevel = 1;
            BuildNewLevel();
        }

        public void LoadContent(ContentManager cm, string name)
        {
            wall = cm.Load<Texture2D>(name);
            wh = new Vector2(wall.Width, wall.Height);
        }

        private int GetArrayWidth()
        {
            return levelfile[0].Length;
        }

        private int GetArrayHeight()
        {
            return levelfile.Length;
        }

        public Vector2 GetLevelSize()
        {
            return new Vector2(GetArrayWidth() * wh.X, GetArrayHeight() * wh.Y);
        }

        public void addLevel()
        {
            currentLevel++;
        }

        public int returnLevel()
        {
            return currentLevel;
        }

        public void BuildNewLevel()
        {
            levelfile = File.ReadAllLines(@"..\Levels\Level " + currentLevel + ".txt");
            foreach (var line in levelfile)
            {
                Console.WriteLine(line);
            }
        }

        public void ResetLevel()
        {
            currentLevel = 1;
        }

        public void Draw(SpriteBatch sprite)
        {
            for (int col = 0; col < GetArrayWidth(); col++) 
            {
                for (int row = 0; row < GetArrayHeight(); row++)
                {
                    if (levelfile[row][col] == 'W')
                    {
                        sprite.Draw(wall, new Vector2(wall.Width * col, wall.Height * row), Color.White);
                    }
                }
            }
        }

        public bool IsWall(int Xpos, int Ypox)
        {
            if (levelfile[(int)(Ypox / wall.Height)][(int)(Xpos/ wall.Width)] == 'W')
                {
                    return true;
                }
                else
                    return false;                            
        }
    }

 
}
