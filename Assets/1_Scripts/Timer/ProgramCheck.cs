using System;
using System.Collections.Generic;
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
    [Tooltip ("절대 인스펙터에서 수정하지 말건, 디버깅 용도임")]
    public List<string> TargetProcessName; // 띄워져있을 때, 타이머가 돌아가게 만들고 싶은 프로그램의 이름을 확장자 없이 적어야함

    //주기 타이머의 진행 여부
    public bool IsPeriodTimerFlowing;

    [Tooltip ("현재 최상단에 있는 창이 무엇인지 검사할 주기")]
    public float DefaultPeriodTime; //현재 최상단에 있는 창이 무엇인지 검사할 주기
    public float PeriodTimer; //주기 전용 타이머

    [SerializeField] ProcessInfo PreviousProcess;
    [SerializeField] ProcessInfo CurProcess;

    public PriorityEvent OnFocusProcessChanged; //새로운 창을 띄웠을 때 호출될 이벤트

    private void Start()
    {
        PreviousProcess = new ProcessInfo();
        CurProcess = new ProcessInfo();

        TargetProcessName = new List<string>();

        OnFocusProcessChanged.AddListener(StartPeriodTimer, 0);
    }

    private void Update()
    {
        CheckFocusEveryPeriod();

        CheckFocusedProcessIsChanged();
    }

    /// <summary>
    /// 특정 주기마다, 현재 최상단에 띄워진 프로그램이 무엇인지 확인하는 함수
    /// </summary>
    void CheckFocusEveryPeriod()
    {
        if(IsPeriodTimerFlowing)
        {
            PeriodTimer += Time.deltaTime;
            if(PeriodTimer >= DefaultPeriodTime)
            {
                PeriodTimer -= DefaultPeriodTime;
                bool IsTargetProcessOnTop = false;
                for (int Index = 0; Index < TargetProcessName.Count; Index++)
                {
                    IsTargetProcessOnTop = IsTargetWindowFocused(TargetProcessName[Index]);
                    if(IsTargetProcessOnTop) //등록된 프로그램들 중에서, 한가지라도 포커싱 되어있는게 감지된 경우
                    {
                        break; //For문 탈출하기
                    }
                }
                GameManager.Instance.TimerManagerSC.TimerFlowSC.TryChangeTimeFlowing(IsTargetProcessOnTop);
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
                OnFocusProcessChanged.Invoke();
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
        GameManager.Instance.TimerManagerSC.ProgramCheckSC.PeriodTimer = 0;
    }


    #region 프로그램 등록 및, 현재 최상단 창인지 검사하는 함수들

    /// <summary>
    /// 현재 등록되어있는 프로그램이 포커스 되어있는지를 확인하는 함수
    /// </summary>
    bool IsTargetWindowFocused(string ProcessNameToCheck)
    {
        var targetProcesses = Process.GetProcessesByName(ProcessNameToCheck);
        if (targetProcesses.Length == 0) //TargetProcess라는 이름의 프로그램이 하나도 없는 경우
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
    public void RegisterCurrentTopProgram(int Index)
    {
        IntPtr topWindow = GetForegroundWindow();
        GetWindowThreadProcessId(topWindow, out uint processID);

        if(processID != 0) //탐지된 프로세스 ID가 0이 아닐때만 등록 실행 (작업 표시줄, 바탕화면 등은 시스템 PID (0번 또는 없음))
        {
            try
            {
                Process CurProcess = Process.GetProcessById((int)processID);
                if(TargetProcessName.Count > Index) //리스트에 인덱스 번호 자리가 있는 경우
                {
                    TargetProcessName[Index] = CurProcess.ProcessName;

                }
                else //리스트 크기가 작아, 새로운 프로그램을 저장할 수 없는 경우
                {
                    int CreatNum = Index - TargetProcessName.Count; //만들어야할 방 갯수
                    //반복문으로 필요한 만큼 칸을 만들고
                    for (int i = 0; i < (CreatNum + 1); i++)
                    {
                        TargetProcessName.Add(string.Empty); 
                    }

                    //칸이 다 만들어졌으니, 원래 넣을려고 했던 인덱스 번호 자리에 프로그램 이름 저장하기
                    TargetProcessName[Index] = CurProcess.ProcessName;
                }
                

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
