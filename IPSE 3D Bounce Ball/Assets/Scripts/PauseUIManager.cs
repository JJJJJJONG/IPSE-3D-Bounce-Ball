using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//PauseUI, HomeButton, RetryButton 세 오브젝트에 붙어 있음
//키 입력(Q/R)은 PauseUI에 붙은 것만 처리하고, 버튼에 붙은 것은 클릭만 처리
//(예전에는 세 개가 모두 키를 처리해서 효과음이 겹치고, 버튼의 Awake가 늦게 실행되면 버튼이 다시 숨겨졌음)
public class PauseUIManager : MonoBehaviour, IPointerClickHandler
{
    Transform homeButton;
    Transform retryButton;
    bool isController;
    bool isPaused;

    private void Awake()
    {
        isController = gameObject.name == "PauseUI";
        if (!isController)
            return;

        homeButton = transform.Find("HomeButton");
        retryButton = transform.Find("RetryButton");
        SetButtons(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (!isController)
            return;

        //GetKey는 누르고 있는 동안 매 프레임 실행돼서 효과음이 계속 반복됐음 -> GetKeyDown
        if (Input.GetKeyDown(KeyCode.Q) && !isPaused) //일시정지 버튼 눌렀을때
        {
            isPaused = true;
            PlayButtonSound();
            //마우스 커서 활성화 및 잠금 해제
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //시간 정지
            Time.timeScale = 0;
            //버튼 활성화
            SetButtons(true);
        }
        else if (Input.GetKeyDown(KeyCode.R) && isPaused) //다시 시작 버튼 눌렀을때
        {
            isPaused = false;
            PlayButtonSound();
            //마우스 커서 비활성화 및 다시 잠금 (잠그지 않으면 커서가 창 밖으로 나감)
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            //시간 재생
            Time.timeScale = 1;
            //버튼 비활성화
            SetButtons(false);
        }
    }

    void SetButtons(bool active)
    {
        if (homeButton != null) homeButton.gameObject.SetActive(active);
        if (retryButton != null) retryButton.gameObject.SetActive(active);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        string buttonName = gameObject.name;

        if (buttonName == "HomeButton")
        {
            //사운드 재생
            PlayButtonSound();
            //시간 재생
            Time.timeScale = 1;
            //StageUI로 화면 전환
            SceneManager.LoadScene("StageUI");
        }
        else if (buttonName == "RetryButton")
        {
            //사운드 재생
            PlayButtonSound();
            //시간 재생
            Time.timeScale = 1;
            //플레이 하고있는 화면 다시 재생 (예전에는 항상 1Stage를 다시 불러왔음)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    static void PlayButtonSound()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySound("Button");
    }
}
