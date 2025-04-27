using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerFlow : MonoBehaviour
{
    public bool IsTimeFlowing; //특정 프로그램을 켜놔서, 타이머의 시간이 흘러야 하는 경우

    //특정 프로그램을 켜놓은 시간을 저장할 변수들
    public float HourTime;
    public float MinuteTime;
    public float SecondTime;

    private void Update()
    {
        if (IsTimeFlowing)
        {
            TimeFlowing();
        }
    }

    /// <summary>
    /// 타이머의 시간이 흐르게 만드는 함수
    /// 초 분 시 단위로 나눠서, 60초인 경우, 1분으로 바뀌는 그런 기능들까지 포함함
    /// </summary>
    void TimeFlowing()
    {
        SecondTime += Time.deltaTime;

        //시간이 60초를 넘어간 경우
        if(SecondTime >= 60)
        {
            SecondTime -= 60; //60만큼 빼기 (소숫값이 사라질 수 있으니 0 으로 수동 초기화하지 않을것임)
            MinuteTime += 1;
            if (MinuteTime >= 60)
            {
                MinuteTime -= 60;
                HourTime += 1;
            }
        }
    }

    /// <summary>
    /// IsTimeFlowing의 값을 바꾸고 싶을때 호출하는 함수
    /// IsTimeFlowing의 값에 따라, 타이머의 색이 변해야 하기 때문에 함수로 만들었음
    /// </summary>
    /// <param name="Value"></param>
    public void ChangeTimeFlowing(bool Value)
    {
        IsTimeFlowing = Value;

        //글자 색을 시간이 흐르는지 아닌지 여부에 따라 변경
        if (Value) //IsTimeFlowing == True 일때
        {
            TimerManager.Instance.ShowTimerSC.TimerText.color = TimerManager.Instance.TimerColorSC.TimeFlowingColor;
        }
        else //IsTimeFlowing == False 일때
        {
            TimerManager.Instance.ShowTimerSC.TimerText.color = TimerManager.Instance.TimerColorSC.TimeStopColor;
        }
    }
}
