using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsolateIsland.Runtime.Hud
{

    public class TitleHud : MonoBehaviour
    {
        public void SetBgm(float value)
        {
            PlayerPrefs.SetFloat("Bgm", value);
            Managers.Managers.Instance.Sound.SetVolume(Managers.SoundManager.SoundType.BGM, value);
        }

        public void SetSfx(float value)
        {
            PlayerPrefs.SetFloat("Sfx", value);
            Managers.Managers.Instance.Sound.SetVolume(Managers.SoundManager.SoundType.SFX, value);
        }

    }
}
