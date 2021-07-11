using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Inventory
{
    public class ItemBehaviourModule_MoveTo : ItemBehaviourModule
    {
        private Vector3 _destPos;
        private Vector3 _startPos;
        public void SetDestinationPosition(Vector3 destinationPosition)
            => _destPos = destinationPosition;
        public void SetStartPosition(Vector3 startPosition)
            => _startPos = startPosition;

        private int _range = 0;
        public void SetRange(int range)
            => _range = range;

        protected Vector2 dir;

        public override void Do()
        {
            base.Do();
            GetRigidBody.isKinematic = true;
            StartCoroutine(MoveTo(_startPos, _destPos));
            //Managers.Managers.Instance.Coroutine.StartRoutine(MoveTo(_startPos, _destPos));
            Managers.Managers.Instance.Util.AddTimer(_range * 0.1f, () => Managers.Managers.Instance.Pool.Destroy(gameObject));
        }

        IEnumerator MoveTo(Vector2 startPos, Vector2 destPos)
        {
            transform.position = _startPos;
            
            Debug.Log(transform.position);
            Debug.Log(destPos);
            while (gameObject.activeInHierarchy)
            {
                dir = (destPos - startPos).normalized;

                GetRigidBody.velocity = dir * 20f;
                transform.eulerAngles = new Vector3(0, 0, (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) -135f);
                yield return null;
            }
        }
    }
}