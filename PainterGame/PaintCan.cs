using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PainterGame
{
    internal class PaintCan
    {
        private Texture2D colorRed, colorGreen, colorBlue;
        private Vector2 position, origin, velocity;
        private Color color, targetColor;
        private float minSpeed;

        public Color Color
        {
            get { return color; }
            set { if (value == Color.Red || value == Color.Green || value == Color.Blue) color = value; }
        }

        public Vector2 Position { get { return position; } }

        public PaintCan(ContentManager Content, float positionOffset, Color target)
        {
            colorRed = Content.Load<Texture2D>("spr_can_red");
            colorGreen = Content.Load<Texture2D>("spr_can_green");
            colorBlue = Content.Load<Texture2D>("spr_can_blue");
            origin = new Vector2(colorRed.Width, colorRed.Height) / 2;
            targetColor = target;
            position = new Vector2(positionOffset, -origin.Y);
            minSpeed = 30f;
            Reset();
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            minSpeed += 0.01f * dt;

            if (velocity != Vector2.Zero)
            {
                position += velocity * dt;
                if (Painter.GameWorld.IsOutsideWorld(Position - origin)) Reset();
            }
            else
            {
                velocity = CalculateRandomVelocity();
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Texture2D currentSprite;
            if (Color == Color.Red) currentSprite = colorRed;
            else if (Color == Color.Green) currentSprite = colorGreen;
            else currentSprite = colorBlue;
            spriteBatch.Draw(currentSprite, position, null, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        }

        public void Reset()
        {
            Color = Color.Blue;
            velocity = Vector2.Zero;
            position.Y = -origin.Y;
        }
        
        private Vector2 CalculateRandomVelocity()
        {
            return new Vector2(0.0f, (float)Painter.Random.NextDouble() * 30 + minSpeed);
        }
    }
}