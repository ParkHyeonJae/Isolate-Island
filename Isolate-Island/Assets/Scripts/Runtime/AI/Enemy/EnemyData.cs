using UnityEngine;

namespace IsolateIsland.Runtime.Ai
{
    [CreateAssetMenu(fileName = "Enemy Data", menuName = "Entity Status/Enemy Data", order = int.MaxValue)]
    public class EnemyData : ScriptableObject
    {
        [Header("Enemy Status")]
        [Tooltip("체력")]
        [SerializeField] private int _hp;
        public int hp { get => _hp; set => _hp = value; }

        [Tooltip("이동속도")]
        [SerializeField] float _moveSpeed;
        public float moveSpeed { get => _moveSpeed; }

        [Tooltip("공격시 데미지")]
        [SerializeField] int _attackDamage;
        public int attackDamage { get => _attackDamage; }


        [Header("Enemy Behavior")]
        [Space(20)]
        [Tooltip("탐색 범위")]
        [SerializeField] float _moveRange;
        public float moveRange { get => _moveRange; }

        [Tooltip("플레이어를 발견하였을시 추격 범위")]
        [SerializeField] float _trackRange;
        public float trackRange { get => _trackRange; }

        [Tooltip("플레이어에게의 공격 범위")]
        [SerializeField] float _attackRange;
        public float attackRange { get => _attackRange; }

        [Tooltip("탐색 시, 새로운 목표 위치를 만드는데 걸리는 시간")]
        [SerializeField] float _makePosCooltime;
        public float makePosCooltime { get => _makePosCooltime; }

        [Tooltip("공격 딜레이(초)")]
        [SerializeField] float _attackDelay;
        public float attackDelay { get => _attackDelay; }

        [Tooltip("공격 당했을 때, 추격 지속시간")]
        [SerializeField] float _revengeDuration;
        public float revengeDuration { get => _revengeDuration; }

        [Tooltip("밤일 때, 수면상태로 빠지기까지 걸리는 시간")]
        [SerializeField] float _sleepingTime;
        public float sleepingTime { get => _sleepingTime; }

        [Tooltip("깨어난 후, 다시 수면상태가 될 때까지 걸리는 시간")]
        [SerializeField] float _sleepingCooltime;
        public float sleepingCooltime { get => _sleepingCooltime; }

        public bool isDayOrNight { get => Managers.Managers.Instance.GameManager.isDay; }
    }
}