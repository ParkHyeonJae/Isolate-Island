using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Ai
{
    public class EnemySoundController : MonoBehaviour
    {
        private AudioSource _audioSource = null;
        public AudioSource audioSource =>
            _audioSource = _audioSource ?? gameObject.GetOrAddComponent<AudioSource>();

        public void PlayAtttackSound()
        {
            Managers.Managers.Instance.Sound.PlayOneShot(audioSource, "적 공격");
        }

        public void PlayHitSound()
        {
            Managers.Managers.Instance.Sound.PlayOneShot(audioSource, "적 피격");
        }

        public void PlayDeadSound()
        {
            Managers.Managers.Instance.Sound.PlayOneShot(audioSource, "적 사망");
        }
    }
}