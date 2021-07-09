using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BT;
using DG.Tweening;

namespace IsolateIsland.Runtime.Ai
{
    public class EnemyAI : MonoBehaviour
    {
        #region Root
        protected Selector root = new Selector();

        protected Selector behavior = new Selector();
        protected BTCondition dead = new BTCondition();

        protected Selector patrol = new Selector();
        protected Selector trace = new Selector();
        protected BTCondition attack = new BTCondition();

        protected Selector noise = new Selector();
        protected BTCondition track = new BTCondition();
        // note: 공격 당했을 때 추적하는 행동 추가

        protected BTAction chaseNoise = new BTAction();
        protected BTCondition checkNoise = new BTCondition();

        protected BTCondition move = new BTCondition();
        protected BTCondition makePos = new BTCondition();
        #endregion

        private Transform _targetTrans;

        private Vector3 _movePos;
        private bool _isAttackCooltime = false;
        private bool _isHearNoise = false;
        private float _makePosTime = 0;

        public EnemyData enemyData;

        private int _hp;
        public int hp { get { return _hp; } set { _hp = value; } }

        [SerializeField] protected Animator animator;
        public Animator Animator
        {
            get
            {
                if (animator == null)
                    animator = GetComponent<Animator>();

                return animator;
            }
        }
        
        [SerializeField] protected SpriteRenderer sprite;
        public SpriteRenderer Sprite
        {
            get
            {
                if (sprite == null)
                    sprite = GetComponent<SpriteRenderer>();

                return sprite;
            }
        }

        void Start() => Init();

        void OnEnable() 
        {
            _movePos = transform.position;
            hp = enemyData.hp;
        }

        protected virtual void Init()
        {
            InitBT();

            _movePos = transform.position;
            hp = enemyData.hp;

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

        protected virtual void Update()
        {
            root.OnUpdate();
            //Debug.Log("루트!");
        }

        bool Dead()
        {
            if (hp <= 0)
            {
                Managers.Managers.Instance.GameManager.UpKillCount();
                animator.Play("Dead");
                Invoke("Destroy", 3);
                return true;
            }
            else
                return false;
        }

        void Destroy()
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();

            sprite.DOFade(0, 1)
                .OnComplete(() =>
                { 
                    gameObject.SetActive(false);
                    sprite.DOFade(1, 0.1f)
                    .OnComplete(() => Managers.Managers.Instance.Pool.Destroy(gameObject));
                });
        }

        public virtual void ReduceHp(int value)
        {
            hp -= value;
        }

        protected virtual bool Attack()
        {
            float dist = Vector3.Distance(transform.position, _targetTrans.position);

            if (_isAttackCooltime)
                return false;

            if (dist <= enemyData.attackRange)
            {
                Fliping(_targetTrans.position);
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
            yield return new WaitForSeconds(enemyData.attackDelay);
            Debug.Log("공격딜레이 풀림");
            _isAttackCooltime = false;
        }

        protected virtual bool Track()
        {
            float dist = Vector2.Distance(transform.position, _targetTrans.position);

            if (dist <= enemyData.trackRange)
            {
                Fliping(_targetTrans.position);
                Vector2 dir = (_targetTrans.position - transform.position).normalized;
                transform.Translate(dir * (enemyData.moveSpeed * 1.2f) * Time.deltaTime);
                //Debug.Log("추적! : dist." + dist + "  dir." + (dir));
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
            if (dist <= 1f || Time.time - _makePosTime >= enemyData.makePosCooltime)
            {
                float x = Random.Range(-enemyData.moveRange, enemyData.moveRange);
                float y = Random.Range(-enemyData.moveRange, enemyData.moveRange);

                _movePos = (new Vector3(x, y, 0) * Random.Range(0.0f, enemyData.moveRange))
                    + transform.position;

                _movePos = new Vector3(x, y, 0) + transform.position;

                //Debug.Log("좌표 생성! " + _movePos);

                _makePosTime = Time.time;

                return true;
            }
            else
                return false;
        }

        protected virtual bool Move()
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

            Fliping(_movePos);
            Vector2 dir = (_movePos - transform.position).normalized;
            transform.Translate(dir * (enemyData.moveSpeed) * Time.deltaTime);
            //Debug.Log("이동! " + dir);
            return true;

        }

        public void Fliping(Vector3 target)
        {
            Sprite.flipX = transform.position.x <= target.x;
        }
    }

}