using System;
using TMPro;
using UnityEngine;

public class ShowTimer : MonoBehaviour
{
    //시간을 표시할 타이머 텍스트
    public TextMeshProUGUI TimerText;

    private void Update()
    {
        equalTimerOnText();
    }

    void equalTimerOnText()
    {
        // TF == TimerFlow
        TimerFlow TF = TimerManager.Instance.TimerFlowSC;

        //정수값만 따로 추출해낸 시간 변수들
        float Hour = MathF.Truncate(TF.HourTime);
        float Min = MathF.Truncate(TF.MinuteTime);
        float Sec = MathF.Truncate(TF.SecondTime);

        TimerText.text = $"Timer : {(int)Hour:D2}:{(int)Min:D2}:{(int)Sec:D2}";
    }
}
