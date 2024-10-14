using Microsoft.Win32;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.IO;
using System.Linq;

namespace StateMachines.Scripts
{
    internal class Level
    {
        private Texture2D wall;
        private Vector2 wh;
        private string[] levelfile;
        private int currentLevel;
        
        public void setCur()
        {
            currentLevel = 1;
            BuildNewLevel();
        }

        public void LoadContent(ContentManager cm, string name)
        {
            wall = cm.Load<Texture2D>(name);
            wh = new Vector2(wall.Bounds.X, wall.Bounds.Y);
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
            levelfile = File.ReadAllLines(@"..\levels\Level " + currentLevel + ".txt");
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
            for (int i = 0; i < GetArrayWidth(); i++) 
            {
                for (int j = 0; j < GetArrayHeight(); j++)
                {
                    if (levelfile[i] == "W")
                    {
                        sprite.Draw(wall, new Vector2(wall.Width * i, wall.Height * j), Color.White);
                    }
                }
            }
        }
    }

 
}
