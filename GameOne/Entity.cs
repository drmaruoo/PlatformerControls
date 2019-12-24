using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameOne
{
    internal class Entity
    {
        public int typeID;
        public string name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        private float speed = 16f;
        private float gravity = 0.4f;
        private float velocityX = 0f;
        private float velocityY = 0f;
        public Texture2D texture { get; set; }
        public bool thisTickVerticalCollision;

        public Entity(int typeID)
        {
            this.typeID = typeID;
        }
        public void setPosition(Vector2 position)
        {
            this.X = position.X;
            this.Y = position.Y;
        }
        public Vector2 GetVector()
        {
            return new Vector2(X, Y);
        }

        public void Move(int direction)
        {
            thisTickVerticalCollision = CollidingWithTopBot(Game1.graphicsH);
            GravityTick();
            velocityX += (direction * speed) / 10;
            if (velocityX > speed) { velocityX = speed; }
            if (velocityX < -speed) { velocityX = -speed; }
            if (!CollidingWithSide(Game1.graphicsW))
            {
                X += velocityX;
            }
            else
            {
                X -= velocityX;
                velocityX = 0;
            }
            if (thisTickVerticalCollision)
            {
                velocityX *= 0.7f;
            }
            else
            {
                velocityX *= 0.9f;
            }
        }

        private void GravityTick()
        {
            if (!thisTickVerticalCollision)
            {
                velocityY += gravity;
            }
            else
            {
                velocityY = 0;
            }
            Y += velocityY;
        }

        public void Jump()
        {
            if (thisTickVerticalCollision)
            {
                Y -= 2;
                velocityY = -10f;
            }
        }

        private bool CollidingWithSide(float w)
        {
            if (X > w - texture.Width / 2)
            {
                X -= 1;
                return true;
            }
            else if (X < texture.Width / 2)
            {
                X += 1;
                return true;
            }
            else { return false; }
        }
        private bool CollidingWithTopBot(float h)
        {
            if (Y > h - texture.Height / 2)
            {
                Y = h - texture.Height / 2;
                return true;
            }
            else if (Y < texture.Height / 2)
            {
                return true;
            }
            else { return false; }
        }
    }
}