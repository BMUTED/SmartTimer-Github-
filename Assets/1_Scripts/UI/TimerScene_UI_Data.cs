using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScene_UI_Data : MonoBehaviour
{
    public TextMeshProUGUI TimerText;

    private void Start()
    {
        UploadTimerText();
    }

    /// <summary>
    /// ShowTImer에서는 제대로 DataObject를 받아오지 못해, 텍스트가 Null 상태로 지정되는 문제가 있었으므로 <br/>
    /// 이를 대비하여, 조금 늦을지라도 후에 텍스트가 정상적으로 지정되도록 만들어주기 위해 만든 함수
    /// </summary>
    void UploadTimerText()
    {
        if (GameManager.Instance.TimerManagerSC.ShowTimerSC.TimerText == null)
        {
            GameManager.Instance.TimerManagerSC.ShowTimerSC.TimerText = TimerText;
            GameManager.Instance.TimerManagerSC.TimerFlowSC.SyncronizeTimeColor();

            Debug.Log("씬 변경 이후에, TimerText를 인식하지 못해, 보험 함수에서 이를 바로잡았습니다");
        }
    }
}
