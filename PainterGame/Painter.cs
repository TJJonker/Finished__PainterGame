using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PainterGame
{
    internal class Painter : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private InputHelper inputHelper;
        static GameWorld gameWorld;
        public static Vector2 ScreenSize { get; private set; }

        public static GameWorld GameWorld { get { return gameWorld; } }

        public static Random Random { get; private set; }

        public Painter()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            inputHelper = new InputHelper();
            Random = new Random();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameWorld = new GameWorld(Content);
            ScreenSize = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
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