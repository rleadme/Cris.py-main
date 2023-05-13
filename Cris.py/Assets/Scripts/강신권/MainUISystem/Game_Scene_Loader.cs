using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameScene
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


    public void SceneLoader(GameScene scene)
    {
        switch (scene)
        {
            case GameScene.MainMenu:
                isGameStart = false;
                LoadScene("MainMenu");
                break;

            case GameScene.GameOver:
                isGameStart = false;
                // LoadScene("GameOver");
                break;

            case GameScene.GameScene1:
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
