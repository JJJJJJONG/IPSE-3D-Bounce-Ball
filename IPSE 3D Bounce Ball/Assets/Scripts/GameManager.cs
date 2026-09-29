using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting; 
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
public class GameManager : MonoBehaviour, IPointerClickHandler
{
    public int[] totalStarCount = new int[15];
    public int[] starCount = new int[15];
    public int stageNumber = 1;
    public int stageIndex = 15;
 
    AudioManager audioManager;

    

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        Scene scene = SceneManager.GetSceneByName("StageUI");
        if (scene.name != "StageUI") 
        {   
            //각 스테이지의 별 개수가 몇개인지 저장
            for(int i=0; i<stageIndex; i++) 
            {
                totalStarCount[i] = GameObject.FindGameObjectsWithTag("Star").Length;
            }
         
             
        }
    }
    void Update() 
    {
        Scene scene = SceneManager.GetSceneByName("StageUI");
        if (scene.name != "StageUI") 
        {
            StageManagement();
        }

       
    }
    private void OnTriggerEnter(Collider other) //떨어졌을 때 리스폰
    {
        if(other.gameObject.tag == "Player") 
        {
            SceneManager.LoadScene(stageNumber+1); //스테이넘버랑 빌드 단계를 같게한다?
        }
    }
    public void OnPointerClick(PointerEventData eventData) 
    {
        string buttonName = eventData.pointerPress.name;
    

        if (buttonName == "StartButton") 
        {
            SceneManager.LoadScene("StageUI");
            audioManager.PlaySound("Button");
        }
        else 
        {
            SceneManager.LoadScene(buttonName);
            audioManager.PlaySound("Button");
            Debug.Log("응애");
        }
        
   
    }
    
    public void StageManagement()
    {
        Ball cmpStarCount = FindObjectOfType<Ball>();

        if (totalStarCount[stageNumber+1] == cmpStarCount.StarCount)
        {   
            ++stageNumber;
            Debug.Log(stageNumber);
            SceneManager.LoadScene(stageNumber+1);
        }
    }

}




