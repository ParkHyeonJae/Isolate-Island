﻿using System.Collections;
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
            User,
            돌맹이,
            Item_List,
            ItemParent
        }
        #endregion LOAD


        public enum EDressableState
        {
            Use,
            Drop
        }

        public enum EAttackAnimationKeyState : byte
        {
            OnEnter,
            OnExit
        }

        #region ANIMATION

        public class AnimationKeys
        {
            public static readonly string DefaultAttackAnimationKey = "Attack";
            public static readonly string RangedAttackAnimationKey = "BowAttack";
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