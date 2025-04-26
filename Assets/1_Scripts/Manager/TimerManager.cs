using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance = null;
    //���� ������ �ٸ� ������Ʈ���� ������ Ŭ���� ������ �����Ҷ�,�̹� ����Ǿ��ִٸ� ���� ����Ǿ� �ִ� ���� ������ �״�� �����Ѵ�.
    //(�ܼ��� ����, Static(���� ������) Ŭ���� �ȿ� ���� ����ȴٰ� �����ϸ� ��)
    //���� ���뿡 ���� �� ���� ������ ���ӸŴ��� �ν��Ͻ��� �� instance�� ��� �༮�� �����ϰ� �� ���̴�.

    public TimerColor TimerColorSC;

    public ProgramCheck ProgramCheckSC;

    public ShowTimer ShowTimerSC;

    public TimerFlow TimerFlowSC;

    void Awake()
    {
        //�̱��� ���� �ڵ�
        if (null == Instance) //�� Ŭ���� �ν��Ͻ��� ź������ �� �������� instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�
        {
            Instance = this; // �ڽ��� �־��ش�.
            DontDestroyOnLoad(this.gameObject); //�� ��ȯ�� �Ǵ��� �ı����� �ʰ� �Ѵ�.
        }
        //���� �� �̵��� �Ǿ��µ� �� ������ Hierarchy�� GameManager ������ ���� �ִ�.
        else// ���� ���������� instance�� �̹� �ٸ� ������Ʈ�� ����Ǿ��ִٸ�
        {
            //Debug.Log("���ο� GameManager �̱��濡 ���� ������");
            Destroy(this.gameObject); // �ڽ�(���ο� ���� GameManager)�� �������ش�.
        }

        ResetScripts();
    }

    /// <summary>
    /// ���� ��ũ��Ʈ�� �̸� ĳ���Ҷ�, ���� �Լ�
    /// </summary>
    void ResetScripts()
    {
        if (TimerColorSC == null)
        {
            TimerColorSC = GetComponent<TimerColor>();
        }
        if (ProgramCheckSC == null)
        {
            ProgramCheckSC = GetComponent<ProgramCheck>();
        }
        if (ShowTimerSC == null)
        {
            ShowTimerSC = GetComponent<ShowTimer>();
        }
        if (TimerFlowSC == null)
        {
            TimerFlowSC = GetComponent<TimerFlow>();
        }
    }
}
