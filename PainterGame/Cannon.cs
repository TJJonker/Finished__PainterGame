using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace PainterGame
{
    internal class Cannon
    {
        private Texture2D cannonBarrel, colorRed, colorGreen, colorBlue;
        private Vector2 barrelPosition, barrelOrigin, colorOrigin;
        private Color currentColor;
        private float angle;

        public Color Color
        {
            get { return currentColor; }
            set { if (value == Color.Red || value == Color.Green || value == Color.Blue) currentColor = value; }
        }

        public Vector2 Position { get { return barrelPosition; } }

        public Vector2 BallPosition
        {
            get
            {
                float opposite = (float)Math.Sin(angle) * cannonBarrel.Width * 0.75f;
                float adjacent = (float)Math.Cos(angle) * cannonBarrel.Width * 0.75f;
                return barrelPosition + new Vector2(adjacent, opposite);
            }
        }

        public Cannon(ContentManager Content)
        {
            cannonBarrel = Content.Load<Texture2D>("spr_cannon_barrel");
            colorRed = Content.Load<Texture2D>("spr_cannon_red");
            colorGreen = Content.Load<Texture2D>("spr_cannon_green");
            colorBlue = Content.Load<Texture2D>("spr_cannon_blue");

            colorOrigin = new Vector2(colorRed.Width, colorRed.Height) / 2;
            barrelPosition = new Vector2(72, 405);
            barrelOrigin = new Vector2(cannonBarrel.Height, cannonBarrel.Height) / 2;

            currentColor = Color.Blue;
        }

        public void Reset()
        {
            currentColor = Color.Blue;
            angle = 0.0f;
        }

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(cannonBarrel, barrelPosition, null, Color.White, angle, barrelOrigin, 1f, SpriteEffects.None, 0);

            // determine the sprite based on the current color
            Texture2D currentSprite;
            if (currentColor == Color.Red) currentSprite = colorRed;
            else if (currentColor == Color.Green) currentSprite = colorGreen;
            else currentSprite = colorBlue;

            // draw that sprite
            spriteBatch.Draw(currentSprite, barrelPosition, null, Color.White, 0f, colorOrigin, 1.0f, SpriteEffects.None, 0);
        }

        public void HandleInput(InputHelper inputHelper)
        {
            // Color selection depending on buttonPressed
            if (inputHelper.KeyPressed(Keys.R)) Color = Color.Red;
            else if (inputHelper.KeyPressed(Keys.G)) Color = Color.Green;
            else if (inputHelper.KeyPressed(Keys.B)) Color = Color.Blue;

            double opposite = inputHelper.MousePosition.Y - Position.Y;
            double adjacent = inputHelper.MousePosition.X - Position.X;
            angle = (float)Math.Atan2(opposite, adjacent);
        }
    }
}