using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TopdownShooter
{
    class Unit
    {
        private int maxHealth;
        private int maxDamage;
        private int maxDefense;
        private int maxRange;
        private int maxSpeed;
        public int playerID { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int health { get; set; }
        public int damage { get; set; }
        public int defense { get; set; }
        public int range { get; set; }
        public bool selected { get; set; }
        private Texture2D texture;
        private Vector2 location;
        private Vector2 origin;
        private float speed;
        private float direction;
        private float rotationSpeed;
        
        public Unit (string name, string type, Texture2D texture, int health, int damage, int defense, int range, int speed)
        {
            this.name = name;
            this.type = type;
            this.texture = texture;
            this.health = health;
            this.damage = damage;
            this.defense = defense;
            this.range = range;
            this.selected = false;

            maxHealth = health;
            maxDamage = damage;
            maxDefense = defense;
            maxRange = range;
            maxSpeed = speed;
        }

        public void deploy(int playerID, int x, int y, float direction)
        {
            this.playerID = playerID;
            this.location = new Vector2(x, y);
            this.direction = direction;
            this.speed = 0.0F;
            this.rotationSpeed = (float)(Math.PI / 60);

            origin = new Vector2(texture.Width / 2.0F, texture.Height / 2.0F);
        }

        public void accelerate()
        {
            if (speed <= maxSpeed)
            {
                if (speed < 0.0F)
                    speed += 0.05F;
                else
                    speed += 0.01F;
            }
        }

        public void reverse()
        {
            if (speed >= -maxSpeed)
            {
                if (speed > 0.0F)
                    speed -= 0.05F;
                else
                    speed -= 0.01F;
            }
        }

        public void update()
        {
            if (speed >= 0.0F)
            {
                location.X += (float)Math.Sin(direction) * speed;
                location.Y -= (float)Math.Cos(direction) * speed;
            }
            else if (speed < 0.0F)
            {
                location.X -= (float)Math.Sin(direction) * -speed;
                location.Y += (float)Math.Cos(direction) * -speed;
            }
        }

        public void stop()
        {
            speed = 0.0F;
        }

        public void rotate(int rotation)
        {
            direction += rotationSpeed * rotation;

            if (direction <= ((float)Math.PI * -1.0F))
                direction = ((float)Math.PI);
            else if (direction > ((float)Math.PI))
                direction = ((float)Math.PI * -1.0F + rotationSpeed);
        }

        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)location.X, (int)location.Y, texture.Width, texture.Height), null, Color.White, direction, origin, SpriteEffects.None, 0.0f);
        }
    }
}
