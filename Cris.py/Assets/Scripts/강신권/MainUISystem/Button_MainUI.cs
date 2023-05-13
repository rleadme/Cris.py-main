using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_MainUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private BtnTypeMainUI buttonType;
    [SerializeField]
    private Transform buttonCurrentScale;
    [SerializeField]
    private Vector3 buttonDefaultScale;


    private Game_Scene_Loader sceneLoader;

    private void Start()
    {
        buttonCurrentScale = transform;
        buttonDefaultScale = buttonCurrentScale.localScale;

        sceneLoader = GameObject.FindObjectOfType<Game_Scene_Loader>();
        if (sceneLoader == null)
        {
            Debug.LogError("Game_Scene_Loader component not found in the scene.");
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonCurrentScale.localScale = buttonDefaultScale * 1.1f;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        buttonCurrentScale.localScale = buttonDefaultScale;
    }


    public void OnBtnClick()
    {
        switch (buttonType)
        {
            case BtnTypeMainUI.GameStart:
                Debug.Log("GameStart");
                sceneLoader.SceneLoader(GameScene.GameScene);
                break;

            case BtnTypeMainUI.GameStartNew:
                Debug.Log("GameStartNew");
                break;

            case BtnTypeMainUI.GameStartContinue:
                Debug.Log("GameStartContinue");
                break;

            case BtnTypeMainUI.GameOption:
                Debug.Log("GameOption");
                break;

            case BtnTypeMainUI.GameQuit:
                Debug.Log("GameQuit");
                Application.Quit();
                break;

            default:
                Debug.Log(buttonType.ToString() + "아직 미구현");
                break;
        }
    }
}
