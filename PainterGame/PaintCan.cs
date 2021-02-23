using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PainterGame
{
    internal class PaintCan : ThreeColorGameObject
    {
        private Color targetColor;
        private float minSpeed;

        public PaintCan(ContentManager Content, float positionOffset, Color target) : base(Content, "spr_can_red", "spr_can_green", "spr_can_blue")
        {
            targetColor = target;
            position = new Vector2(positionOffset, -origin.Y);
            minSpeed = 30f;
        }

        public override void Update(GameTime gameTime)
        {
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