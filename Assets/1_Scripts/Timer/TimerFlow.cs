using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerFlow : MonoBehaviour
{
    SaveDatas SaveData;

    public bool IsTimeFlowing { get; private set; }
    public bool TryTimeFlowing { get; private set; } //특정 프로그램을 켜놔서, 타이머의 시간이 흘러야 하는 경우

    public bool IsTimerForceStopped;

    private void Start()
    {
        GameManager.Instance.SceneChangeManagerSC.OnSceneLoaded.AddListener(SyncronizeTimeColor, 11);

        SaveData = GameManager.Instance.SaveManagerSC.SaveData;
    }

    private void Update()
    {
        if (IsTimeFlowing && !IsTimerForceStopped)
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
        SaveData.SecondTime += Time.deltaTime;

        //시간이 60초를 넘어간 경우
        if(SaveData.SecondTime >= 60)
        {
            SaveData.SecondTime -= 60; //60만큼 빼기 (소숫값이 사라질 수 있으니 0 으로 수동 초기화하지 않을것임)
            SaveData.MinuteTime += 1;
            if (SaveData.MinuteTime >= 60)
            {
                SaveData.MinuteTime -= 60;
                SaveData.HourTime += 1;
            }
        }
    }

    /// <summary>
    /// IsTimeFlowing의 값을 바꾸고 싶을때 호출하는 함수
    /// IsTimeFlowing의 값에 따라, 타이머의 색이 변해야 하기 때문에 함수로 만들었음
    /// </summary>
    /// <param name="Value"></param>
    public void TryChangeTimeFlowing(bool Value)
    {
        TryTimeFlowing = Value;

        TimeFlowingCheck();
    }

    /// <summary>
    /// TimeFlowing과 AFKChecker 속 IsTimerStopBeingAFK가 각각 맞아떨어져서, IsTimeFlowing이 True가 될 때인지를 확인할 때, 호출하는 함수 <br/>
    /// TimeFlowSC 속 TryTimeFlowing을 수정하거나, AFK_Checker속 IsTimerStopBeingAFK를 수정할 때, 무조건적으로 호출해야하는 함수이다
    /// </summary>
    public void TimeFlowingCheck()
    {
        //잠수 상태가 아니고, 포커스된 창이 등록되어있는 창일때 (즉, 타이머가 흘러야할 상황일 때)
        if (TryTimeFlowing && GameManager.Instance.TimerManagerSC.AFK_CheckerSC.IsTimerStopBeingAFK == false)
        {
            IsTimeFlowing = true;
        }
        else
        {
            IsTimeFlowing = false;
        }

        if (GameManager.Instance.SceneChangeManagerSC.CurSceneName == "TimerScene")
        {
            //글자 색을 시간이 흐르는지 아닌지 여부에 따라 변경
            if (IsTimeFlowing) //IsTimeFlowing == True 일때
            {
                ChangeTimerColor(true);
            }

            else //IsTimeFlowing == False 일때
            {
                ChangeTimerColor(false);
            }
        }
    }

    /// <summary>
    /// 텍스트 색을 타이머가 흐르고있는지에 대한 여부에 따라 변경하는 함수
    /// SceneChangeManager 속 OnSceneLoaded 이벤트에 구독하여 사용
    /// </summary>
    void SyncronizeTimeColor()
    {
        //글자 색을 시간이 흐르는지 아닌지 여부에 따라 변경
        if (TryTimeFlowing) //IsTimeFlowing == True 일때
        {
            ChangeTimerColor(true);
        }
        else //IsTimeFlowing == False 일때
        {
            ChangeTimerColor(false);
        }
    }

    /// <summary>
    /// 타이머의 색을 변경할때, 사용하는 함수
    /// </summary>
    /// <param name="IsTimerFlowing">True == 푸른색 (타이머 흐를때) / False == 붉은색 (타이머 멈춤)</param>
    public void ChangeTimerColor(bool IsTimerFlowing)
    {
        if (IsTimerFlowing)
        {
            GameManager.Instance.TimerManagerSC.ShowTimerSC.TimerText.color = GameManager.Instance.TimerManagerSC.TimerColorSC.TimeFlowingColor;
        }
        else
        {
            GameManager.Instance.TimerManagerSC.ShowTimerSC.TimerText.color = GameManager.Instance.TimerManagerSC.TimerColorSC.TimeStopColor;
        }
    }
}
