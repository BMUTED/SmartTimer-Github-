using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// MenuScene �� PeriodTime_InputField�� �������� �Լ����� ����ִ� ��ũ��Ʈ
/// </summary>
public class PeriodTime_Input : MonoBehaviour
{
    /// <summary>
    /// InputField�� �ƹ��͵� �� �����ÿ� ������ ����� �ؽ�Ʈ
    /// </summary>
    [SerializeField] TextMeshProUGUI PlaceHolder;

    private void Start()
    {
        PlaceHolder.text = $"Enter TimeOut ({GameManager.Instance.TimerManagerSC.ProgramCheckSC.DefaultPeriodTime}s)...";
    }

    /// <summary>
    /// �Էµ� ���ڿ��� �Ǽ������� �ٲپ� DefaultPeriodTime�� �����ϴ� ���
    /// </summary>
    /// <param name="InputTime"></param>
    public void SetPeriodTime(string InputTime)
    {
        float.TryParse(InputTime, out GameManager.Instance.TimerManagerSC.ProgramCheckSC.DefaultPeriodTime);
        Debug.Log($"DefaultPeriodTime�� {InputTime}�� �����Ͽ����ϴ�");
    }
}
