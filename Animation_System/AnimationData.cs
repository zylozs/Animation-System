using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Animation_Player
{
    public class AnimationData
    {
        // Animation Info
        public int m_MsPerCel;
        public int m_MsUntilNextCel;
        public int m_CurrentCel;
        public bool m_IsLooping;
        public bool m_IsPaused;

        // Drawing Info
        public Vector2 m_Position;
        public Vector2 m_Origin = Vector2.Zero;
        public Color m_Color;
        public float m_Rotation;
        public Vector2 m_Scale;
        public SpriteEffects m_SpriteEffects;
        public float m_Depth;
    }
}
