using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerPauseButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ButtonText;

    /// <summary>
    /// 타이머를 일시정지하거나, 일시정지를 풀 때 호출하는 함수
    /// </summary>
    public void PausePlay_TimerButton()
    {
        //타이머가 일시정지 되어있는 상태에서 함수를 호출했을 때 (사용자가, 타이머를 재생 시키려고 함수를 호출한 경우)
        if (GameManager.Instance.TimerManagerSC.TimerFlowSC.IsTimerForceStopped)
        {
            ButtonText.text = ">>"; //재생 상태 모양인 >>자로 텍스트 표시 변경
        }
        //타이머가 일시정지 되어있지 않은 상태에서 함수를 호출했을 때 (사용자가, 타이머를 정지시키려고 함수를 호출한 경우)
        else
        {
            ButtonText.text = "ll"; //정지 상태 모양인 ll자로 텍스트 표시 변경
        }

        GameManager.Instance.TimerManagerSC.TimerFlowSC.IsTimerForceStopped = !GameManager.Instance.TimerManagerSC.TimerFlowSC.IsTimerForceStopped;
    }
}
