using TMPro;
using UnityEngine;

/// <summary>
/// OpenList_Button에 달아야 하는 스크립트 컴포넌트로  <br/>
/// 해상도 크기를 줄이거나 늘리는 기능을 하는 GameManager속 기능을 버튼에서 실행할 수 있도록 만들어줌
/// </summary>
public class OpenList_Button : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ButtonText;

    private void Start()
    {
        //MenuScene으로 씬을 변경했을때, 자동으로 프로그램 크기가, 등록 버튼 갯수에 맞춰 변하도록 만들기
        OpenClose_List_Button();
    }

    public void OpenClose_List_Button()
    {
        GameManager.Instance.ScreenSizeManagerSC.OpenClose_List();
        //텍스트
        if (GameManager.Instance.ScreenSizeManagerSC.WasListOpened == false)
        {
            ButtonText.text = "Open List";
            //Debug.Log($"리스트 닫힘 {GameManager.Instance.ScreenSizeManagerSC.WasListOpened}");
        }
        else
        {
            ButtonText.text = "Close List";
            //Debug.Log($"리스트 열음 {GameManager.Instance.ScreenSizeManagerSC.WasListOpened}");
        }
    }
}
