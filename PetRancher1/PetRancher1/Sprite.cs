using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PetRancher1
{
    class Sprite
    {

        public int rows;
        public int columns;
        public int frameWidth;
        public int frameHeight;
        public Point index;

        public int spriteDirection = 1;


        public Vector2 Position = new Vector2(0, 0);

        private Texture2D mSpriteTexture;

        public void LoadContent(ContentManager theContentManager, string theAssetName,
                                int rows, int columns, int frameWidth, int frameHeight,
                                Point index)
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);

            this.rows = rows;
            this.columns = columns;
            this.frameHeight = frameHeight;
            this.frameWidth = frameWidth;
            this.index = index;

        }

        public void Draw(SpriteBatch theSpriteBatch)
        {



            theSpriteBatch.Draw(mSpriteTexture, Vector2.Zero,
                new Rectangle((index.X * frameWidth),
                              (index.Y * frameHeight),
                              frameWidth,
                              frameHeight), 
                Color.White,0, Vector2.Zero, 
                3, SpriteEffects.None, 0);
        }


        public void GoToNextFrame()
        {
            /*this.index.X++;
            if (this.index.X >= this.columns )
            {
                this.index.X = 0;
                this.index.Y++;
                if (this.index.Y >= this.rows)
                    this.index.Y = 0;
            }*/ 

            this.index.X += this.spriteDirection;
            if (this.index.X >= this.rows -2 )
            {
                this.spriteDirection = -1;
            }

            else if (this.index.X <= 0)
            {
                this.spriteDirection = 1;
            }

            /*if (this.index.X == 0)
            {
                this.index.X = 2;
            }

            else { this.index.X = 0; }*/
        }
    }
}
