using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PainterGame
{
    public class Painter : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private InputHelper inputHelper;
        private GameWorld gameWorld;


        public Painter()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            inputHelper = new InputHelper();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameWorld = new GameWorld(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            inputHelper.Update();
            gameWorld.HandleInput(inputHelper);
            gameWorld.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            gameWorld.Draw(gameTime, spriteBatch);
        }
    }
}