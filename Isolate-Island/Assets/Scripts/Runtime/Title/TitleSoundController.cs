using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Title
{

    public class TitleSoundController : MonoBehaviour
    {
        public void Start()
        {
            PlayTitleBgmSound();
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlayClickButtonSound();
            }
        }

        public void PlayTitleBgmSound()
        {
            Managers.Managers.Instance.Sound.Play("타이틀");
        }

        public void PlayClickButtonSound()
        {
            Managers.Managers.Instance.Sound.PlayOneShot("타이틀 마우스 버튼 클릭");
        }

    }

}