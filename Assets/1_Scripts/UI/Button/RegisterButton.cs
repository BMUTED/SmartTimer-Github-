using TMPro;
using UnityEngine;

public class RegisterButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ButtonText;

    ProgramCheck ProgramCheckSC;

    public int Index;

    private void Start()
    {
        ProgramCheckSC = GameManager.Instance.TimerManagerSC.ProgramCheckSC;
        ProgramCheckSC.OnFocusProcessChanged.AddListener(ChangeButtonText, 0);

        if ((ProgramCheckSC.TargetProcessName.Count - 1) >= Index)
        {
            ButtonText.text = ProgramCheckSC.TargetProcessName[Index];
        }
    }

    /// <summary>
    /// 등록 버튼의 텍스트를, 등록한 프로그램의 이름으로 바꿔주는 함수
    /// </summary>
    void ChangeButtonText()
    {
        if((ProgramCheckSC.TargetProcessName.Count-1) >= Index) 
        {
            ButtonText.text = ProgramCheckSC.TargetProcessName[Index];
        }
    }

    /// <summary>
    /// 현재 포커싱 된 창을, 새로운 등록 프로그램으로 설정하는 함수
    /// </summary>
    public void RegisterNewProgram()
    {
        ProgramCheckSC.RegisterIndex = Index;
        ProgramCheckSC.IsRegistingProgam = true;
        ButtonText.text = "Awaiting For Registing";
    }

    /// <summary>
    /// 버튼이 사라질때, 호출되는, TimerManager속 등록된 프로그램을 지우는 기능을 하는 함수
    /// </summary>
    public void RemoveCurrentProgramOnButton()
    {
        if(GameManager.Instance.TimerManagerSC.ProgramCheckSC.TargetProcessName.Count > Index)
        {
            GameManager.Instance.TimerManagerSC.ProgramCheckSC.TargetProcessName.RemoveAt(Index);
        }
        else
        {  
            Debug.Log($"현재 등록 프로세스 리스트 크기인 : {GameManager.Instance.TimerManagerSC.ProgramCheckSC.TargetProcessName.Count}보다 등록 버튼 인덱스 값인 {Index}이 더 크거나 같아서, 리스트 속 값 제거 명령이 이행되지 않음");
        }
    }
}
