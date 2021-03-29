using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneUI : MonoBehaviour
{
    public void Play2024Game()
    {
        SceneManager.LoadScene("2014Game");
    }
    public void PlayIngredientsGame()
    {
        SceneManager.LoadScene("IngredientsGame");
    }
}
