using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName;

    public void Play() 
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void Quit() 
    {
        Application.Quit();
    }
}
