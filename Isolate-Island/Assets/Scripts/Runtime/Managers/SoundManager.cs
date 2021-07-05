using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Managers
{
    public class SoundManager : MonoManagerInit
    {
        private AudioSource _audioBGMSource = null;
        public AudioSource audioBGMSource =>
            _audioBGMSource = _audioBGMSource ?? gameObject.GetOrAddComponent<AudioSource>();

        private AudioSource _audioEffectSource = null;
        public AudioSource audioEffectSource =>
            _audioEffectSource = _audioEffectSource ?? gameObject.GetOrAddComponent<AudioSource>();
        public override void OnInit()
        {
            
        }

        public void PlayOneShot(string name)
        {
            var clip = Managers.Instance.Resource.Load<AudioClip>(name);
            audioEffectSource.PlayOneShot(clip);
        }
        public void Play(string name)
        {
            var clip = Managers.Instance.Resource.Load<AudioClip>(name);
            audioBGMSource.loop = true;
            audioBGMSource.clip = clip;
            audioBGMSource.Play();
        }
    }
}