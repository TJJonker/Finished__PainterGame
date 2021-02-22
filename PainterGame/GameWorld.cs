using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PainterGame
{
    internal class GameWorld
    {
        private Cannon cannon;
        private Texture2D background, life;
        private Ball ball;
        private PaintCan can1, can2, can3;
        private int lives;

        public Cannon Cannon { get { return cannon; } }
        public Ball Ball { get { return ball; } }

        public GameWorld(ContentManager Content)
        {
            background = Content.Load<Texture2D>("spr_background");
            life = Content.Load<Texture2D>("spr_lives");
            cannon = new Cannon(Content);
            ball = new Ball(Content);
            can1 = new PaintCan(Content, 480.0f, Color.Red);
            can2 = new PaintCan(Content, 610.0f, Color.Green);
            can3 = new PaintCan(Content, 740.0f, Color.Blue);
            lives = 5;
        }

        public void HandleInput(InputHelper inputHelper)
        {
            cannon.HandleInput(inputHelper);
            ball.HandleInput(inputHelper);
        }

        public void Update(GameTime gameTime)
        {
            ball.Update(gameTime);
            can1.Update(gameTime);
            can2.Update(gameTime);
            can3.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            ball.Draw(gameTime, spriteBatch);
            cannon.Draw(gameTime, spriteBatch);
            can1.Draw(gameTime, spriteBatch);
            can2.Draw(gameTime, spriteBatch);
            can3.Draw(gameTime, spriteBatch);
            for (int i = 0; i < lives; i++)
            {
                spriteBatch.Draw(life, new Vector2(i * life.Width + 15, 20), Color.White);
            }
            spriteBatch.End();
        }

        public bool IsOutsideWorld(Vector2 position)
        {
            return position.X < 0 || position.X > Painter.ScreenSize.X
                || position.Y > Painter.ScreenSize.Y;
        }

        public void LoseLife()
        {
            lives--;
        }
    }
}