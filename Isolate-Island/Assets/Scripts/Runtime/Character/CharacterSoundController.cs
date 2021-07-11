using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IsolateIsland.Runtime.Event;

namespace IsolateIsland.Runtime.Character
{
    public class CharacterSoundController : MonoBehaviour
    {
        private AudioSource _audioSource = null;
        public AudioSource audioSource =>
            _audioSource = _audioSource ?? gameObject.GetOrAddComponent<AudioSource>();

        public void Start()
        {
            Managers.Managers.Instance.Event.GetListener<OnPlayerHitEvent>().Subscribe(PlayHitSound);
        }

        public void PlayWalkSound()
        {
            int temp = Random.Range(1, 9);
            string key = "플레이어 걸음 " + temp.ToString();

            Managers.Managers.Instance.Sound.PlayOneShot(audioSource, key);
        }

        public void PlayMeleeAttackSound()
        {

        }

        public void PlayRangedAttackSound()
        {

        }

        public void PlayHitSound()
        {
            Managers.Managers.Instance.Sound.PlayOneShot(audioSource, "플레이어 피해");
        }
    }

}