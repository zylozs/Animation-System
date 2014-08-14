using File_System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Animation_Player
{
    public class Animation
    {
        /********* DATA *********/
        [XmlIgnore]
        public Texture2D m_Texture;

        [XmlIgnore]
        public string m_DefaultTextureFilepath;

        //public string m_Name;
        public string m_Filepath;
        public string m_FullFilepath;

        // Default Values, don't touch!
        public int m_MsPerCel;
        public int m_MsUntilNextCel;
        public int m_CurrentCel;
        public bool m_IsLooping;
        public bool m_IsPaused;
        public Vector2 m_Scale;

        public List<Rectangle> m_Cels;

		#region Constructors

        /*  Function:  public Animation()
         *   Purpose:  Constructor
         ****************************************************************/
        public Animation()
        {
            m_Cels = new List<Rectangle>();
        }

        /*  Function:  public Animation(Texture2D texture, int currentCel, int msPerCel)
         *   Purpose:  Constructor
         ****************************************************************/
        public Animation(Texture2D texture, int currentCel, int msPerCel)
        {
            m_Cels = new List<Rectangle>();
            this.m_Filepath = texture.Name;
            this.m_Texture = texture;
            this.m_CurrentCel = currentCel;
            this.m_MsPerCel = msPerCel;
            this.m_MsUntilNextCel = m_MsPerCel;
            this.m_Scale = new Vector2(1f, 1f);
            this.m_IsLooping = true;
        }

        /*  Function:  public Animation(Texture2D texture, int currentCel, int msPerCel, bool loop)
         *   Purpose:  Constructor
         ****************************************************************/
        public Animation(Texture2D texture, int currentCel, int msPerCel, bool loop)
        {
            m_Cels = new List<Rectangle>();
            this.m_Filepath = texture.Name;
            this.m_Texture = texture;
            this.m_CurrentCel = currentCel;
            this.m_MsPerCel = msPerCel;
            this.m_MsUntilNextCel = m_MsPerCel;
            this.m_Scale = new Vector2(1f, 1f);
            this.m_IsLooping = loop;
        }

        /*  Function:  public Animation(Texture2D texture, int currentCel, int msPerCel, float scale, bool loop)
         *   Purpose:  Constructor
         ****************************************************************/
        public Animation(Texture2D texture, int currentCel, int msPerCel, float scale, bool loop)
        {
            m_Cels = new List<Rectangle>();
            this.m_Filepath = texture.Name;
            this.m_Texture = texture;
            this.m_CurrentCel = currentCel;
            this.m_MsPerCel = msPerCel;
            this.m_MsUntilNextCel = m_MsPerCel;
            this.m_Scale = new Vector2(scale, scale);
            this.m_IsLooping = loop;
        }

        public Animation(Texture2D texture, int currentCel, int msPerCel, Vector2 scale, bool loop)
        {
            m_Cels = new List<Rectangle>();
            this.m_Filepath = texture.Name;
            this.m_Texture = texture;
            this.m_CurrentCel = currentCel;
            this.m_MsPerCel = msPerCel;
            this.m_MsUntilNextCel = m_MsPerCel;
            this.m_Scale = scale;
            this.m_IsLooping = loop;
        }

		#endregion

        #region LoadContent

        public void LoadContent(ContentManager content, TextureLoader loader)
        {
            if (m_Filepath != m_FullFilepath)
            {
                try
                {
                    m_Texture = content.Load<Texture2D>(m_Filepath);
                }
                catch (System.Exception ex) // Everything goes to shit!!
                {
                    try
                    {
                        m_Texture = loader.FromFile(m_FullFilepath);
                    }
                    catch (System.Exception ex2)
                    {
                        MessageBox.Show("There was an error loading an Animation Texture at filepath: " + m_FullFilepath + "\nPlease make sure this filepath is correct. The game will use a default Texture.", "Texture Loading Error", MessageBoxButtons.OK);
                        m_Texture = content.Load<Texture2D>(m_DefaultTextureFilepath);
                    }
                }
            }
            else
            {
                try
                {
                    m_Texture = loader.FromFile(m_FullFilepath);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("There was an error loading an Animation Texture at filepath: " + m_FullFilepath + "\nPlease make sure this filepath is correct. The game will use a default Texture.", "Texture Loading Error", MessageBoxButtons.OK);
                    m_Texture = content.Load<Texture2D>(m_DefaultTextureFilepath);
                }
            }
        }

        #endregion

        #region Add Cel

        /*  Function:  public void AddCel(int x, int y, int width, int height)
         *   Purpose:  This will add a cel to the end of the list of cels
         ****************************************************************/
        public void AddCel(int x, int y, int width, int height)
        {
            m_Cels.Add(new Rectangle(x, y, width, height));
        }

        /*  Function:  public void AddCel(int x, int y, int width, int height, int index)
         *   Purpose:  This will add a cel at the specified index of the list of cels
         ****************************************************************/
        public void AddCel(int x, int y, int width, int height, int index)
        {
            m_Cels.Insert(index, new Rectangle(x, y, width, height));
        }

        #endregion

        #region Remove Cel

        /*  Function:  public void RemoveCel(int index)
         *   Purpose:  This will remove a cel from the specified index
         ****************************************************************/
        public void RemoveCel(int index)
        {
            m_Cels.RemoveAt(index);
        }

        #endregion

        #region Create Data

        public AnimationData CreateData()
        {
            AnimationData data = new AnimationData();
            
            // Animation Info
            data.m_MsPerCel = m_MsPerCel;
            data.m_CurrentCel = m_CurrentCel;
            data.m_IsLooping = m_IsLooping;
            data.m_IsPaused = m_IsPaused;

            // Drawing Info
            data.m_Scale = m_Scale;
            data.m_Position = Vector2.Zero;
            data.m_Color = Color.White;
            data.m_Rotation = 0f;
            data.m_SpriteEffects = SpriteEffects.None;
            data.m_Depth = 0f;

            return data;
        }

        #endregion

        #region Draw
        /*  Function:  public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position)
         *   Purpose:  This will draw the animation to the screen
         ****************************************************************/
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(m_Texture, position, m_Cels[m_CurrentCel], Color.White, 0.0f,
                             new Vector2(m_Cels[m_CurrentCel].Width / 2, m_Cels[m_CurrentCel].Height / 2), 
                             m_Scale, SpriteEffects.None, 0.0f);
            spriteBatch.End();
        }

        /*  Function:  public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Color color)
         *   Purpose:  This will draw the animation to the screen
         ****************************************************************/
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(m_Texture, position, m_Cels[m_CurrentCel], color, 0.0f,
                             new Vector2(m_Cels[m_CurrentCel].Width / 2, m_Cels[m_CurrentCel].Height / 2),
                             m_Scale, SpriteEffects.None, 0.0f);
            spriteBatch.End();
        }

        /*  Function:  public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Color color, float rotation)
         *   Purpose:  This will draw the animation to the screen
         ****************************************************************/
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Color color, float rotation)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(m_Texture, position, m_Cels[m_CurrentCel], color, rotation,
                             new Vector2(m_Cels[m_CurrentCel].Width / 2, m_Cels[m_CurrentCel].Height / 2),
                             m_Scale, SpriteEffects.None, 0.0f);
            spriteBatch.End();
        }

        /*  Function:  public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, SpriteEffects spriteEffects)
         *   Purpose:  This will draw the animation to the screen
         ****************************************************************/
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, SpriteEffects spriteEffects)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(m_Texture, position, m_Cels[m_CurrentCel], color, rotation,
                             new Vector2(m_Cels[m_CurrentCel].Width / 2, m_Cels[m_CurrentCel].Height / 2),
                             m_Scale, spriteEffects, 0.0f);
            spriteBatch.End();
        }

        /*  Function:  public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, SpriteEffects spriteEffects, float depth)
         *   Purpose:  This will draw the animation to the screen
         ****************************************************************/
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, SpriteEffects spriteEffects, float depth)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(m_Texture, position, m_Cels[m_CurrentCel], color, rotation,
                             new Vector2(m_Cels[m_CurrentCel].Width / 2, m_Cels[m_CurrentCel].Height / 2),
                             m_Scale, spriteEffects, depth);
            spriteBatch.End();
        }

        /*  Function:  public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, float depth)
         *   Purpose:  This will draw the animation to the screen
         ****************************************************************/
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, float depth)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(m_Texture, position, m_Cels[m_CurrentCel], Color.White, 0.0f,
                             new Vector2(m_Cels[m_CurrentCel].Width / 2, m_Cels[m_CurrentCel].Height / 2),
                             m_Scale, SpriteEffects.None, depth);
            spriteBatch.End();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, int currentCel, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects spriteEffects, float depth)
        {
            if (origin == Vector2.Zero)
            {
                spriteBatch.Draw(m_Texture, position, m_Cels[currentCel], color, rotation,
                                 new Vector2(m_Cels[currentCel].Width / 2, m_Cels[currentCel].Height / 2),
                                 scale, spriteEffects, depth);
            }
            else
            {
                spriteBatch.Draw(m_Texture, position, m_Cels[currentCel], color, rotation, origin, scale, spriteEffects, depth);
            }
        }

		#endregion
    }
}