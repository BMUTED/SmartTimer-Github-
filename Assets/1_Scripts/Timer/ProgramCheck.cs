using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

[Serializable]
public class ProcessInfo
{
    public Process Process;

    public string ProcessName;
    public uint ProcessID;
}

//이 스크립트에서 할일 : 특정 주기마다, 등록된 창이 최상단에 있는지 검사하는 함수 완성하기
public class ProgramCheck : MonoBehaviour
{
    [SerializeField] string targetProcessName = "Unity"; // 띄워져있을 때, 타이머가 돌아가게 만들고 싶은 프로그램의 이름을 확장자 없이 적어야함

    //주기 타이머의 진행 여부
    public bool IsPeriodTimerFlowing;

    [Tooltip ("현재 최상단에 있는 창이 무엇인지 검사할 주기")]
    [SerializeField] float DefaultPeriodTime; //현재 최상단에 있는 창이 무엇인지 검사할 주기
    public float PeriodTimer; //주기 전용 타이머

    [SerializeField] ProcessInfo PreviousProcess;
    [SerializeField] ProcessInfo CurProcess;

    public event Action OnFocusProcessChanged; //새로운 창을 띄웠을 때 호출될 이벤트

    private void Start()
    {
        PreviousProcess = new ProcessInfo();
        CurProcess = new ProcessInfo();

        OnFocusProcessChanged += StartPeriodTimer;
    }

    private void Update()
    {
        CheckFocusEveryPeriod();

        CheckFocusedProcessIsChanged();
    }

    /// <summary>
    /// 특정 주기마다, 현재 최상단에 띄워진 프로그램이 무엇인지 확인하는 함수 (미완)
    /// </summary>
    void CheckFocusEveryPeriod()
    {
        if(IsPeriodTimerFlowing)
        {
            PeriodTimer += Time.deltaTime;
            if(PeriodTimer >= DefaultPeriodTime)
            {
                PeriodTimer -= DefaultPeriodTime;
                TimerManager.Instance.TimerFlowSC.ChangeTimeFlowing(IsTargetWindowFocused());
                IsPeriodTimerFlowing = false;
            }
        }
    }

    /// <summary>
    /// 새로운 창을 클릭하여, 포커스가 바뀌었는지를 감지하는 함수 (Update 단에서 호출 되어야함)
    /// </summary>
    void CheckFocusedProcessIsChanged()
    {
        if(CurProcess.Process != null)
        {
            InitProcessInfo(PreviousProcess, CurProcess.Process, CurProcess.ProcessID);
        }

        IntPtr topWindow = GetForegroundWindow();
        GetWindowThreadProcessId(topWindow, out uint processID);

        InitProcessInfo(CurProcess, Process.GetProcessById((int)processID), processID);

        UnityEngine.Debug.Log($"현재 최상단에 있는 프로그램은 {CurProcess.Process.ProcessName} 입니다");

        if(PreviousProcess.Process != null && CurProcess.Process != null)
        {
            if (PreviousProcess.Process.ProcessName != CurProcess.Process.ProcessName)
            {
                UnityEngine.Debug.Log($"새로운 창을 띄운것을 확인함");
                OnFocusProcessChanged?.Invoke();
            }
        }
    }

    /// <summary>
    /// ProcessInfo 클래스에 들어갈 여러 정보를 한꺼번에 초기화 하는 함수
    /// </summary>
    /// <param name="ProcessInfoToInit"></param>
    /// <param name="ProcessToSave"></param>
    /// <param name="ProcessIDToSave"></param>
    void InitProcessInfo(ProcessInfo ProcessInfoToInit, Process ProcessToSave, uint ProcessIDToSave)
    {
        ProcessInfoToInit.Process = ProcessToSave;
        ProcessInfoToInit.ProcessID = ProcessIDToSave;
        ProcessInfoToInit.ProcessName = ProcessToSave.ProcessName;
    }

    /// <summary>
    /// 주기 타이머를 시작하는 함수
    /// </summary>
    void StartPeriodTimer()
    {
        //주기 타이머가 흐를지에 대한 여부를, 스마트 타이머가 돌아가고 있는지에 대한 반대값으로 설정
        IsPeriodTimerFlowing = true;
        TimerManager.Instance.ProgramCheckSC.PeriodTimer = 0;
    }


    #region 프로그램 등록 및, 현재 최상단 창인지 검사하는 함수들

    /// <summary>
    /// 현재 등록되어있는 프로그램이 포커스 되어있는지를 확인하는 함수
    /// </summary>
    bool IsTargetWindowFocused()
    {
        var targetProcesses = Process.GetProcessesByName(targetProcessName);
        if (targetProcesses.Length == 0)
            return false;

        IntPtr foregroundWindow = GetForegroundWindow();
        GetWindowThreadProcessId(foregroundWindow, out uint foregroundProcessId);

        foreach (var proc in targetProcesses)
        {
            if ((uint)proc.Id == foregroundProcessId)
                return true;
        }

        return false;
    }

    /// <summary>
    /// 가장 최상단에 있는 프로그램의 이름을 가져와 등록하는 함수
    /// </summary>
    void RegisterCurrentTopProgram()
    {
        IntPtr topWindow = GetForegroundWindow();
        GetWindowThreadProcessId(topWindow, out uint processID);

        if(processID != 0) //탐지된 프로세스 ID가 0이 아닐때만 등록 실행 (작업 표시줄, 바탕화면 등은 시스템 PID (0번 또는 없음))
        {
            try
            {
                Process CurProcess = Process.GetProcessById((int)processID);
                targetProcessName = CurProcess.ProcessName; // 자동으로 저장!

                UnityEngine.Debug.Log($"✅ 등록 완료: {CurProcess.ProcessName} (PID {processID})");
            }
            catch
            {
                UnityEngine.Debug.Log("❌ 등록 실패: 프로세스 정보를 가져올 수 없습니다.");
            }
        }
    }

    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", SetLastError = true)]
    static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    #endregion
}
