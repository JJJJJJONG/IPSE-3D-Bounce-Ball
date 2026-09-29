using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IPointerClickHandler
{
    //클리어한 가장 높은 스테이지 번호 (PlayerPrefs에 저장)
    public const string ClearedStageKey = "ClearedStage";
    //빌드 순서: 0 MainMenuUI, 1 StageUI, 2부터 스테이지 (1Stage = 2)
    const int FirstStageBuildIndex = 2;

    public int stageNumber = 1; //현재 스테이지 번호 (1Stage = 1)
    public int stageIndex = 15; //스테이지 버튼 개수

    int totalStarCount; //이 스테이지의 별 개수
    Ball ball;
    //한 씬에 GameManager가 여러 개 있어도 씬 로드는 한 번만
    static bool isLoading;

    void Awake()
    {
        isLoading = false;
        //인스펙터 값은 모든 씬에서 1로 저장돼 있어서 2Stage에서도 1Stage로 처리됐음 -> 실제 씬 순서로 계산
        stageNumber = SceneManager.GetActiveScene().buildIndex - FirstStageBuildIndex + 1;
    }

    void Start()
    {
        //스테이지 씬에만 Ball이 있음 (메인메뉴/스테이지 선택 화면에서는 아무것도 하지 않음)
        ball = FindObjectOfType<Ball>();
        if (ball != null)
        {
            //각 스테이지의 별 개수가 몇개인지 저장
            totalStarCount = GameObject.FindGameObjectsWithTag("Star").Length;
        }
    }

    void Update()
    {
        if (ball != null && !isLoading && totalStarCount > 0 && ball.StarCount >= totalStarCount)
        {
            ClearStage();
        }
    }

    private void OnTriggerEnter(Collider other) //떨어졌을 때 리스폰
    {
        if (other.CompareTag("Player"))
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        string buttonName = gameObject.name;

        if (buttonName == "StartButton")
        {
            PlayButtonSound();
            LoadScene("StageUI");
        }
        //잠긴 스테이지나 아직 없는 스테이지(3Stage~15Stage) 버튼은 무시
        else if (int.TryParse(buttonName.Replace("Stage", ""), out int number) && CanPlayStage(number))
        {
            PlayButtonSound();
            LoadScene(buttonName);
        }
    }

    void ClearStage()
    {
        if (stageNumber > PlayerPrefs.GetInt(ClearedStageKey, 0))
        {
            PlayerPrefs.SetInt(ClearedStageKey, stageNumber);
            PlayerPrefs.Save();
        }

        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene < SceneManager.sceneCountInBuildSettings)
            LoadScene(nextScene);
        else
            LoadScene("StageUI"); //마지막 스테이지 클리어
    }

    //1스테이지는 항상 열려 있고, 이전 스테이지를 클리어해야 다음 스테이지가 열림
    public static bool IsStageUnlocked(int number)
    {
        return number <= PlayerPrefs.GetInt(ClearedStageKey, 0) + 1;
    }

    public static bool CanPlayStage(int number)
    {
        return IsStageUnlocked(number) && Application.CanStreamedLevelBeLoaded(number + "Stage");
    }

    static void PlayButtonSound()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySound("Button");
    }

    static void LoadScene(int buildIndex)
    {
        if (isLoading) return;
        isLoading = true;
        SceneManager.LoadScene(buildIndex);
    }

    static void LoadScene(string sceneName)
    {
        if (isLoading) return;
        isLoading = true;
        SceneManager.LoadScene(sceneName);
    }
}
