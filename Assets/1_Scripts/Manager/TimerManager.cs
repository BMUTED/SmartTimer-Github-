using UnityEngine;

/// <summary>
/// Ÿ�̸ӿ� ���õ� ���� ��ũ��Ʈ���� �̸� ĳ���صδ� �뵵�� ��ũ��Ʈ
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
    /// ���� ��ũ��Ʈ�� �̸� ĳ���Ҷ�, ���� �Լ�
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
