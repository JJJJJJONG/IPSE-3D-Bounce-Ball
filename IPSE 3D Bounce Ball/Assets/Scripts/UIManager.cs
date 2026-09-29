using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button[] stageButtons = new Button[15];
    public TextMeshProUGUI[] stageTexts = new TextMeshProUGUI[15];

    Sprite openedBtnSprite;
    Sprite lockedBtnSprite;

    private void Awake()
    {
        //스테이지에서 돌아오면 커서가 잠긴 채라 버튼을 누를 수 없었음
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        openedBtnSprite = Resources.Load<Sprite>("Image/OpenedButton");
        lockedBtnSprite = Resources.Load<Sprite>("Image/RockedButton");

        //스테이지 자식 오브젝트(text)를 위한 변수 초기화
        for (int i = 0; i < stageButtons.Length; i++)
        {
            if (stageButtons[i] != null)
                stageTexts[i] = stageButtons[i].GetComponentInChildren<TextMeshProUGUI>(true);
        }
    }

    private void Start()
    {
        //스테이지 초기화
        SetStage();
    }

    //저장된 진행도(클리어한 스테이지)에 맞춰 버튼 이미지와 숫자를 표시
    //(예전에는 비어 있는 starCount 배열을 매 프레임 비교해서 2스테이지는 잠기고 3스테이지가 열려 있었음)
    public void SetStage()
    {
        for (int i = 0; i < stageButtons.Length; i++)
        {
            if (stageButtons[i] == null)
                continue;

            bool isOpen = GameManager.CanPlayStage(i + 1);
            stageButtons[i].image.sprite = isOpen ? openedBtnSprite : lockedBtnSprite;

            if (stageTexts[i] != null)
                stageTexts[i].gameObject.SetActive(isOpen);
        }
    }
}
