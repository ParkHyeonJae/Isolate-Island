using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Managers
{
    public class SoundManager : MonoManagerInit
    {

        [SerializeField]
        public enum SoundType
        { 
            BGM,
            SFX
        }


        private AudioSource _audioBGMSource = null;
        public AudioSource audioBGMSource =>
            _audioBGMSource = _audioBGMSource ?? gameObject.AddComponent<AudioSource>();

        private AudioSource _audioEffectSource = null;
        public AudioSource audioEffectSource =>
            _audioEffectSource = _audioEffectSource ?? gameObject.AddComponent<AudioSource>();
        public override void OnInit()
        {
            
        }

        public void PlayOneShot(string name)
        {
            var clip = Managers.Instance.Resource.Load<AudioClip>(name);
            audioEffectSource.PlayOneShot(clip);
        }

        public void PlayOneShot(string name, Vector3 pos)
        {
            var clip = Managers.Instance.Resource.Load<AudioClip>(name);
            AudioSource.PlayClipAtPoint(clip, pos, audioEffectSource.volume);
        }

        public void PlayOneShot(AudioSource source, string name)
        {
            var clip = Managers.Instance.Resource.Load<AudioClip>(name);
            source.PlayOneShot(clip, audioEffectSource.volume);
        }

        public void StopSound(SoundType type)
        {
            switch (type)
            {
                case SoundType.BGM:
                    audioBGMSource.Stop();
                    break;
                case SoundType.SFX:
                    audioEffectSource.Stop();
                    break;
                default:
                    break;
            }
        }

        public void Play(string name)
        {
            var clip = Managers.Instance.Resource.Load<AudioClip>(name);
            audioBGMSource.loop = true;
            audioBGMSource.clip = clip;
            audioBGMSource.Play();
        }

        public void SetVolume(SoundType type, float volume)
        {
            switch (type)
            {
                case SoundType.BGM:
                    audioBGMSource.volume = volume;
                    break;
                case SoundType.SFX:
                    audioEffectSource.volume = volume;
                    break;
                default:
                    break;
            }
        }
    }
}