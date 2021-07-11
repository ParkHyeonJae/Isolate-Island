using IsolateIsland.Runtime.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace IsolateIsland.Runtime.Stat
{
    [System.Serializable]
    public enum EParts
    {
        PARTS_NONE,
        PARTS_HEAD,
        PARTS_LEFT_HAND,
        PARTS_RIGHT_HAND,
        PARTS_BODY,
        PARTS_LEG,
    }

    public enum EAbilityStatType
    {
        NONE,
        //ATK,
        //DEF,
        //HEALTH,
        EFFECT,
        //BUFF,
    }

    public enum EAbilityDressableType
    {
        DRESSABLE,
    }


    [System.Serializable]
    public class Stat
    {
        public int MAX_HP { get; set; }
        public int HP { get; set; }
        public int Hungry { get; set; }

        public int ATK { get; set; }
        public int DEF { get; set; }
        public int RANGE { get; set; }

        public int MoveSpeed;
        public int AttackSpeed;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"MAX HP : {MAX_HP}\n");
            sb.Append($"HP : {HP}\n");
            sb.Append($"HUNGRY : {Hungry}\n");
            sb.Append($"ATK : {ATK}\n");
            sb.Append($"DEF : {DEF}\n");
            sb.Append($"RANGE : {RANGE}\n");

            return sb.ToString();
        }
    }

    [System.Serializable]
    public class EffectStat : Stat
    {

        public void ApplyEffect(ref Stat stat)
        {
            stat.MAX_HP += EFFECT_MAX_HEALTH;
            stat.HP += EFFECT_HEALTH;
            stat.Hungry += EFFECT_HUNGRY;
            stat.ATK += EFFECT_ATK;
            stat.DEF += EFFECT_DEF;
            stat.RANGE += EFFECT_RANGE;
        }

        public bool ExistEffectStat => (EFFECT_ATK | EFFECT_RANGE | EFFECT_DEF | EFFECT_MAX_HEALTH | EFFECT_HEALTH | EFFECT_HUNGRY) != 0;
        public bool IsConsumable => ExistEffectStat;
        #region Ability Stat

        //public int ATK_ATK;
        //public int ATK_Range;

        //public int DEF_DEF;
        //public EParts DEF_eParts;

        //public int HEALTH_HP;
        //public int HEALTH_Hungry;




        // 소비탬 처럼 일회성으로 적용될 효과들
        public int EFFECT_ATK;
        public int EFFECT_RANGE;
        public int EFFECT_DEF;
        public int EFFECT_MAX_HEALTH;
        public int EFFECT_HEALTH;
        public int EFFECT_HUNGRY;

        //public int BUFF_SLOT1;
        //public int BUFF_SLOT2;
        //public int BUFF_SLOT3;
        //public int BUFF_SLOT4;

        #endregion
    }

    [System.Serializable]
    public class DressableStat : EffectStat
    {

        // 인번토리 장비와 같이 착용하고 있을 때 영구적으로 적용될 효과들
        public EParts DRESSABLE_Parts;

        public int DRESSABLE_ATK;
        public int DRESSABLE_RANGE;
        public int DRESSABLE_DEF;
        public int DRESSABLE_MAX_HEALTH;
        public int DRESSABLE_HEALTH;
        public int DRESSABLE_HUNGRY;

        public void ApplyDressable(ref Stat stat)
        {
            stat.MAX_HP += DRESSABLE_MAX_HEALTH;
            stat.HP += DRESSABLE_HEALTH;
            stat.Hungry += DRESSABLE_HUNGRY;
            stat.ATK += DRESSABLE_ATK;
            stat.DEF += DRESSABLE_DEF;
            stat.RANGE += DRESSABLE_RANGE;
        }
        public void DeApplyDressable(ref Stat stat)
        {
            stat.MAX_HP -= DRESSABLE_MAX_HEALTH;
            stat.HP -= DRESSABLE_HEALTH;
            stat.Hungry -= DRESSABLE_HUNGRY;
            stat.ATK -= DRESSABLE_ATK;
            stat.DEF -= DRESSABLE_DEF;
            stat.RANGE -= DRESSABLE_RANGE;
        }

    }


    public class StatBuilder : IBuilder<Stat>
    {
        private Stat _stat = null;
        public Stat Stat { get => _stat = _stat ?? new Stat(); }

        public StatBuilder SetMAXHP(int _maxhp)
        {
            Stat.MAX_HP = _maxhp;
            return this;
        }
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
        public StatBuilder SetAttack(int _attack)
        {
            Stat.ATK = _attack;
            return this;
        }

        public StatBuilder SetAttackRange(int _range)
        {
            Stat.RANGE = _range;
            return this;
        }


        public Stat Build()
        {
            return Stat;
        }
    }


}