using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameSceneCrisPy
{
    MainMenu,
    GameOver,
    GameScene1,
}


public class Game_Scene_Loader : MonoBehaviour
{
    private bool isGameStart = false;


    private void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }


    public void LoadScene(GameSceneCrisPy scene)
    {
        switch (scene)
        {
            case GameSceneCrisPy.MainMenu:
                isGameStart = false;
                LoadScene("MainMenu");
                break;

            case GameSceneCrisPy.GameOver:
                isGameStart = false;
                // LoadScene("GameOver");
                break;

            case GameSceneCrisPy.GameScene1:
                isGameStart = true;
                LoadScene("TestMap");
                break;

            // ... GameScene Add...

            default:
                Debug.Log(scene.ToString() + "아직 미구현");
                break;
        }
    }
}
