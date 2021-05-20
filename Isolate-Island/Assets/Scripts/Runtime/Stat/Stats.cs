using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Stat
{

    public class Stat
    {
        public int HP { get; set; }
        public int Hungry { get; set; }


        public int MoveSpeed { get; set; }
        public int AttackSpeed { get; set; }

    }

    public class IncreaseStat : Stat
    {

    }


    public class StatBuilder
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