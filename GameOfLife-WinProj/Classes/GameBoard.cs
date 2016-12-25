using ConwaysGameOfLife;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace GameOfLife_WinProj.Classes
{
    /// <summary>
    /// This class serves as a visual representation of the gameboard,
    /// usingc a GameOfLife Object to hold the mechanics behind it.
    /// </summary>
    class GameBoard
    {
        public GameOfLife Game;
        public Color LiveColor;
        public Color DeadColor;
        public ArrayList screenShots;

        // time counted in seconds
        private float interval, timeSinceLastUpdate;

        public GameBoard(float interval)
        {
            LiveColor = Color.DarkSlateGray;
            DeadColor = Color.LightSlateGray;
            this.interval = interval;
            timeSinceLastUpdate = 0;
            screenShots = new ArrayList();
        }
        public void Update(GameTime gameTime)
        {
            timeSinceLastUpdate += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // if the allotted time has passed
            if(timeSinceLastUpdate > interval)
            {
                Game.UpdateGrid();
                timeSinceLastUpdate = 0;
            }
        }

        internal void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            for (int y = 0; y < Game.Height; y++)
            {
                for (int x = 0; x < Game.Width; x++)
                {
                    var cellTexture = new Texture2D(graphics.GraphicsDevice, 1, 1);
                    cellTexture.SetData(new Color[] { Color.White });
                    var cellRectangle = new Rectangle(x * Misc.GameSettings.CellWidth, y * Misc.GameSettings.CellHeight, Misc.GameSettings.CellWidth, Misc.GameSettings.CellHeight);
                    spriteBatch.Draw(cellTexture, cellRectangle, (Game.Grid[y, x] == 0) ? DeadColor : LiveColor);
                }
            }
        }

        internal void GetScreenShot(GraphicsDevice device)
        {
            Color[] screenData = new Color[device.PresentationParameters.BackBufferHeight *
                                           device.PresentationParameters.BackBufferWidth];

            RenderTarget2D screenShot = new RenderTarget2D(device,
                device.PresentationParameters.BackBufferWidth,
                device.PresentationParameters.BackBufferHeight);

            device.SetRenderTarget(screenShot);
        }
    }
}
