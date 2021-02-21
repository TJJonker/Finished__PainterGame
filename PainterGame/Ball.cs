using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PainterGame
{
    internal class Ball
    {
        private Vector2 position, origin, velocity;
        private Texture2D colorRed, colorGreen, colorBlue;
        private Color color;
        private bool shooting;

        public Color Color
        {
            get { return color; }
            set { if (value == Color.Red || value == Color.Green || value == Color.Blue) color = value; }
        }

        public Vector2 Position { get { return position; } }

        public Ball(ContentManager Content)
        {
            colorRed = Content.Load<Texture2D>("spr_ball_red");
            colorGreen = Content.Load<Texture2D>("spr_ball_green");
            colorBlue = Content.Load<Texture2D>("spr_ball_blue");
            origin = new Vector2(colorRed.Width / 2, colorRed.Height / 2);
            Reset();
        }

        public void Reset()
        {
            position = new Vector2(65, 390);
            Color = Color.Blue;
            shooting = false;
            velocity = Vector2.Zero;
        }

        public void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.MouseLeftButtonPressed() && !shooting)
            {
                shooting = true;
                velocity = (inputHelper.MousePosition - Painter.GameWorld.Cannon.Position) * 1.2f;
            }
        }

        public void Update(GameTime gametime)
        {
            if (shooting)
            {
                float dt = (float)gametime.ElapsedGameTime.TotalSeconds;
                position += velocity * dt;
                velocity.Y += 400.0f * dt;
            }
            else
            {
                Color = Painter.GameWorld.Cannon.Color;
                position = Painter.GameWorld.Cannon.BallPosition;
            }
        }

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            Texture2D currentSprite;
            if (Color == Color.Red) currentSprite = colorRed;
            else if (Color == Color.Green) currentSprite = colorGreen;
            else currentSprite = colorBlue;
            spriteBatch.Draw(currentSprite, position, null, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}