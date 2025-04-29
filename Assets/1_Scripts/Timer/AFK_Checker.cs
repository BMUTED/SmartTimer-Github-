
//���� : IsInputDetected �Ұ��� ����, IsIdle�� True �Ǵ� False�� �����ϴ� Ÿ�̸� �Լ� ��������

using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// ��� �������� �ƴ��� Ȯ���ϴ� ����� �ϴ� ��ũ��Ʈ <br/>
/// ��� ���� Ȯ�ο� Ÿ�̸ӵ� �� ��ũ��Ʈ���� ���ư�
/// </summary>
public class AFK_Checker : MonoBehaviour
{
    [SerializeField] float IdleTimer;

    private Vector2 lastMousePosition;
    bool IsInputDetected = false;

    public bool IsTimerStopBeingAFK { get; private set; }

    private void Start()
    {
        lastMousePosition = GetMousePos();
    }

    private void Update()
    {
        InputDetecting();
        AFKTimerFlowing();
    }

    /// <summary>
    /// Ű���� �Է��̳�, ���콺 Ŭ�� ���� �Է� �Ǵ� ���콺 �������� �ִ��� ������ Ȯ���ϴ� �Լ�
    /// </summary>
    void InputDetecting()
    {
        lastMousePosition = GetMousePos();

        // Ű����, ���콺 Ŭ��, �Ǵ� ���콺 �������� �ִ��� ������ Ȯ��
        //�� �����, ���α׷��� ��Ŀ�� �Ǿ����� ���� ���� Ž������ ����
        if (Input.anyKey || Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2) || GetMousePos() != lastMousePosition) 
        {
            IsInputDetected = true;
            SetIsTimerStopBeingAFK(false);
            Debug.Log("Ű���� �Է��� �����Ǿ�, ��� ���°� �����Ǿ����ϴ�");
        }
        else //�ƹ� �Է�, ������ ������ ���, ����� ���·� �Ǵ�
        {
            IsInputDetected = false;
        }
    }

    void AFKTimerFlowing()
    {
        if (!IsInputDetected) //����ڰ� ��� �����϶�
        {
            IdleTimer += Time.deltaTime;
            if (IdleTimer >= GameManager.Instance.TimerManagerSC.ProgramCheckSC.DefaultPeriodTime) //DefaultPeriodTime �� �̻� ��� ������ ���
            {
                IdleTimer = 0; //��� Ÿ�̸� �ʱ�ȭ
                SetIsTimerStopBeingAFK(true);
                Debug.Log($"{GameManager.Instance.TimerManagerSC.ProgramCheckSC.DefaultPeriodTime} ����, �Է��� ���, Ÿ�̸Ӱ�, ��� ���¶� �ǴܵǾ����ϴ�");
            }
        }

        else
        {
            IdleTimer = 0;
        }
    }

    /// <summary>
    /// IsTimerStopBeingAFK�� ���� �����ϰ� ������, ȣ���ϴ� �Լ� <br/>
    /// Ÿ�̸� ��� ���ο� ����, �� ����ȭ�� ����, ������������ �� �Լ��� ����, �Ұ��� �ٲ�߸� ��
    /// </summary>
    /// <param name="Value"></param>
    public void SetIsTimerStopBeingAFK(bool Value)
    {
        IsTimerStopBeingAFK = Value;

        GameManager.Instance.TimerManagerSC.TimerFlowSC.TimeFlowingCheck();
    }

    #region ���콺 ��ġ�� ���ϴ� ��ɰ� ���õ� �ڵ��
#if UNITY_STANDALONE_WIN

    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int X;
        public int Y;
    }

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT pos);

#endif
    /// <summary>
    /// ���� ���콺 ��ġ�� Vector2�� ��ȯ�մϴ�. <br/>
    /// WindowAPI�� ����� ������� Input.mousePos�� �ٸ��� ���콺�� ���α׷� ȭ�� ������ ������, ��ġ�� ��ȯ�մϴ�
    /// </summary>
    private Vector2 GetMousePos()
    {
#if UNITY_STANDALONE_WIN
        if (GetCursorPos(out POINT point))
        {
            return new Vector2(point.X, point.Y);
        }
        else
        {
            Debug.LogWarning("Ŀ�� ��ġ�� �������� ���߽��ϴ�.");
            return Vector2.zero;
        }
#else
        Vector3 pos = Input.mousePosition;
        return new Vector2(pos.x, pos.y);
#endif
    }
    #endregion
}
