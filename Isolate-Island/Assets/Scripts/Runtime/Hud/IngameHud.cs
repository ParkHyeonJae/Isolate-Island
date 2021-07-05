using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IsolateIsland.Runtime.Stat;
using PlayerPrefs = IsolateIsland.Runtime.Managers.PlayerPrefs;

namespace IsolateIsland.Runtime.Hud
{
    public class Gauge : MonoBehaviour
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

        [Header("Gauge")]
        [SerializeField] private Image _hpGaugeImage;
        [SerializeField] private Image _hungerGaugeImage;

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

        [Header("Quick Slot")]
        [SerializeField] private Image _slotImage;
        [SerializeField] private Text _slotText;

        private void Start()
        {
            Init();
            StartCoroutine(SetGaugeValue());
        }

        private void Init()
        {
            _playerStat = Managers.Managers.Instance.statManager.UserStat;

            _hpGauge = new Gauge(_hpGaugeImage, _playerStat.HP, 0, _playerStat.HP);
            _hungerGauge = new Gauge(_hungerGaugeImage, _playerStat.Hungry, 0, _playerStat.Hungry);

            _bgmSlider.value = PlayerPrefs.GetFloat("Bgm", 1);
            _sfxSlider.value = PlayerPrefs.GetFloat("Sfx", 1);

            _vibrationToggle.isOn = PlayerPrefs.GetBool("Vibration", true);
            _autoaimToggle.isOn = PlayerPrefs.GetBool("AutoAim", false);

            Managers.Managers.Instance.Event.GetListener<Event.OnClickConfigButtonEventListener>().Subscribe(OnInitQuickSlot);
        }

        IEnumerator SetGaugeValue()
        {
            _hpGauge.SetCurrnetFillAmount(_playerStat.HP);
            _hungerGauge.SetCurrnetFillAmount(_playerStat.Hungry);

            // 테스트를 위한 코드
            //if (_hungerGauge.GetCurrentFillAmountRatio() <= 0.25f)
            //    Managers.Managers.Instance.statManager.UserStat.MoveSpeed = 10;
            //else
            //    Managers.Managers.Instance.statManager.UserStat.MoveSpeed = 20;

            //Managers.Managers.Instance.statManager.UserStat.HP--;
            //Managers.Managers.Instance.statManager.UserStat.Hungry--;

            //Debug.Log(Managers.Managers.Instance.statManager.UserStat.MoveSpeed);
            // 끝

            yield return new WaitForEndOfFrame();

            StartCoroutine(SetGaugeValue());
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
            //씬 전환 코드
        }

        public void SetBgm(float value)
        {
            PlayerPrefs.SetFloat("Bgm", value);
        }

        public void SetSfx(float value)
        {
            PlayerPrefs.SetFloat("Sfx", value);
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
            _gameoverPopup.SetActive(true);
            yield return 0;
        }

        public void ClickRetry()
        {

        }


        public void OnInitQuickSlot(Utils.Defines.EDressableState eDressableState)
        {
            var itemParts = Managers.Managers.Instance.Inventory.Dressable.GetParts(EParts.PARTS_RIGHT_HAND);
            if (itemParts is null)
                return;

            var itemCount = Managers.Managers.Instance.Inventory.Dressable.GetItemCount(itemParts);
            _slotImage.sprite = itemParts.DressableCombinationNode.sprite;
            _slotText.text = itemCount.ToString();
        }
    }
}
