using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IsolateIsland.Runtime.Hud
{

    public class TitleHud : MonoBehaviour
    {
        [SerializeField] private FadeInOut fadeObject;
        [SerializeField] private SceneChanger sceneChanger;

        [SerializeField] private Slider bgmSlider;
        [SerializeField] private Slider sfxSlider;

        public void Start()
        {
            Managers.Managers.Instance.GameManager.onGame = false;

            bgmSlider.value = PlayerPrefs.GetFloat("Bgm", 1);
            sfxSlider.value = PlayerPrefs.GetFloat("Sfx", 1);
        }

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

        public void ChangeScene()
        {
            fadeObject.gameObject.SetActive(true);
            Invoke("ChangeSceneIngame", fadeObject._StartFadeTime);
        }

        public void ChangeSceneIngame() => sceneChanger._InGame();

    }
}
