using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public enum SceneNumber
{
    Title, InGame
}

public class SceneChange : MonoBehaviour
{
    [SerializeField] SceneNumber sceneNumber;

    public void OnpressedPlayButton(SceneNumber scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
    public void _Title()
    {
        SceneManager.LoadScene((int)SceneNumber.Title);
    }
    public void _InGame()
    {
        SceneManager.LoadScene((int)SceneNumber.InGame);
    }
}
