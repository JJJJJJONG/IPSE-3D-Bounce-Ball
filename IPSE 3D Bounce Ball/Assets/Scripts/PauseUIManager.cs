using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseUIManager : MonoBehaviour, IPointerClickHandler
{
    GameObject pauseUI;
    Transform homeButton;
    Transform retryButton;
    AudioManager audioManager;
    GameManager gameManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
        pauseUI = GameObject.Find("PauseUI");
        homeButton = pauseUI.transform.Find("HomeButton");
        retryButton = pauseUI.transform.Find("RetryButton");

        retryButton.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q)) //일시정지 버튼 눌렀을때
        {
            audioManager.PlaySound("Button");
            //마우스 커서 활성화 및 잠금 해제
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //시간 정지
            Time.timeScale = 0;
            //버튼 활성화
            retryButton.gameObject.SetActive(true);
            homeButton.gameObject.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.R)) //일시정지 버튼 눌렀을때
        {
            audioManager.PlaySound("Button");
            //마우스 커서 비활성화
            Cursor.visible = false;
            //시간 재생
            Time.timeScale = 1;
            //버튼 비활성화
            retryButton.gameObject.SetActive(false);
            homeButton.gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    

        string buttonName = eventData.pointerPress.name;


        if (buttonName == "HomeButton")
        {
            //사운드 재생
            audioManager.PlaySound("Button");
            //StageUI로 화면 전환
            SceneManager.LoadScene("StageUI");
            //UI 비활성화
            pauseUI.SetActive(false);
            //시간 재생
            Time.timeScale = 1;

        }
        else if (buttonName == "RetryButton")
        {
            //사운드 재생
            audioManager.PlaySound("Button");
            //플레이 하고있는 화면 다시 재생
            SceneManager.LoadScene(gameManager.stageNumber + 1);
            //UI비활성화
            pauseUI.SetActive(false);
            //시간 재생
            Time.timeScale = 1;
        }

    }
}
