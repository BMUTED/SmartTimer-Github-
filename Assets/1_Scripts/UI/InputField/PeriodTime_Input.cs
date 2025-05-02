using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MenuScene �� PeriodTime_InputField�� �������� �Լ����� ����ִ� ��ũ��Ʈ
/// </summary>
public class PeriodTime_Input : MonoBehaviour
{
    /// <summary>
    /// InputField�� �ƹ��͵� �� �����ÿ� ������ ����� �ؽ�Ʈ
    /// </summary>
    [SerializeField] TextMeshProUGUI PlaceHolder;

    /// <summary>
    /// InputField�� ����ڰ� ���� ���ڰ� ǥ�õǴ� �ؽ�Ʈ
    /// </summary>
    [SerializeField] TMP_InputField TimeOut_InputField;

    private void Start()
    {
        PlaceHolder.text = $"Enter TimeOut ({GameManager.Instance.TimerManagerSC.ProgramCheckSC.DefaultPeriodTime}s)...";
    }

    #region InputField ���� �ִ� �ν����� �̺�Ʈ���� ���Ǵ� �Լ���

    /// <summary>
    /// �Էµ� ���ڿ��� �Ǽ������� �ٲپ� DefaultPeriodTime�� �����ϰ�, InputField �ؽ�Ʈ â��, ����Ǿ����� ǥ���ϴ� �Լ�
    /// </summary>
    /// <param name="InputTime"></param>
    public void SetPeriodTime(string InputTime)
    {
        float.TryParse(InputTime, out GameManager.Instance.TimerManagerSC.ProgramCheckSC.DefaultPeriodTime);
        TimeOut_InputField.text = string.Empty;

        PlaceHolder.text = $"-TimeOut : {GameManager.Instance.TimerManagerSC.ProgramCheckSC.DefaultPeriodTime} (s)-";

        Debug.Log($"DefaultPeriodTime�� {GameManager.Instance.TimerManagerSC.ProgramCheckSC.DefaultPeriodTime}�� �����Ͽ����ϴ�");
    }
    #endregion
}
