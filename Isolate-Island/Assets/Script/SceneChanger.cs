using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IsolateIsland.Runtime
{
    public class SceneChanger : MonoBehaviour
    {
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
}