using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class ScreenSizeManager : MonoBehaviour
{
    public bool WasListOpened;

    private void Start()
    {
        ResetScreenSize(); //타이머 프로그램 처음 실행할때, 해상도 리셋
        GameManager.Instance.SceneChangeManagerSC.OnSceneLoaded.AddListener(ResetScreenSize, 0);
    }

    /// <summary>
    /// 메뉴 씬에서 리스트를 열어놓고 타이머 씬으로 돌아가는 경우, 다시 해상도를 원상복귀 시키고, <br/>
    /// 리스트가 열려있는지 판단하는 불값도 초기화 시키는 함수
    /// </summary>
    void ResetScreenSize()
    {
        if(GameManager.Instance.SceneChangeManagerSC.CurSceneName == "TimerScene")
        {
            WasListOpened = false;
            SetResolutionKeepTop(200, 100, false);

            //Debug.Log("ResetScreenSIze에 의해 WasListOpened가 False로 변경되었습니다");
        }
    }

    /// <summary>
    /// 버튼에서 호출되는 함수 <br/>
    /// 길이가 늘어나 있으면, 줄이고, <br/>
    /// 줄어들어 있는 경우는 늘림
    /// <para>
    /// 프로젝트의 기본 크기는 200x100
    /// </para>
    /// <para>
    /// 등록 프로그램 하나당 늘어나는 크기는 20으로 설정하였음
    /// </para>
    /// </summary>
    public void OpenClose_List()
    {
        if (WasListOpened)
        {
            SetResolutionKeepTop(200, 100, false);
        }
        else
        {
            SetResolutionKeepTop(200, 100 + 20 * GameManager.Instance.SaveManagerSC.SaveData.RegistedProgramNum, false);
        }

        WasListOpened = !WasListOpened;
        //Debug.Log($"WasListOpened가 {!WasListOpened} 에서 {WasListOpened}으로 변경되었습니다");
    }

    #region 화면 위치를 유지하면서, 해상도만 바꾸는 기능을 위한 코드들
#if UNITY_STANDALONE_WIN

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
        int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool AdjustWindowRectEx(ref RECT lpRect, uint dwStyle, bool bMenu, uint dwExStyle);

    [DllImport("user32.dll")]
    private static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

    private static readonly IntPtr HWND_TOP = IntPtr.Zero;
    private const uint SWP_SHOWWINDOW = 0x0040;

    private const int GWL_STYLE = -16;
    private const int GWL_EXSTYLE = -20;

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

#endif

    /// <summary>
    /// 창 해상도를 변경하고, 상단 위치를 고정시킵니다. (오차 보정 포함)
    /// </summary>
    public void SetResolutionKeepTop(int newClientWidth, int newClientHeight, bool fullScreen = false)
    {
#if UNITY_EDITOR

#else
        StartCoroutine(SetResolutionAndAdjustWindow(newClientWidth, newClientHeight, fullScreen));
#endif
    }

    private IEnumerator SetResolutionAndAdjustWindow(int newClientWidth, int newClientHeight, bool fullScreen)
    {
#if UNITY_STANDALONE_WIN
        // 현재 창 위치 저장
        IntPtr windowHandle = GetActiveWindow();
        RECT rect = new RECT();
        if (!GetWindowRect(windowHandle, ref rect))
        {
            Debug.LogError("GetWindowRect 실패");
            yield break;
        }

        int currentX = rect.left;
        int currentTopY = rect.top;

        // 해상도 변경
        Screen.SetResolution(newClientWidth, newClientHeight, fullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);

        // 한 프레임 대기
        yield return null;

        // 창 스타일 가져오기
        uint style = GetWindowLong(windowHandle, GWL_STYLE);
        uint exStyle = GetWindowLong(windowHandle, GWL_EXSTYLE);

        // 클라이언트 영역을 기준으로 전체 창 크기 보정
        RECT clientRect = new RECT
        {
            left = 0,
            top = 0,
            right = newClientWidth,
            bottom = newClientHeight
        };
        if (!AdjustWindowRectEx(ref clientRect, style, false, exStyle))
        {
            Debug.LogError("AdjustWindowRectEx 실패");
            yield break;
        }

        int adjustedWidth = clientRect.right - clientRect.left;
        int adjustedHeight = clientRect.bottom - clientRect.top;

        // 창 위치 및 크기 설정
        SetWindowPos(windowHandle, HWND_TOP, currentX, currentTopY, adjustedWidth, adjustedHeight, SWP_SHOWWINDOW);
#else
        Debug.LogWarning("현재 플랫폼에서는 지원하지 않습니다.");
        yield break;
#endif
    }
#endregion
}
