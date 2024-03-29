﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using IsolateIsland.Runtime.Stat;
using IsolateIsland.Runtime.Event;
using IsolateIsland.Runtime.Managers;
using PlayerPrefs = IsolateIsland.Runtime.Managers.PlayerPrefs;
using Manager = IsolateIsland.Runtime.Managers.Managers;

namespace IsolateIsland.Runtime.Hud
{
    public class Gauge
    {
        private Image _fillImage;
        private float _min, _max;
        private float _curFillAmount;

        public Gauge(Image fillImage, float max, float min = 0, float curValue = 0)
        {
            _fillImage = fillImage;
            _max = max;
            _min = min;
            SetCurrnetFillAmount(curValue);
        }

        public Gauge(Image fillImage, float curValue = 0)
        {
            _fillImage = fillImage;
            _max = 1;
            _min = 0;
            SetCurrnetFillAmount(curValue);
        }

        public void SetCurrnetFillAmount(float curValue)
        {
            if (curValue < _min)
                _curFillAmount = 0;
            else if (curValue > _max)
                _curFillAmount = 1;
            else
                _curFillAmount = curValue / (_min + _max);

            _fillImage.fillAmount = _curFillAmount;
        }

        public float GetCurrentFillAmountValue()
        {
            float value = _curFillAmount * (_min + _max);
            return value;
        }

        public float GetCurrentFillAmountRatio()
        {
            return _curFillAmount;
        }
    }


    public class IngameHud : MonoBehaviour
    {
        private Stat.Stat _playerStat;
        private Gauge _hpGauge;
        private Gauge _hungerGauge;
        private Gauge _timeDayGauge;
        private Gauge _timeNightGauge;

        [Header("Gauge")]
        [SerializeField] private Image _hpGaugeImage;
        [SerializeField] private Image _hungerGaugeImage;
        [SerializeField] private Image _timeDayGaugeImage;
        [SerializeField] private Image _timeNightGaugeImage;

        [Header("Popup")]
        [SerializeField] private GameObject _pausePopup;
        [SerializeField] private GameObject _optionPopup;
        [SerializeField] private GameObject _gameoverPopup;

        [Header("Slider")]
        [SerializeField] private Slider _bgmSlider;
        [SerializeField] private Slider _sfxSlider;

        [Header("Toggle")]
        [SerializeField] private Toggle _vibrationToggle;
        [SerializeField] private Toggle _autoaimToggle;
        [SerializeField] private Toggle _googleToggle;

        [Header("Image")]
        [SerializeField] private Image _bloodScreenImage;
        [SerializeField] private Image _hungerWarningPoint;

        [Header("Text")]
        [SerializeField] private Text _timeDateText;
        [SerializeField] private Text _gameoverKillText;
        [SerializeField] private Text _gameoverDayText;

        private void Start()
        {
            Init();
            StartCoroutine(SetGaugeValue());
        }

        private void Init()
        {
            _playerStat = Manager.Instance.statManager.UserStat;

            _hpGauge = new Gauge(_hpGaugeImage, _playerStat.HP, 0, _playerStat.HP);
            _hungerGauge = new Gauge(_hungerGaugeImage, _playerStat.Hungry, 0, _playerStat.Hungry);

            _timeDayGauge = new Gauge(_timeDayGaugeImage, 1);
            _timeNightGauge = new Gauge(_timeNightGaugeImage, 1);

            _hungerWarningPoint.rectTransform.anchoredPosition = new Vector3(Mathf.Lerp(0, 350, GameManager.debuffHungry * 0.01f), 0);
            //_hungerWarningPoint.rectTransform.

            _bgmSlider.value = PlayerPrefs.GetFloat("Bgm", 1);
            _sfxSlider.value = PlayerPrefs.GetFloat("Sfx", 1);

            _vibrationToggle.isOn = PlayerPrefs.GetBool("Vibration", true);
            _autoaimToggle.isOn = PlayerPrefs.GetBool("AutoAim", false);

            Manager.Instance.Event.GetListener<OnGameoverEvent>().Subscribe(() => StartCoroutine(OnGameoverPopup()));
            Manager.Instance.Event.GetListener<OnChangeDayOrNightEvent>().Subscribe(ChangeDay);
            Manager.Instance.Event.GetListener<OnPlayerHitEvent>().Subscribe(OnHitScreen);
        }

