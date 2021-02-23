using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PainterGame
{
    internal class ThreeColorGameObject
    {
        private Texture2D colorRed, colorGreen, colorBlue;
        private Color color;
        private Vector2 position, origin, velocity;
        private float rotation;

        public Color Color
        {
            get { return color; }
            set { if (value == Color.Red || value == Color.Green || value == Color.Blue) color = value; }
        }

        public Vector2 Position { get { return position; } }

        public Rectangle BoundingBox
        {
            get
            {
                Rectangle spriteBounds = colorRed.Bounds;
                spriteBounds.Offset(position - origin);
                return spriteBounds;
            }
        }

        public void Reset()
        {
            Color = Color.Blue;
        }

        public void HandleInput(InputHelper inputHelper)
        {
        }

        public void Update(GameTime gameTime)
        {
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Texture2D currentSprite;
            if (Color == Color.Red) currentSprite = colorRed;
            else if (Color == Color.Green) currentSprite = colorGreen;
            else currentSprite = colorBlue;
            spriteBatch.Draw(currentSprite, position, null, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}