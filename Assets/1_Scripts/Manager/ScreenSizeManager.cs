using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ScreenSizeManager : MonoBehaviour
{
    public bool WasListOpened;

    private void Start()
    {
        ResetScreenSize(); //Ÿ�̸� ���α׷� ó�� �����Ҷ�, �ػ� ����
        GameManager.Instance.SceneChangeManagerSC.OnSceneLoaded.AddListener(ResetScreenSize, 0);
    }

    /// <summary>
    /// �޴� ������ ����Ʈ�� ������� Ÿ�̸� ������ ���ư��� ���, �ٽ� �ػ󵵸� ���󺹱� ��Ű��, <br/>
    /// ����Ʈ�� �����ִ��� �Ǵ��ϴ� �Ұ��� �ʱ�ȭ ��Ű�� �Լ�
    /// </summary>
    void ResetScreenSize()
    {
        WasListOpened = false;
        SetResolutionKeepTop(200, 100, false);
    }

    /// <summary>
    /// ��ư���� ȣ��Ǵ� �Լ� <br/>
    /// ���̰� �þ ������, ���̰�, <br/>
    /// �پ��� �ִ� ���� �ø�
    /// <para>
    /// ������Ʈ�� �⺻ ũ��� 200x100
    /// </para>
    /// <para>
    /// ���� �𸣰�����, �Ʒ� ������ ��ġ ����� �Բ� ���� �ػ� ���� �Լ������� ũ�Ⱑ, x,y ���� 50 ���� �۰� ������ ������ ���� <br/>
    /// ���, 200 x 100 / 200 x 400 ��� 250 x 150 / 250 x 450 ���� �����ϵ��� �������
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
            SetResolutionKeepTop(200, 400, false);
        }

        WasListOpened = !WasListOpened;
    }

    #region ȭ�� ��ġ�� �����ϸ鼭, �ػ󵵸� �ٲٴ� ����� ���� �ڵ��
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
    /// â �ػ󵵸� �����ϰ�, ��� ��ġ�� ������ŵ�ϴ�. (���� ���� ����)
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
        // ���� â ��ġ ����
        IntPtr windowHandle = GetActiveWindow();
        RECT rect = new RECT();
        if (!GetWindowRect(windowHandle, ref rect))
        {
            Debug.LogError("GetWindowRect ����");
            yield break;
        }

        int currentX = rect.left;
        int currentTopY = rect.top;

        // �ػ� ����
        Screen.SetResolution(newClientWidth, newClientHeight, fullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);

        // �� ������ ���
        yield return null;

        // â ��Ÿ�� ��������
        uint style = GetWindowLong(windowHandle, GWL_STYLE);
        uint exStyle = GetWindowLong(windowHandle, GWL_EXSTYLE);

        // Ŭ���̾�Ʈ ������ �������� ��ü â ũ�� ����
        RECT clientRect = new RECT
        {
            left = 0,
            top = 0,
            right = newClientWidth,
            bottom = newClientHeight
        };
        if (!AdjustWindowRectEx(ref clientRect, style, false, exStyle))
        {
            Debug.LogError("AdjustWindowRectEx ����");
            yield break;
        }

        int adjustedWidth = clientRect.right - clientRect.left;
        int adjustedHeight = clientRect.bottom - clientRect.top;

        // â ��ġ �� ũ�� ����
        SetWindowPos(windowHandle, HWND_TOP, currentX, currentTopY, adjustedWidth, adjustedHeight, SWP_SHOWWINDOW);
#else
        Debug.LogWarning("���� �÷��������� �������� �ʽ��ϴ�.");
        yield break;
#endif
    }
#endregion
}
