using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PainterGame
{
    internal class Ball
    {
        private Vector2 position, origin;
        private Texture2D colorRed, colorGreen, colorBlue;
        private Color color;

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
            color = Color.Blue;
        }

        public void HandleInput(InputHelper inputHelper)
        {
        }

        public void Update(GameTime gametime)
        {
        }

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            Texture2D currentSprite;
            if (color == Color.Red) currentSprite = colorRed;
            else if (color == Color.Green) currentSprite = colorGreen;
            else currentSprite = colorBlue;
            spriteBatch.Draw(currentSprite, position, null, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}