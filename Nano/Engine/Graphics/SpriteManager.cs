using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Nano.Engine.Graphics.Sprites;
using Microsoft.Xna.Framework.Content;
using System.Text.RegularExpressions;

namespace Nano.Engine.Graphics
{
    public class SpriteManager : ISpriteManager
    {
        Dictionary<ITexture2D,Texture2D> m_TextureDict = new Dictionary<ITexture2D, Texture2D>();
        Dictionary<ISprite,Texture2D> m_SpriteTextureDict = new Dictionary<ISprite, Texture2D>();

        SpriteBatch m_SpriteBatch;
        ContentManager m_ContentManager;

        #region Create Sprites/textures

        public ISprite CreateSprite(string texture)
        {
            Texture2D tex2D = m_ContentManager.Load<Texture2D>(texture);
            ITexture2D impl = CreateTextureImpl(tex2D);
            Register(impl,tex2D);

            Rectangle sourceRectangle = new Rectangle(0, 0, tex2D.Width, tex2D.Height);

            BasicSprite sprite = new BasicSprite(impl, sourceRectangle);
            sprite.SpriteManager = this;
            Register(sprite, tex2D);

            return sprite;
        }

        public ISprite CreateSprite(string texture, Rectangle sourceRectangle)
        {
            Texture2D tex2D = m_ContentManager.Load<Texture2D>(texture);
            ITexture2D impl = CreateTextureImpl(tex2D);
            Register(impl,tex2D);

            BasicSprite sprite = new BasicSprite(impl, sourceRectangle);
            sprite.SpriteManager = this;
            Register(sprite, tex2D);

            return sprite;
        }

        public ISprite CreateSprite(ITexture2D tex2D, Rectangle sourceRectangle)
        {
            BasicSprite sprite = new BasicSprite(tex2D, sourceRectangle);
            sprite.SpriteManager = this;
            Register(sprite, Resolve(tex2D));

            return sprite;
        }

        public ITexture2D CreateTexture2D(string texture)
        {
            Texture2D tex2D = m_ContentManager.Load<Texture2D>(texture);
            ITexture2D impl = CreateTextureImpl(tex2D);
            Register(impl,tex2D);

            return impl;
        }

        #endregion

        public SpriteManager(ContentManager contentManager, SpriteBatch spriteBatch)
        {
            m_ContentManager = contentManager;
            m_SpriteBatch = spriteBatch;
        }

        private void Register(ITexture2D texInterface, Texture2D texture)
        {
            m_TextureDict.Add(texInterface, texture);    
        }

        private void Register(ISprite sprite, Texture2D texture)
        {
            m_SpriteTextureDict.Add(sprite, texture);
        }

        private Texture2D Resolve(ITexture2D texture)
        {
            if (m_TextureDict.ContainsKey(texture))
                return m_TextureDict[texture];
            else
                throw new KeyNotFoundException("Requested ITexture2D Texture2D not available in SpriteManager");
        }

        private Texture2D Resolve(ISprite sprite)
        {
            if (m_SpriteTextureDict.ContainsKey(sprite))
                return m_SpriteTextureDict[sprite];
            else
                throw new KeyNotFoundException("Requested ISprite Texture2D not available in SpriteManager");
        }

        private ITexture2D CreateTextureImpl(Texture2D texture)
        {
            return new Texture2DImpl(texture.Width, texture.Height);
        }

        public void StartBatch()
        {
            m_SpriteBatch.Begin();   
        }

        public void EndBatch()
        {
            m_SpriteBatch.End();
        }
           
        /// <summary>
        /// Draws the sprite.
        /// Must be preceeded by a call to StartBatch()
        /// </summary>
        /// <param name="sprite">Sprite.</param>
        public void DrawSprite(ISprite sprite)
        {
            Texture2D tex = Resolve(sprite); //TODO consider moving this resolve to the startbatch call i.e. StartBatch(ITexture2D) need to profile this

            m_SpriteBatch.Draw(tex, sprite.Position, sprite.SourceRectangle, 
                Color.White, sprite.Rotation, sprite.RotationOrigin, 1.0f, SpriteEffects.None, 1);
        }

        public void DrawTexture2D(ITexture2D texture, Vector2 position, Rectangle sourceRectangle, float rotation, Vector2 origin)
        {
            Texture2D tex = Resolve(texture);//TODO consider moving this resolve to the startbatch call i.e. StartBatch(ITexture2D) need to profile this

            m_SpriteBatch.Draw(tex, position, sourceRectangle, 
                Color.White, rotation, origin, 1.0f, SpriteEffects.None, 1);
        }

        public void DrawTexture2D(ITexture2D texture, Rectangle destinationRectangle, Rectangle sourceRectangle)
        {
            Texture2D tex = Resolve(texture); //TODO consider moving this resolve to the startbatch call i.e. StartBatch(ITexture2D) need to profile this
            
            m_SpriteBatch.Draw(tex, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}

