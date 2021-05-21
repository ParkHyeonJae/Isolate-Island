using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAnimation : MonoBehaviour
{
    private SceneNumber sceneNumber;
    public Animator transition;
    public float transitionTime;
    public string SceneName;
    
    void Start()
    {
        StartCoroutine(_ScreenAnimation());
    }

    void Update()
    {
       //dynamic a = sceneNumber;
       //SceneManager.LoadScene(a);
    }

    IEnumerator _ScreenAnimation()
    {
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(transitionTime);
    }
}
