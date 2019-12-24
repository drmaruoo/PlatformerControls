using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameOne
{
    public class Game1 : Game
    {
        Entity player = new Entity(0);

        public static float graphicsW, graphicsH;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }
        protected override void Initialize()
        {
            graphicsW = graphics.PreferredBackBufferWidth;
            graphicsH = graphics.PreferredBackBufferHeight;
            player.setPosition(new Vector2(graphicsW / 2, graphicsH / 2));
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player.texture = Content.Load<Texture2D>("player");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            var kstate = Keyboard.GetState();
            float egt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            int direction;
            if (kstate.IsKeyDown(Keys.Left) && !kstate.IsKeyDown(Keys.Right)) { direction = -1; }
            else if (kstate.IsKeyDown(Keys.Right) && !kstate.IsKeyDown(Keys.Left)) { direction = 1; }
            else { direction = 0; }
            if (kstate.IsKeyDown(Keys.Space)) { player.Jump(); }

            player.Move(direction);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.FromNonPremultiplied(10, 15, 15, 255));

            spriteBatch.Begin();

            spriteBatch.Draw
                (
                player.texture,
                player.GetVector(),
                null,
                Color.White, 0f,
                new Vector2(player.texture.Width / 2, player.texture.Height / 2),
                Vector2.One, SpriteEffects.None,
                0f
                );
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
