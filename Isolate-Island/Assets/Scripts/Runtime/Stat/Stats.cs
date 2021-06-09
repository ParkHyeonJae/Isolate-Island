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
        public int HP { get; set; }
        public int Hungry { get; set; }

        public int MoveSpeed;
        public int AttackSpeed;
    }

    [System.Serializable]
    public class EffectStat : Stat
    {
        #region Ability Stat

        public int ATK_ATK;
        public int ATK_Range;

        public int DEF_DEF;
        public EParts DEF_eParts;

        public int HEALTH_HP;
        public int HEALTH_Hungry;





        public int EFFECT_ATK;
        public int EFFECT_RANGE;
        public int EFFECT_DEF;

        public int BUFF_SLOT1;
        public int BUFF_SLOT2;
        public int BUFF_SLOT3;
        public int BUFF_SLOT4;

        #endregion
    }


    public class StatBuilder : IBuilder<Stat>
    {
        private Stat _stat = null;
        public Stat Stat { get => _stat = _stat ?? new Stat(); }

        public StatBuilder SetHP(int _hp)
        {
            Stat.HP = _hp;
            return this;
        }
        public StatBuilder SetHungry(int _hungry)
        {
            Stat.Hungry = _hungry;
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