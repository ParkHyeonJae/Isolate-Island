using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    [Header("-실행조건 -활성화될시")]
    [Space(3)]
    public Image FadeImg;
    private Color _color;
    private bool isPlaying = false;
    private float time;
    private float _Start;
    private float _End;

    [Header("-FadeInOut 시간 관련 조정")]
    [Range(0.0f,10.0f)]
    public float _StartFadeTime = 2.0f;
    [Range(0.0f, 10.0f)]
    public float _OutFadeTime = 2.0f;
    [Space(3)]
    [Header("   ->FadeStart 와 FadeEnd사이의 멈춰있는시간")]
    [Range(0.0f, 10.0f)]
    public float _BreakTime = 0.0f;

    [Space(3)]

    [Header("-Fade Check")]
    public bool _StartFade = false;
    public bool _OutFade = false;
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
    private void OnEnable()
    {
        if (_StartFade == true && _OutFade == false)
        {
            isPlaying = false;
            _Start = 0.0f;
            _End = 1.0f;
            time = 0;
            _color.a = 0.0f;
            FadeImg = GetComponent<Image>();

            InStartFade();
        }
        else if (_OutFade == true && _StartFade == false)
        {
            isPlaying = false;
            _Start = 1.0f;
            _End = 0.0f;
            time = 0;
            _color.a = 255.0f;
            FadeImg = GetComponent<Image>();

            InOutFade();
        }
        else if (_OutFade == true && _StartFade == true)
        {
            isPlaying = false;
            _Start = 0.0f;
            _End = 1.0f;
            time = 0;
            _color.a = 0.0f;
            FadeImg = GetComponent<Image>();

            InStartFade();
        }
    }
    void InStartFade()
    {
        if (isPlaying == true)
            return;

        _Start = 0.0f;
        _End = 1.0f;

        StartCoroutine(StartFade());
    }
    IEnumerator StartFade()
    {

        isPlaying = true;
        _color = FadeImg.color;
        time = 0;
        _color.a = 0.0f;
        while (_color.a < 1.0f)
        {
            time += Time.deltaTime / _StartFadeTime;

            _color.a = Mathf.Lerp(_Start, _End, time);

            FadeImg.color = _color;
            //Debug.Log(_color.a);
            yield return null;
        }
        
        isPlaying = false;
        if (_OutFade == true)
        {
            yield return new WaitForSeconds(_BreakTime);
            InOutFade();
        }
    }
    void InOutFade()
    {
        if (isPlaying == true)
            return;

        _Start = 1.0f;
        _End = 0.0f;

        StartCoroutine(OutFade());
    }
    IEnumerator OutFade()
    {
        isPlaying = true;
        _color = FadeImg.color;
        time = 0;
        _color.a = 255.0f;
        while (_color.a > 0.0f)
        {
            time += Time.deltaTime / _OutFadeTime;
            
            _color.a = Mathf.Lerp(_Start, _End, time);

            FadeImg.color = _color;
            yield return null;
        }
        isPlaying = false;

        this.gameObject.SetActive(false);
    }

}
