using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RegisterButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ButtonText;

    public int Index;

    /// <summary>
    /// ���� ��Ŀ�� �� â��, ���ο� ��� ���α׷����� �����ϴ� �Լ�
    /// </summary>
    public void RegisterNewProgram()
    {
        ProgramCheck ProgramCheckSC = GameManager.Instance.TimerManagerSC.ProgramCheckSC;

        ProgramCheckSC.RegisterCurrentTopProgram(Index);
        ButtonText.text = ProgramCheckSC.TargetProcessName[Index];
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
            Debug.Log($"���� ��� ���μ��� ����Ʈ ũ���� : {GameManager.Instance.TimerManagerSC.ProgramCheckSC.TargetProcessName.Count}���� ��� ��ư �ε��� ���� {Index}�� �� ũ�ų� ���Ƽ�, ���� ����� ������� ����");
        }
    }
}
