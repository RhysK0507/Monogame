﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Windows.Forms;

namespace StateMachines.Scripts
{
    internal class Projectile
    {
        private bool inUseProjectile;
        private ContentManager content;
        private GraphicsDevice graphics;
        private GraphicsDeviceManager deviceManager;
        private Texture2D bullet;
        private Vector2 pos;
        private int projectileSpeed;
        private Level level;

        public Projectile(ContentManager contentManager, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager, int speed) 
        {
            content = contentManager;
            graphics = graphicsDevice;
            deviceManager = graphicsDeviceManager;
            inUseProjectile = false;
            speed = projectileSpeed;
            pos = Vector2.Zero;
            }

        public void LoadContent(string name)
        {
            bullet = content.Load<Texture2D>(name);
        }

        public void Update(Level returnLevel, bool isEnemyFiring)
        {
            if (isEnemyFiring)
            {
                if (!level.IsWall((int)pos.X - 1, (int)pos.Y) == false)
                {
                    pos.X -= projectileSpeed;
                } else
                {
                    inUseProjectile = false;
                }
            }
            if (!isEnemyFiring)
            {
                if (!level.IsWall((int)pos.X + 1, (int)pos.Y) == false)
                {
                    pos.X += projectileSpeed;
                }
                else
                {
                    inUseProjectile = false;
                }
            }
        }

        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(bullet, pos, new Rectangle(0, 0, bullet.Width, bullet.Height), Color.White);
        }

        public bool CollidesWith(Creature creature) 
        {
            if (pos.X <= creature.GetPos().X + GetSpriteWidth() - 1
                && pos.X + GetSpriteWidth() - 1 >= creature.GetPos().X
                && pos.Y <= creature.GetPos().Y + GetSpriteHeight() - 1
                && pos.Y + GetSpriteHeight() - 1 >= creature.GetPos().Y)
            {
                return true;
            } else
            {
                return false;  
            }
        }

        private int GetSpriteWidth()
        {
            return 335;
        }

        private int GetSpriteHeight()
        {
            return 159;
        }

        public void ActivateBullet(Vector2 startPos)
        {
            pos = startPos;
            inUseProjectile = true;
        }

        public bool IsProjectileActive()
        {
            return inUseProjectile; 
        }

        public void ResetBullet()
        {
            inUseProjectile = false;
            pos = Vector2.Zero;
        }

        public Vector2 GetPosition()
        {
            return pos;
        }
    }
}
