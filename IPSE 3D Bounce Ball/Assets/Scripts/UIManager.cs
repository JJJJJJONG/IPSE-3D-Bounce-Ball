using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine.EventSystems;

//뒤로가기 버튼이 필요함.
public class UIManager : MonoBehaviour
{
    public Button[] stageButtons = new Button[15];
    public TextMeshProUGUI[] stageTexts = new TextMeshProUGUI[15];
   
    Sprite openedBtnSprite;
    Sprite lockedBtnSprite;
   
    GameManager gameManager;
    AudioManager audioManager;
    SceneManager sceneManager;

    private void Awake()
    {   
        gameManager = FindObjectOfType<GameManager>();
      
        
        openedBtnSprite = Resources.Load<Sprite>("Image/OpenedButton");
        lockedBtnSprite = Resources.Load<Sprite>("Image/RockedButton");

        //스테이지 자식 오브젝트(text)를 위한 변수 초기화
        Scene scene = SceneManager.GetSceneByName("StageUI");

        if(scene.name == "StageUI") 
        {
            for (int i = 0; i < gameManager.stageIndex; i++)
            {

                stageTexts[i] = stageButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            }

           
        }

    }

    private void Start()
    {
        //스테이지 초기화
        SetStage();
    }
    private void Update()
    {
 
        CheckStage();
    }

    public void SetStage() 
    {

        stageButtons[0].image.sprite = openedBtnSprite;

        stageTexts[0].gameObject.SetActive(true);

        for(int i = 1; i < gameManager.stageIndex; i++)
        {
            stageButtons[i].image.sprite = lockedBtnSprite;

            stageTexts[i].gameObject.SetActive(false);
        }
          
    }
    void CheckStage() //스테이지가 클리어 됐는지 안됐는지 확인.
    {
        bool isOpen = false;

        if (gameManager.starCount[gameManager.stageNumber + 1] == gameManager.totalStarCount[gameManager.stageNumber + 1])
        {
            isOpen = true;
            int rstageNumber = gameManager.stageNumber+1;
            UpdateStageImage(isOpen, rstageNumber);
        }
    }
    
    
    void UpdateStageImage(bool isOpen, int rstageNumber) //스테이지를 업데이트
    {
        if (isOpen)
        {
            // 스테이지가 열렸을 때, 스프라이트를 열린 스테이지의 스프라이트로 변경
            stageButtons[rstageNumber].image.sprite = openedBtnSprite;
            stageTexts[rstageNumber].gameObject.SetActive(true);
        }

    }
   

}
