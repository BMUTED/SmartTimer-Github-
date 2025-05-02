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
    /// ��� ��ư�� �ؽ�Ʈ��, ����� ���α׷��� �̸����� �ٲ��ִ� �Լ�
    /// </summary>
    void ChangeButtonText()
    {
        if((ProgramCheckSC.TargetProcessName.Count-1) >= Index) 
        {
            ButtonText.text = ProgramCheckSC.TargetProcessName[Index];
        }
    }

    /// <summary>
    /// ���� ��Ŀ�� �� â��, ���ο� ��� ���α׷����� �����ϴ� �Լ�
    /// </summary>
    public void RegisterNewProgram()
    {
        ProgramCheckSC.RegisterIndex = Index;
        ProgramCheckSC.IsRegistingProgam = true;
        ButtonText.text = "Awaiting For Registing";
    }

    /// <summary>
    /// ��ư�� �������, ȣ��Ǵ�, TimerManager�� ��ϵ� ���α׷��� ����� ����� �ϴ� �Լ�
    /// </summary>
    public void RemoveCurrentProgramOnButton()
    {
        if(GameManager.Instance.TimerManagerSC.ProgramCheckSC.TargetProcessName.Count > Index)
        {
            GameManager.Instance.TimerManagerSC.ProgramCheckSC.TargetProcessName.RemoveAt(Index);
        }
        else
        {  
            Debug.Log($"���� ��� ���μ��� ����Ʈ ũ���� : {GameManager.Instance.TimerManagerSC.ProgramCheckSC.TargetProcessName.Count}���� ��� ��ư �ε��� ���� {Index}�� �� ũ�ų� ���Ƽ�, ����Ʈ �� �� ���� ����� ������� ����");
        }
    }
}
