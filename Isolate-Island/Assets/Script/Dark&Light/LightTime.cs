using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class LightTime : MonoBehaviour
{
    public Volume volume;

    [SerializeField] float Timer;
    private int Day;
    [SerializeField] float Timercycle;
    private float angle = 90;
    [SerializeField] float Stoptimer;

    private float appender = 1;

    double GetRadian(int degres)
    {
        return degres *(Mathf.PI / 180.0);
    }

    void Start()
    {
        volume.weight = 0;
        Debug.Log("Sun");
        StartCoroutine(night());
    }

    void Update()
    {
        
        Timer += Time.deltaTime * appender;
        
    }
    IEnumerator night()
    {

        //weight의 값이움직이는 시간을 timercycle로 해준다.
        while (gameObject.activeInHierarchy)
        {
            double num1 = GetRadian((int)angle); 
            float k = (Mathf.Sin(Timer) + 1) / 2;
            Debug.Log(k);
            yield return new WaitForSeconds((1.0f / Timercycle)); // 1 = 0.1
            --angle;
            volume.weight = k;

            if (Timer >= Timercycle)
                appender = -1;

            //if (appender == -1)          
            //yield return new WaitForSeconds((1.0f / Stoptimer));          

            else if (Timer <= 0)
                appender = 1;

            var ret = Mathf.Lerp(0, 1, Mathf.Clamp(Timer, 0, Timercycle) / Timercycle);
            volume.weight = ret;

            yield return null;
        }

        //float tt = Mathf.Lerp(0, 1, Timercycle);


        //if (Timer => )
        //{
        //    Day += 1;            
        //    Debug.Log("Day");       
        //}

        
        yield return null;

    }

}
