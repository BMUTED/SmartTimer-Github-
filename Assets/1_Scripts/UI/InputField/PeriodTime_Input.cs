using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MenuScene 속 PeriodTime_InputField의 여러가지 함수들을 담고있는 스크립트
/// </summary>
public class PeriodTime_Input : MonoBehaviour
{
    /// <summary>
    /// InputField에 아무것도 안 적을시에 나오는 설명용 텍스트
    /// </summary>
    [SerializeField] TextMeshProUGUI PlaceHolder;

    /// <summary>
    /// InputField에 사용자가 적은 글자가 표시되는 텍스트
    /// </summary>
    [SerializeField] TMP_InputField TimeOut_InputField;

    private void Start()
    {
        PlaceHolder.text = $"Enter TimeOut ({GameManager.Instance.TimerManagerSC.ProgramCheckSC.DefaultPeriodTime}s)...";
    }

    #region InputField 내에 있는 인스펙터 이벤트에서 사용되는 함수들

    /// <summary>
    /// 입력된 문자열을 실수형으로 바꾸어 DefaultPeriodTime에 저장하고, InputField 텍스트 창에, 저장되었음을 표기하는 함수
    /// </summary>
    /// <param name="InputTime"></param>
    public void SetPeriodTime(string InputTime)
    {
        float.TryParse(InputTime, out GameManager.Instance.TimerManagerSC.ProgramCheckSC.DefaultPeriodTime);
        TimeOut_InputField.text = string.Empty;

        PlaceHolder.text = $"-TimeOut : {GameManager.Instance.TimerManagerSC.ProgramCheckSC.DefaultPeriodTime} (s)-";

        Debug.Log($"DefaultPeriodTime을 {GameManager.Instance.TimerManagerSC.ProgramCheckSC.DefaultPeriodTime}로 저장하였습니다");
    }
    #endregion
}
