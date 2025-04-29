using UnityEngine;

/// <summary>
/// 타이머와 관련된 여러 스크립트들을 미리 캐싱해두는 용도의 스크립트
/// </summary>
public class TimerManager : MonoBehaviour
{
    [SerializeField] GameObject TimerScript;
    [Space (10f)]

    public TimerColor TimerColorSC;
    public ProgramCheck ProgramCheckSC;
    public ShowTimer ShowTimerSC;
    public TimerFlow TimerFlowSC;
    public AFK_Checker AFK_CheckerSC;

    void Start()
    {
        ResetScripts();

        GameManager.Instance.SceneChangeManagerSC.OnSceneLoaded.AddListener(ResetScripts, 0);
    }

    /// <summary>
    /// 여러 스크립트를 미리 캐싱할때, 쓰는 함수
    /// </summary>
    void ResetScripts()
    {
        if (TimerColorSC == null)
        {
            TimerColorSC = TimerScript.GetComponent<TimerColor>();
        }
        if (ProgramCheckSC == null)
        {
            ProgramCheckSC = TimerScript.GetComponent<ProgramCheck>();
        }
        if (ShowTimerSC == null)
        {
            ShowTimerSC = TimerScript.GetComponent<ShowTimer>();
        }
        if (TimerFlowSC == null)
        {
            TimerFlowSC = TimerScript.GetComponent<TimerFlow>();
        }
        if (AFK_CheckerSC == null)
        {
            AFK_CheckerSC = TimerScript.GetComponent<AFK_Checker>();
        }
    }
}
