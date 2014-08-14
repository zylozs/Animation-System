using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Animation_Player
{
    public class AnimationPlayer
    {
        /********* EVENT *********/
        public delegate void AnimationCompleteDelegate(string name);
        public event AnimationCompleteDelegate AnimationCompleteEvent;

        /********* DATA *********/
        public List<string> m_Names;
        public List<Animation> m_Animations;
        public List<AnimationData> m_Datas;

		#region Constructors
        /*  Function:  public AnimationPlayer()
         *   Purpose:  Constructor
         ****************************************************************/
        public AnimationPlayer()
        {
            m_Animations = new List<Animation>();
            m_Datas = new List<AnimationData>();
            m_Names = new List<string>();
        }
		#endregion

		#region PlayAnimation

        public void PlayAnimation(string name, Animation animation)
        {
            int index = 0;

            if (!IsNameExist(name, ref index)) // If it doesn't exist, it will add it to the end
            {
                m_Animations.Add(animation);
                m_Datas.Add(animation.CreateData());
                m_Names.Add(name);
            }
            else // However, if it does exist, it will replace it
            {
                m_Animations[index] = animation;
                m_Datas[index] = animation.CreateData();
                m_Names[index] = name;
            }
        }

        public void PlayAnimation(string name, Animation animation, AnimationData data)
        {
            int index = 0;

            if (!IsNameExist(name, ref index)) // If it doesn't exist, it will add it to the end
            {
                m_Animations.Add(animation);
                m_Datas.Add(data);
                m_Names.Add(name);
            }
            else // However, if it does exist, it will replace it
            {
                m_Animations[index] = animation;
                m_Datas[index] = data;
                m_Names[index] = name;
            }
        }

		#endregion

        #region StopAnimation

        public void StopAnimation(string name)
        {
            int index = 0;

            if (IsNameExist(name, ref index))
            {
                m_Names.RemoveAt(index);
                m_Animations.RemoveAt(index);
                m_Datas.RemoveAt(index);
            }
        }

        #endregion

        #region StopAllAnimations

        public void StopAllAnimations()
        {
            m_Names.Clear();
            m_Animations.Clear();
            m_Datas.Clear();
        }

        #endregion

        #region PauseAnimation

        public void PauseAnimation(string name)
        {
            int index = 0;

            if (IsNameExist(name, ref index))
            {
//                 AnimationData data = m_Datas[index];
//                 data.m_IsPaused = false;
                m_Datas[index].m_IsPaused = false;
            }
        }

        #endregion

        #region UnPauseAnimation

        public void UnPauseAnimation(string name)
        {
            int index = 0;

            if (IsNameExist(name, ref index))
            {
//                 AnimationData data = m_Datas[index];
//                 data.m_IsPaused = false;
                m_Datas[index].m_IsPaused = false;
            }
        }

        #endregion

        #region Update
        /*  Function:  public void Update(GameTime gameTime)
         *   Purpose:  This will update the animation. It will move through the animation cels as defined by the
         *             variable msUntilNextCel. It will also trigger an event when an animation that doesn't loop ends
         ****************************************************************/
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < m_Animations.Count; i++)
            {
//                 Animation anim = m_Animations[i];
//                 AnimationData data = m_Datas[i];
//                 string name = m_Names[i];

                if (!m_Datas[i].m_IsPaused)
                {
                    m_Datas[i].m_MsUntilNextCel -= gameTime.ElapsedGameTime.Milliseconds;

                    if (m_Datas[i].m_MsUntilNextCel <= 0)
                    {
                        m_Datas[i].m_CurrentCel++;
                        m_Datas[i].m_MsUntilNextCel += m_Datas[i].m_MsPerCel;
                    }

                    if (m_Datas[i].m_CurrentCel >= m_Animations[i].m_Cels.Count && m_Datas[i].m_IsLooping)
                        m_Datas[i].m_CurrentCel = 0;
                    else if (m_Datas[i].m_CurrentCel >= m_Animations[i].m_Cels.Count && !m_Datas[i].m_IsLooping)
                    {
                        m_Datas[i].m_CurrentCel = m_Animations[i].m_Cels.Count - 1;
                        AnimationCompleteEvent(m_Names[i]);
                    }
                }

//                 m_Animations[i] = anim;
//                 m_Datas[i] = data;
//                 m_Names[i] = name;
            } 
        }
		#endregion

        #region Name Exists

        private bool IsNameExist(string name, ref int index)
        {
            index = -1;

            for (int i = 0; i < m_Names.Count; i++)
            {
                if (m_Names[i] == name)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
                return false;
            else
                return true;
        }

        #endregion

        #region Update Data

        public void UpdateData(string name, AnimationData data)
        {
            int index = 0;

            if (IsNameExist(name, ref index))
            {
                m_Datas[index] = data;
            }
        }

        public void UpdateData(string name, Vector2 position, Color color)
        {
            int index = 0;

            if (IsNameExist(name, ref index))
            {
//                 AnimationData data = m_Datas[index];
//                 data.m_Position = position;
//                 data.m_Color = color;
// 
//                 m_Datas[index] = data;
                m_Datas[index].m_Position = position;
                m_Datas[index].m_Color = color;
            }
        }

        public void UpdateData(string name, Vector2 position, Color color, float rotation)
        {
            int index = 0;

            if (IsNameExist(name, ref index))
            {
//                 AnimationData data = m_Datas[index];
//                 data.m_Position = position;
//                 data.m_Color = color;
//                 data.m_Rotation = rotation;
// 
//                 m_Datas[index] = data;
                m_Datas[index].m_Position = position;
                m_Datas[index].m_Color = color;
                m_Datas[index].m_Rotation = rotation;
            }
        }

        public void UpdateData(string name, Vector2 position, Color color, float rotation, Vector2 scale)
        {
            int index = 0;

            if (IsNameExist(name, ref index))
            {
//                 AnimationData data = m_Datas[index];
//                 data.m_Position = position;
//                 data.m_Color = color;
//                 data.m_Rotation = rotation;
//                 data.m_Scale = scale;
// 
//                 m_Datas[index] = data;
                m_Datas[index].m_Position = position;
                m_Datas[index].m_Color = color;
                m_Datas[index].m_Rotation = rotation;
                m_Datas[index].m_Scale = scale;
            }
        }

        public void UpdateData(string name, Vector2 position, Color color, float rotation, Vector2 scale, SpriteEffects spriteEffects, float depth)
        {
            int index = 0;

            if (IsNameExist(name, ref index))
            {
//                 AnimationData data = m_Datas[index];
//                 data.m_Position = position;
//                 data.m_Color = color;
//                 data.m_Rotation = rotation;
//                 data.m_Scale = scale;
//                 data.m_SpriteEffects = spriteEffects;
//                 data.m_Depth = depth;
// 
//                 m_Datas[index] = data;
                m_Datas[index].m_Position = position;
                m_Datas[index].m_Color = color;
                m_Datas[index].m_Rotation = rotation;
                m_Datas[index].m_Scale = scale;
                m_Datas[index].m_SpriteEffects = spriteEffects;
                m_Datas[index].m_Depth = depth;
            }
        }

        public void UpdateData(string name, Vector2 position, Vector2 scale)
        {
            int index = 0;

            if (IsNameExist(name, ref index))
            {
//                 AnimationData data = m_Datas[index];
//                 data.m_Position = position;
//                 data.m_Scale = scale;
// 
//                 m_Datas[index] = data;
                m_Datas[index].m_Position = position;
                m_Datas[index].m_Scale = scale;
            }
        }

        #endregion

        #region Draw
        /*  Function:  public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position)
         *   Purpose:  This will draw the animation to the screen
         ****************************************************************/
		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            for (int i = 0; i < m_Animations.Count; i++)
            {
                m_Animations[i].Draw(gameTime, spriteBatch, m_Datas[i].m_CurrentCel, m_Datas[i].m_Position, m_Datas[i].m_Color, m_Datas[i].m_Rotation, m_Datas[i].m_Origin, m_Datas[i].m_Scale, m_Datas[i].m_SpriteEffects, m_Datas[i].m_Depth);
            }
            spriteBatch.End();
        }

        /*  Function:  public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Color color)
         *   Purpose:  This will draw the animation to the screen
         ****************************************************************/
        /*public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            for (int i = 0; i < m_Animations.Count; i++)
                m_Animations[i].Draw(gameTime, spriteBatch, position, color);
        }*/

        /*  Function:  public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Color color, float rotation)
         *   Purpose:  This will draw the animation to the screen
         ****************************************************************/
        /*public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Color color, float rotation)
        {
            for (int i = 0; i < m_Animations.Count; i++)
                m_Animations[i].Draw(gameTime, spriteBatch, position, color, rotation);
        }*/

        /*  Function:  public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, SpriteEffects spriteEffects)
         *   Purpose:  This will draw the animation to the screen
         ****************************************************************/
        /*public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, SpriteEffects spriteEffects)
        {
            for (int i = 0; i < m_Animations.Count; i++)
                m_Animations[i].Draw(gameTime, spriteBatch, position, color, rotation, spriteEffects);
        }*/

        /*  Function:  public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, SpriteEffects spriteEffects, float depth)
         *   Purpose:  This will draw the animation to the screen
         ****************************************************************/
        /*public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, SpriteEffects spriteEffects, float depth)
        {
            for (int i = 0; i < m_Animations.Count; i++)
                m_Animations[i].Draw(gameTime, spriteBatch, position, color, rotation, spriteEffects, depth);
        }*/

        /*  Function:  public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, float depth)
         *   Purpose:  This will draw the animation to the screen
         ****************************************************************/
        /*public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, float depth)
        {
            for (int i = 0; i < m_Animations.Count; i++)
                m_Animations[i].Draw(gameTime, spriteBatch, position, depth);
        }*/
		#endregion
    }
}