        IEnumerator SetGaugeValue()
        {
            _hpGauge.SetCurrnetFillAmount(_playerStat.HP);
            _hungerGauge.SetCurrnetFillAmount(_playerStat.Hungry);

            if (Manager.Instance.GameManager.isDay)
            {
                _timeDayGauge.SetCurrnetFillAmount(Manager.Instance.GameManager.flowDayTime);
            }
            else
            {
                _timeNightGauge.SetCurrnetFillAmount(Manager.Instance.GameManager.flowDayTime);
            }

            yield return new WaitForEndOfFrame();

            StartCoroutine(SetGaugeValue());
        }

        public void ChangeDay()
        {
            bool isDay = Manager.Instance.GameManager.isDay;

            if (!isDay)
            {
                _timeNightGauge.SetCurrnetFillAmount(1);
                _timeDayGauge.SetCurrnetFillAmount(1);
                _timeDateText.text = "Day " + (Manager.Instance.GameManager.survivalDate + 1).ToString();
            }
        }

        public void OnPause()
        {
            if (!_pausePopup.activeSelf)
            {
                _pausePopup.SetActive(true);
                Time.timeScale = 0;
            }
        }

        public void ClickResume()
        {
            if (_pausePopup.activeSelf)
            {
                _pausePopup.SetActive(false);
                Time.timeScale = 1;
            }
        }

        public void ClickOption()
        {
            _pausePopup.SetActive(false);
            _optionPopup.SetActive(true);
        }

        public void ClickTitle()
        {
            Time.timeScale = 1;
            SceneChange._Title();
        }

        public void SetBgm(float value)
        {
            PlayerPrefs.SetFloat("Bgm", value);
            Manager.Instance.Sound.SetVolume(Managers.SoundManager.SoundType.BGM, value);
        }

        public void SetSfx(float value)
        {
            PlayerPrefs.SetFloat("Sfx", value);
            Manager.Instance.Sound.SetVolume(Managers.SoundManager.SoundType.SFX, value);
        }

        public void ChangeVibration(bool isActive)
        {
            PlayerPrefs.SetBool("Vibration", isActive);
        }

        public void ChangeAutoaim(bool isActive)
        {
            PlayerPrefs.SetBool("AutoAim", isActive);
        }

        public void ClickGoogleLogin()
        {
            //...
        }

        public void CloseOption()
        {
            _optionPopup.SetActive(false);
            _pausePopup.SetActive(true);
        }

        IEnumerator OnGameoverPopup()
        {
            float fadeDurationDimming = 4;
            float fadeDurationUi = 2.5f;

            _gameoverDayText.text = "Day : " + Manager.Instance.GameManager.survivalDate.ToString();
            _gameoverKillText.text = "Kill : " + Manager.Instance.GameManager.killCount.ToString();

            _gameoverPopup.SetActive(true);

            _gameoverPopup.transform.Find("Dimming").gameObject.GetOrAddComponent<Image>()
                .DOFade(180f/255f, fadeDurationDimming);
            yield return new WaitForSeconds(fadeDurationDimming);
            _gameoverPopup.transform.Find("UI").gameObject.GetOrAddComponent<CanvasGroup>()
                .DOFade(1, fadeDurationUi);
            Manager.Instance.Sound.PlayOneShot("게임오버");
        }

        public void ClickRetry()
        {
            SceneChange._InGame();
        }

        public void OnHitScreen()
        {
            float fadeDuration = 1;

            _bloodScreenImage.DOFade(165f / 255f, 0.1f)
                .OnComplete(() => _bloodScreenImage.DOFade(0, fadeDuration));
        }
    }
}
