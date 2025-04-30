#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

using UnityEngine;
using System;
using System.Runtime.InteropServices;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AlwaysOnTop : MonoBehaviour
{
    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    static readonly IntPtr HWND_NOT_TOPMOST = new IntPtr(-2);
    const uint SWP_NOMOVE = 0x0002;
    const uint SWP_NOSIZE = 0x0001;
    const uint SWP_SHOWWINDOW = 0x0040;

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

    [DllImport("kernel32.dll")]
    static extern int GetCurrentThreadId();

    delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

    IntPtr windowHandle;

    void Start()
    {
#if UNITY_EDITOR

#else
            windowHandle = GetWindowHandle();
            SetTopmostAccordingToState();

            GameManager.Instance.TimerManagerSC.ProgramCheckSC.OnFocusProcessChanged.AddListener(SetTopmostAccordingToState, 0);                 
#endif
    }

    static IntPtr GetWindowHandle()
    {
        IntPtr hwnd = IntPtr.Zero;
        int threadId = GetCurrentThreadId();

        EnumThreadWindows(threadId, (hWnd, lParam) =>
        {
            if (hwnd == IntPtr.Zero)
                hwnd = hWnd;
            return true;
        }, IntPtr.Zero);

        return hwnd;
    }

    //원도우를 최상단에 띄울 때 사용하는 함수
    void SetTopmostAccordingToState()
    {
        SetWindowPos(windowHandle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
    }
}

#endif
