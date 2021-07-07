using UnityEngine;

namespace IsolateIsland.Runtime.Ai
{
    [CreateAssetMenu(fileName = "Animal Data", menuName = "Entity Status/Animal Data", order = int.MaxValue)]
    public class AnimalData : ScriptableObject
    {
        [Header("Animal Status")]
        [Tooltip("체력")]
        [SerializeField] private int _hp;
        public int hp { get => _hp; set => _hp = value; }

        [Tooltip("이동속도")]
        [SerializeField] float _moveSpeed;
        public float moveSpeed { get => Managers.Managers.Instance.GameManager.isDay ? _moveSpeed : _moveSpeed * 0.5f; }

        public float runSpeed { get => _moveSpeed * 2f; }



        [Header("Animal Behavior")]
        [Space(20)]
        [Tooltip("이동 범위")]
        [SerializeField] float _moveRange;
        public float moveRange { get => Managers.Managers.Instance.GameManager.isDay ? _moveRange : _moveRange * 0.7f; }

        [Tooltip("이동 시, 새로운 목표 위치를 만드는데 걸리는 시간")]
        [SerializeField] float _makePosCooltime;
        public float makePosCooltime { get => _makePosCooltime; }

        [Tooltip("도망 시, 도망효과가 지속되는 시간")]
        [SerializeField] float _runtime;
        public float runtime { get => _runtime; }

        [Tooltip("밤일 때, 수면상태로 빠지기까지 걸리는 시간")]
        [SerializeField] float _sleepingTime;
        public float sleepingTime { get => _sleepingTime; }

        [Tooltip("깨어난 후, 다시 수면상태가 될 때까지 걸리는 시간")]
        [SerializeField] float _sleepingCooltime;
        public float sleepingCooltime { get => _sleepingCooltime; }
    }
}