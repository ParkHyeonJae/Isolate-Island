using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Ai
{
    public class Pig : AnimalAI
    {
        private AudioSource _audioSource = null;
        public AudioSource audioSource =>
            _audioSource = _audioSource ?? gameObject.GetOrAddComponent<AudioSource>();

        public override void ReduceHp(int value)
        {
            base.ReduceHp(value);
            animator.SetTrigger("isHit");
        }

        protected override bool Move()
        {
            animator.SetBool("isMove", base.Move());

            if (Managers.Managers.Instance.GameManager.isDay == false)
            {
                sleepCount += Time.time - deltaTime;
                deltaTime = Time.time;
            }

            return base.Move();
        }

        protected override bool RunMove()
        {
            animator.SetBool("isRun", base.RunMove());

            if (Managers.Managers.Instance.GameManager.isDay == false)
            {
                sleepCount += Time.time - deltaTime;
                deltaTime = Time.time;
            }

            return base.RunMove();
        }

        public void PlayWalkSound()
        {
            int temp = Random.Range(1, 5);
            string key = "돼지 걷는 소리 " + temp;

            Managers.Managers.Instance.Sound.PlayOneShot(audioSource, key);
        }

        public void PlayHitSound()
        {
            Managers.Managers.Instance.Sound.PlayOneShot(audioSource, "돼지 피해 소리");
        }

        public void PlayDeadSound()
        {
            Managers.Managers.Instance.Sound.PlayOneShot(audioSource, "돼지 죽는 소리");
        }
    }
}