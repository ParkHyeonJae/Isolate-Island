using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Utils
{
    public class Defines
    {
        public enum MouseEvent
        {
            Press,
            Click
        }


        #region LOAD
        public enum Load_Object
        {
            Player,
            돌맹이,
            Item_List
        }
        #endregion LOAD


        public enum EDressableState
        {
            Use,
            Drop
        }

        #region ANIMATION

        public class AnimationKeys
        {
            public static readonly string DefaultAttackAnimationKey = "Attack";
        }
        #endregion


        #region TAGS
        public class Tags
        {
            public static readonly string TAG_ENTITY = "Entity";
            public static readonly string TAG_PLAYER = "Player";
        }
        #endregion
    }
}