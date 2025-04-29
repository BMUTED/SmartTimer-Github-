using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// MenuScene 속 PeriodTime_InputField의 여러가지 함수들을 담고있는 스크립트
/// </summary>
public class PeriodTime_Input : MonoBehaviour
{
    /// <summary>
    /// InputField에 아무것도 안 적을시에 나오는 설명용 텍스트
    /// </summary>
    [SerializeField] TextMeshProUGUI PlaceHolder;

    private void Start()
    {
        PlaceHolder.text = $"Enter TimeOut ({GameManager.Instance.TimerManagerSC.ProgramCheckSC.DefaultPeriodTime}s)...";
    }

    /// <summary>
    /// 입력된 문자열을 실수형으로 바꾸어 DefaultPeriodTime에 저장하는 기능
    /// </summary>
    /// <param name="InputTime"></param>
    public void SetPeriodTime(string InputTime)
    {
        float.TryParse(InputTime, out GameManager.Instance.TimerManagerSC.ProgramCheckSC.DefaultPeriodTime);
        Debug.Log($"DefaultPeriodTime을 {InputTime}로 저장하였습니다");
    }
}
