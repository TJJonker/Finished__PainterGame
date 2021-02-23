using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;

namespace PainterGame
{
    internal class PaintCan : ThreeColorGameObject
    {
        private Color targetColor;
        private float minSpeed;
        SoundEffect soundPoint;

        public PaintCan(ContentManager Content, float positionOffset, Color target) : base(Content, "spr_can_red", "spr_can_green", "spr_can_blue")
        {
            soundPoint = Content.Load<SoundEffect>("snd_collect_points");
            targetColor = target;
            position = new Vector2(positionOffset, -origin.Y);
            minSpeed = 30f;
        }

        public override void Update(GameTime gameTime)
        {
            rotation = (float)Math.Sin(Position.Y / 50.0f) * 0.05f;

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            minSpeed += 0.01f * dt;

            base.Update(gameTime);

            if (velocity != Vector2.Zero)
            {
                // Checks if outside the world
                if (Painter.GameWorld.IsOutsideWorld(Position - origin))
                {
                    // If wrong color, lose a life
                    if (Color != targetColor) Painter.GameWorld.LoseLife();
                    else {
                        soundPoint.Play();
                        Painter.GameWorld.Score += 10;
                    }
                    Reset();
                }

                // Check for collision with the ball
                if (BoundingBox.Intersects(Painter.GameWorld.Ball.BoundingBox))
                {
                    Color = Painter.GameWorld.Ball.Color;
                    Painter.GameWorld.Ball.Reset();
                }
            }
            // Respawn at certain probability
            else if (Painter.Random.NextDouble() < 0.01f)
            {
                velocity = CalculateRandomVelocity();
                Color = CalculateRandomColor();
            }
        }

        public override void Reset()
        {
            base.Reset();
            velocity = Vector2.Zero;
            position.Y = -origin.Y;
        }

        private Vector2 CalculateRandomVelocity()
        {
            return new Vector2(0.0f, (float)Painter.Random.NextDouble() * 30 + minSpeed);
        }

        private Color CalculateRandomColor()
        {
            int randomVal = Painter.Random.Next(3);
            if (randomVal == 0) return Color.Red;
            else if (randomVal == 1) return Color.Green;
            else return Color.Blue;
        }

        public void ResetMinSpeed()
        {
            minSpeed = 30f;
        }
    }
}