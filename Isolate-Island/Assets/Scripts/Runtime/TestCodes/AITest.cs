using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BT;

public class AITest : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStatus
    {
        [SerializeField] float _hp = 10;
        public float hp { get => _hp; set => _hp = value; }

        [Range(0.0f, 10.0f)]
        [SerializeField] float _moveSpeed = 5.0f;
        public float moveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

        [SerializeField] int _attackDamage = 10;
        public int attackDamage { get => _attackDamage; set => _attackDamage = value; }

    }

    //Memo : 밤/낮 상태를 확인할 수 있는 변수가 있다면, 그것을 받아와서 하단 변수의 프로퍼티에서 값의 변화를 준다.
    [System.Serializable]
    public class EnemyBehaviour
    {
        [SerializeField] float _moveRange;
        public float moveRange { get => _moveRange; set => _moveRange = value; }

        [SerializeField] float _trackRange;
        public float trackRange { get => _trackRange; set => _trackRange = value; }

        [SerializeField] float _attackRange;
        public float attackRange { get => _attackRange; set => _attackRange = value; }

        float _attackDelay = 1f;
        public float attackDelay { get => _attackDelay; set => _attackDelay = value; }

        [SerializeField] float _revengeDuration = 5f;
        public float revengeDuration { get => _revengeDuration; set => _revengeDuration = value; }

        [SerializeField] float _sleepingTime = 60f;
        public float sleepingTime { get => _sleepingTime; set => _sleepingTime = value; }

        [SerializeField] float _sleepingCooltime = 30f;
        public float sleepingCooltime { get => _sleepingCooltime; set => _sleepingCooltime = value; }

        [SerializeField] float _makePosCooltime = 5f;
        public float makePosCooltime { get => _makePosCooltime; set => _makePosCooltime = value; }
    }

    [SerializeField] EnemyBehaviour _enemyBehaviour;

    [SerializeField] EnemyStatus _enemyStatus;

    [SerializeField] Transform _targetTrans;

    Vector3 _movePos;
    bool _isAttackCooltime = false;
    bool _isHearNoise = false;
    float _makePosTime = 0;

    Selector root = new Selector();

    Selector behavior = new Selector();
    BTCondition dead = new BTCondition();

    Selector patrol = new Selector();
    Selector trace = new Selector();
    BTCondition attack = new BTCondition();

    Selector noise = new Selector();
    BTCondition track = new BTCondition();
    // note: 공격 당했을 때 추적하는 행동 추가

    BTAction chaseNoise = new BTAction();
    BTCondition checkNoise = new BTCondition();

    BTCondition move = new BTCondition();
    BTCondition makePos = new BTCondition();

    void Start() => Init();

    void OnEnable() => _movePos = transform.position;

    void Init()
    {
        InitBT();

        _movePos = transform.position;

        _targetTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void InitBT()
    {
        root.AddChild(behavior);
        root.AddChild(dead);

        dead.action = Dead;

        behavior.AddChild(patrol);
        behavior.AddChild(trace);
        behavior.AddChild(attack);

        attack.action = Attack;

        trace.AddChild(noise);
        trace.AddChild(track);
        // note: 공격 당했을 때 추적하는 행동 추가

        patrol.AddChild(move);
        patrol.AddChild(makePos);



        track.action = Track;

        noise.AddChild(chaseNoise);
        noise.AddChild(checkNoise);

        chaseNoise.action = ChaseNoise;
        checkNoise.action = CheckNoise;

        move.action = Move;
        makePos.action = MakePos;
    }

    void Update()
    {
        root.OnUpdate();
        Debug.Log("루트!");
    }

    bool Dead()
    {
        if (_enemyStatus.hp <= 0)
        {
            Debug.Log("죽음~");
            gameObject.SetActive(false);
            return true;
        }
        else
            return false;
    }

    bool Attack()
    {
        float dist = Vector3.Distance(transform.position, _targetTrans.position);

        if (_isAttackCooltime)
            return false;

        if (dist <= _enemyBehaviour.attackRange)
        {
            Debug.Log("공격!");
            StartCoroutine(CooldownAttack());
            return true;
        }
        else
            return false;
    }

    IEnumerator CooldownAttack()
    {
        _isAttackCooltime = true;
        yield return new WaitForSeconds(_enemyBehaviour.attackDelay);
        Debug.Log("공격딜레이 풀림");
        _isAttackCooltime = false;
    }

    bool Track()
    {
        float dist = Vector2.Distance(transform.position, _targetTrans.position);

        if (dist <= _enemyBehaviour.trackRange)
        {
            Vector2 dir = (_targetTrans.position - transform.position).normalized;
            transform.Translate(dir * (_enemyStatus.moveSpeed * 1.2f) * Time.deltaTime);
            Debug.Log("추적! : dist." + dist + "  dir." + (dir));
            return true;
        }
        else
            return false;
    }

    bool CheckNoise()
    {
        if (false) // note: 소음을 들었을 때를 확인해야 하나 소음 발생 자체가 일어나지 않으므로 보류
        {
            _isHearNoise = true;
            Debug.Log("s!");
            return true;
        }
        else
            return false;
    }

    bool ChaseNoise()
    {
        if (_isHearNoise)
        { 
            return true;
            Debug.Log("s!");
        }
        else
            return false;
    }

    bool MakePos()
    {
        float dist = Vector3.Distance(transform.position, _movePos);

        //Debug.Log(dist);
        if (dist <= 1f || Time.time - _makePosTime >= _enemyBehaviour.makePosCooltime)
        {
            float x = Random.Range(-_enemyBehaviour.moveRange, _enemyBehaviour.moveRange);
            float y = Random.Range(-_enemyBehaviour.moveRange, _enemyBehaviour.moveRange);

            _movePos = (new Vector3(x, y, 0) * Random.Range(0.0f, _enemyBehaviour.moveRange))
                + transform.position;

            _movePos = new Vector3(x, y, 0) + transform.position;

            Debug.Log("좌표 생성! " + _movePos);

            _makePosTime = Time.time;

            return true;
        }
        else
            return false;
    }

    bool Move()
    {
        float dist = Vector3.Distance(transform.position, _movePos);

        //if (dist >= _enemyBehaviour.trackRange)
        //{
        //    Vector3 dir = (_movePos - transform.position).normalized;
        //    dir.z = 0;
        //    transform.Translate(dir * _enemyStatus.moveSpeed * Time.deltaTime);
        //    Debug.Log("이동! " + dist);
        //    return true;
        //}
        //else
        //    return false;

        Vector2 dir = (_movePos - transform.position).normalized;
        transform.Translate(dir * (_enemyStatus.moveSpeed) * Time.deltaTime);
        //Debug.Log("이동! " + dir);
        return true;

    }
}
