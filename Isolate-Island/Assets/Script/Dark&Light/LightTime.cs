using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class LightTime : MonoBehaviour
{
    public Volume volume;

    [SerializeField] float Timer;
    private int Day;
    [SerializeField] int Timercycle;
    private float angle = 90;
    double GetRadian(int degres)
    {
        return degres *(Mathf.PI / 180.0);
    }

    void Start()
    {
        volume.weight = 0;
        Debug.Log("Sun");
    }

    void Update()
    {
        Timer += Time.deltaTime;
        StartCoroutine(night());
    }
    IEnumerator night()
    {
        
        double num1 = GetRadian((int)angle);
        float k = (Mathf.Sin((float)num1) + 1) / 2;
        Debug.Log(k);
        //weight의 값이움직이는 시간을 timercycle로 해준다.

        --angle;

        //float tt = Mathf.Lerp(0, 1, Timercycle);
        

        //if (Timer => )
        //{
        //    Day += 1;            
        //    Debug.Log("Day");       
        //}
            
        volume.weight = k;
        yield return null;

    }

}
