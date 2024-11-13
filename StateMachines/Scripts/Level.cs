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
        private Texture2D pellet;
        private Texture2D hplat1;
        private Vector2 wh;
        private string[] levelfile;
        private int currentLevel;
        private bool[,] Items = new bool[8,21];
        
        public Level()
        {
            currentLevel = 1;
            BuildNewLevel();
        }

        public void LoadContent(ContentManager cm, string wallname,  string pelletname,  string platname)
        {
            wall = cm.Load<Texture2D>(wallname);
            pellet = cm.Load<Texture2D>(pelletname);
            hplat1 = cm.Load<Texture2D>(platname);
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
            // Gets level and level size, checks if there is a P for pickup.
            levelfile = File.ReadAllLines(@"..\Levels\Level " + currentLevel + ".txt");
            for (int col = 0; col < GetArrayWidth(); col++)
            {
                for (int row = 0; row < GetArrayHeight(); row++)
                {
                    if (levelfile[row][col] == 'P')
                    {
                        Items[row,col] = true;
                    }
                }
            }
            foreach (var line in levelfile)
            {
                Console.WriteLine(line);
            }
        }

        public void ResetLevel()
        {
            currentLevel = 1;
        }

        public bool IsInSameRow(int y1, int y2)
        {
            int first = y1 / wall.Height;
            int second = y2 / wall.Height;

            if (first == second)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public void Draw(SpriteBatch sprite)
        {
            // Checks level width and height and draws a wall if a W is present.
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

            for (int col = 0; col < GetArrayWidth(); col++)
            {
                for (int row = 0; row < GetArrayHeight(); row++)
                {
                    if (levelfile[row][col] == 'J')
                    {
                        sprite.Draw(hplat1, new Vector2(hplat1.Width * col, hplat1.Height * row), Color.White);
                    }
                }
            }

            for (int col = 0; col < GetArrayWidth(); col++)
            {
                for (int row = 0; row < GetArrayHeight(); row++)
                {                    
                     if (Items[row,col] == true)
                     {
                        sprite.Draw(pellet, new Vector2(wall.Width * col, wall.Height * row), Color.White);
                     }                    
                }
            }
        }

        // Draws pellet for pick up
        public bool IsPickUp(int Xpos, int Ypox)
        {
            if (Items[(int)(Ypox / wall.Height),(int)(Xpos / wall.Width)] == true)
            {
                return true;
            }
            else
                return false;
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

        public bool IsPlatform(int Xpos, int Ypox)
        {
            if (levelfile[(int)(Ypox / hplat1.Height)][(int)(Xpos / hplat1.Width)] == 'J')
            {
                return true;
            }
            else
                return false;
        }
        public void RemoveItem(int Xpos, int Ypox)
        {
            Items[(int)(Ypox / wall.Height), (int)(Xpos / wall.Width)] = false;

        }
    }

   
}
