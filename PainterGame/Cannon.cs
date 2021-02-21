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

        public Cannon(ContentManager Content)
        {
            cannonBarrel = Content.Load<Texture2D>("spr_cannon_barrel");
            colorRed = Content.Load<Texture2D>("spr_cannon_red");
            colorGreen = Content.Load<Texture2D>("spr_cannon_green");
            colorBlue = Content.Load<Texture2D>("spr_cannon_blue");

            colorOrigin = new Vector2(colorRed.Width, colorRed.Height) / 2;
            barrelPosition = new Vector2(72, 405);
            barrelOrigin = new Vector2(cannonBarrel.Width, cannonBarrel.Height) / 2;

            currentColor = Color.Blue;
        }

        public void Reset()
        {
            currentColor = Color.Blue;
            angle = 0.0f;
        }

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(cannonBarrel, barrelPosition, null, Color.White, angle, barrelOrigin, 1.0f, SpriteEffects.None, 0);
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