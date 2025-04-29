
//할일 : IsInputDetected 불값에 따라, IsIdle을 True 또는 False로 변경하는 타이머 함수 만들어야함

using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// 잠수 상태인지 아닌지 확인하는 기능을 하는 스크립트 <br/>
/// 잠수 상태 확인용 타이머도 이 스크립트에서 돌아감
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
    /// 키보드 입력이나, 마우스 클릭 등의 입력 또는 마우스 움직임이 있는지 없는지 확인하는 함수
    /// </summary>
    void InputDetecting()
    {
        lastMousePosition = GetMousePos();

        // 키보드, 마우스 클릭, 또는 마우스 움직임이 있는지 없는지 확인
        //이 기능은, 프로그램이 포커싱 되어있지 않은 경우는 탐지하지 못함
        if (Input.anyKey || Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2) || GetMousePos() != lastMousePosition) 
        {
            IsInputDetected = true;
            SetIsTimerStopBeingAFK(false);
            Debug.Log("키보드 입력이 감지되어, 잠수 상태가 해제되었습니다");
        }
        else //아무 입력, 움직임 반응이 없어서, 잠수인 상태로 판단
        {
            IsInputDetected = false;
        }
    }

    void AFKTimerFlowing()
    {
        if (!IsInputDetected) //사용자가 잠수 상태일때
        {
            IdleTimer += Time.deltaTime;
            if (IdleTimer >= GameManager.Instance.TimerManagerSC.ProgramCheckSC.DefaultPeriodTime) //DefaultPeriodTime 초 이상 잠수 상태인 경우
            {
                IdleTimer = 0; //잠수 타이머 초기화
                SetIsTimerStopBeingAFK(true);
                Debug.Log($"{GameManager.Instance.TimerManagerSC.ProgramCheckSC.DefaultPeriodTime} 동안, 입력이 없어서, 타이머가, 잠수 상태라 판단되었습니다");
            }
        }

        else
        {
            IdleTimer = 0;
        }
    }

    /// <summary>
    /// IsTimerStopBeingAFK의 값을 수정하고 싶을때, 호출하는 함수 <br/>
    /// 타이머 재생 여부에 따른, 색 동기화를 위해, 무조건적으로 이 함수를 통해, 불값을 바꿔야만 함
    /// </summary>
    /// <param name="Value"></param>
    public void SetIsTimerStopBeingAFK(bool Value)
    {
        IsTimerStopBeingAFK = Value;

        GameManager.Instance.TimerManagerSC.TimerFlowSC.TimeFlowingCheck();
    }

    #region 마우스 위치를 구하는 기능과 관련된 코드들
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
    /// 현재 마우스 위치를 Vector2로 반환합니다. <br/>
    /// WindowAPI를 사용한 방식으로 Input.mousePos와 다르게 마우스가 프로그램 화면 밖으로 나가도, 위치를 반환합니다
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
            Debug.LogWarning("커서 위치를 가져오지 못했습니다.");
            return Vector2.zero;
        }
#else
        Vector3 pos = Input.mousePosition;
        return new Vector2(pos.x, pos.y);
#endif
    }
    #endregion
}
