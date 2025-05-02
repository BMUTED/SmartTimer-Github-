using System;
using TMPro;
using UnityEngine;

public class ShowTimer : MonoBehaviour
{
    SaveDatas Savedata;
    
    public GameObject DataObject;

    //시간을 표시할 타이머 텍스트
    public TextMeshProUGUI TimerText;

    private void Start()
    {
        GameManager.Instance.SceneChangeManagerSC.OnSceneLoaded.AddListener(ResearchTimerText, 10);

        Savedata = GameManager.Instance.SaveManagerSC.SaveData;
    }

    private void Update()
    {
        EqualTimerOnText();
    }

    void ResearchTimerText()
    {
        if(TimerText == null)
        {
            if (GameManager.Instance.SceneChangeManagerSC.CurSceneName == "TimerScene")
            {
                TimerText = GameObject.FindWithTag("DataObject").GetComponent<TimerScene_UI_Data>().TimerText;

                Debug.Log($"타이머 텍스트를 받아왔습니다 / 받아온 텍스트 오브젝트 == {TimerText}");
            }
        }
    }

    /// <summary>
    /// 타이머의 시간과, 타이머 텍스트가 표기하는 시간이 동기화 되도록 만들어주는 함수 <br/>
    /// 시간이 Pause 버튼을 눌러 강제로 멈춘 경우에는 타이머 텍스트를 다르게 표기함
    /// </summary>
    void EqualTimerOnText()
    {
        //타이머가 강제로 멈춘 경우
        if(GameManager.Instance.TimerManagerSC.TimerFlowSC.IsTimerForceStopped)
        {
            if (TimerText != null)
            {
                TimerText.text = $"Time Is Paused";
            }
        }

        else
        {
            //정수값만 따로 추출해낸 시간 변수들
            float Hour = MathF.Truncate(Savedata.HourTime);
            float Min = MathF.Truncate(Savedata.MinuteTime);
            float Sec = MathF.Truncate(Savedata.SecondTime);

            if (TimerText != null)
            {
                TimerText.text = $"Timer : {(int)Hour:D2}:{(int)Min:D2}:{(int)Sec:D2}";
            }
        }
    }
}
