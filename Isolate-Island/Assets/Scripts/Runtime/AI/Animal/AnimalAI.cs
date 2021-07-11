using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BT;
using DG.Tweening;

namespace IsolateIsland.Runtime.Ai
{
    public class AnimalAI : MonoBehaviour
    {
        #region Root
        protected Selector root = new Selector();

        protected Selector behavior = new Selector();
        protected BTCondition dead = new BTCondition();

        protected Selector patrol = new Selector();
        protected Selector run = new Selector();
        protected BTCondition sleeping = new BTCondition();

        protected BTCondition move = new BTCondition();
        protected BTCondition makePos = new BTCondition();

        protected BTCondition runMove = new BTCondition();
        protected BTCondition runPos = new BTCondition();
        #endregion

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

        public AnimalData entityData;

        private int _hp;
        public int hp { get { return _hp; } set { _hp = value; } }

        protected float sleepCount;
        protected float deltaTime;
        protected bool isSleeping;
        protected bool isRun;
        
        private Vector3 _standardPos;
        private Vector3 _movePos;
        private float _makePosTime = 0;
        private bool _isDead = false;


        void Start() => Init();

        void OnEnable()
        {
            _movePos = transform.position;
            _standardPos = transform.position;
            hp = entityData.hp;
            _isDead = false;
        }

        protected virtual void Init()
        {
            InitBT();

            _movePos = transform.position;
            _standardPos = transform.position;
            hp = entityData.hp;

            sleepCount = 0;
            deltaTime = 0;
            isSleeping = false;
            isRun = false;
            _isDead = false;
        }

        void InitBT()
        {
            root.AddChild(behavior);
            root.AddChild(dead);

            dead.action = Dead;

            behavior.AddChild(patrol);
            behavior.AddChild(run);
            behavior.AddChild(sleeping);

            patrol.AddChild(move);
            patrol.AddChild(makePos);

            run.AddChild(runMove);
            run.AddChild(runPos);


            sleeping.action = Sleeping;

            move.action = Move;
            makePos.action = MakePos;

            runMove.action = RunMove;
            runPos.action = RunPos;
        }

        protected virtual void Update()
        {
            if (!_isDead)
            root.OnUpdate();
        }

        public virtual void ReduceHp(int value)
        {
            hp -= value;

            sleepCount = 0;
            deltaTime = 0;

            isRun = true;
            Invoke("EndRun", entityData.runtime);
        }

        void EndRun() => isRun = false;

        bool Dead()
        {
            if (_isDead)
                return true;
            if (hp <= 0)
            {
                animator.Play("Dead");
                Invoke("Destroy", 3);
                return true;
            }
            else
                return false;
        }

        void Destroy()
        {
            if (_isDead)
                return;

            _isDead = true;

            SpriteRenderer sprite = GetComponent<SpriteRenderer>();

            var meat = Managers.Managers.Instance.Pool.Instantiate("날고기");
            meat.SetActive(true);
            meat.transform.position = transform.position;

            sprite.DOFade(0, 1)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    sprite.DOFade(1, 0.1f)
                    .OnComplete(() => Managers.Managers.Instance.Pool.Destroy(gameObject));
                });
        }

        protected bool Sleeping()
        {
            if (Managers.Managers.Instance.GameManager.isDay)
            {
                sleepCount = 0;
                deltaTime = 0;
                isSleeping = false;
            }

            if (sleepCount >= entityData.sleepingTime)
            {
                isSleeping = true;
                return true;
            }
            else if (sleepCount >= entityData.sleepingCooltime && isSleeping)
            {
                return true;
            }

            return false;
        }

        bool MakePos()
        {
            if (Time.time - _makePosTime >= entityData.makePosCooltime)
            {
                float x = Random.Range(-entityData.moveRange, entityData.moveRange);
                float y = Random.Range(-entityData.moveRange, entityData.moveRange);

                _movePos = new Vector3(x, y, 0) + _standardPos;

                //Debug.Log("좌표 생성! " + _movePos);

                _makePosTime = Time.time;

                return true;
            }
            else
                return false;
        }

        bool RunPos()
        {
            if (!isRun)
                return false;

            float dist = Vector3.Distance(transform.position, _movePos);

            //Debug.Log(dist);
            if (dist <= 1f || Time.time - _makePosTime >= entityData.makePosCooltime)
            {
                float x = Random.Range(-entityData.moveRange*0.5f, entityData.moveRange*0.5f);
                float y = Random.Range(-entityData.moveRange*0.5f, entityData.moveRange*0.5f);

                _movePos = new Vector3(x, y, 0) + _standardPos;

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

            if (dist <= 1f)
                return false;

            Vector2 dir = (_movePos - transform.position).normalized;
            transform.Translate(dir * (entityData.moveSpeed * 2) * Time.deltaTime);
            //Debug.Log("이동! " + dir);
            return true;

        }

        protected virtual bool RunMove()
        {
            if (!isRun)
                return false;

            float dist = Vector3.Distance(transform.position, _movePos);

            Vector2 dir = (_movePos - transform.position).normalized;
            transform.Translate(dir * (entityData.runSpeed) * Time.deltaTime);
            //Debug.Log("이동! " + dir);

            sleepCount = 0;
            deltaTime = 0;

            return true;

        }
    }
}