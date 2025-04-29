using System;
using TMPro;
using UnityEngine;

public class ShowTimer : MonoBehaviour
{
    public GameObject DataObject;

    //시간을 표시할 타이머 텍스트
    public TextMeshProUGUI TimerText;

    private void Start()
    {
        GameManager.Instance.SceneChangeManagerSC.OnSceneLoaded.AddListener(ResearchTimerText, 10);
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

    void EqualTimerOnText()
    {
        // TF == TimerFlow
        TimerFlow TF = GameManager.Instance.TimerManagerSC.TimerFlowSC;

        //정수값만 따로 추출해낸 시간 변수들
        float Hour = MathF.Truncate(TF.HourTime);
        float Min = MathF.Truncate(TF.MinuteTime);
        float Sec = MathF.Truncate(TF.SecondTime);

        if(TimerText != null)
        {
            TimerText.text = $"Timer : {(int)Hour:D2}:{(int)Min:D2}:{(int)Sec:D2}";
        }
    }
}
