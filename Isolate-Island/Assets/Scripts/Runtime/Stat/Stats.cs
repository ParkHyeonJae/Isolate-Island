using IsolateIsland.Runtime.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Stat
{
    [System.Serializable]
    public enum EParts
    {
        PARTS_NONE,
        PARTS_HEAD,
        PARTS_BODY,
        PARTS_LEG,
    }

    public enum EAbilityStatType
    {
        ATK,
        DEF,
        HEALTH,
        EFFECT,
        BUFF,
    }

    [System.Serializable]
    public class Stat
    {
        #region Ability Stat

        public int ATK_ATK;
        public int ATK_Range;

        public int DEF_DEF;
        public EParts DEF_eParts;

        public int HEALTH_HP;
        public int HEALTH_Hungry;


        public int MoveSpeed;
        public int AttackSpeed;

        public int EFFECT_ATK;
        public int EFFECT_RANGE;
        public int EFFECT_DEF;

        public string EFFECT_STR;

        public int BUFF_H;
        public int BUFF_H1;
        public int BUFF_H2;
        public int BUFF_H3;

        #endregion
    }

    public class IncreaseStat : Stat
    {

    }


    public class StatBuilder : IBuilder<Stat>
    {
        private Stat _stat = null;
        public Stat Stat { get => _stat = _stat ?? new Stat(); }

        public StatBuilder SetHP(int _hp)
        {
            Stat.HEALTH_HP = _hp;
            return this;
        }
        public StatBuilder SetHungry(int _hungry)
        {
            Stat.HEALTH_Hungry = _hungry;
            return this;
        }
        public StatBuilder SetMoveSpeed(int _moveSpeed)
        {
            Stat.MoveSpeed = _moveSpeed;
            return this;
        }
        public StatBuilder SetAttackSpeed(int _attackSpeed)
        {
            Stat.AttackSpeed = _attackSpeed;
            return this;
        }


        public Stat Build()
        {
            return Stat;
        }
    }


}