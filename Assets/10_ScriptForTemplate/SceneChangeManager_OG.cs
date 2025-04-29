using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager_OG : MonoBehaviour
{
    #region ������
    /// <summary>
    /// ���� ������ �ε��Ǿ��� ��, ȣ��Ǵ� �̺�Ʈ
    /// </summary>
    public event Action OnSceneLoaded;

    /// <summary>
    /// ���� �� �̸��� ��ȯ�ϴ� ���� (���� ����Ǵ� ���߿���, �ε��� ������ ������ ���� ���ϰ� �ִ� ���� �̸��� ����)
    /// </summary>
    public string CurSceneName;
    #endregion 

    /// <summary>
    /// ���� �񵿱�� �ε��մϴ�.
    /// </summary>
    /// <param name="sceneName">�ε��� �� �̸�</param>
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        // �� �񵿱� �ε� ���� (LoadSceneMode.Single�� ���� ���� �����)
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        // ���� �ٷ� Ȱ��ȭ�Ǵ� �� ���´�
        asyncOperation.allowSceneActivation = false;

        // �ε��� �Ϸ�� ������ ���
        while (asyncOperation.progress < 0.9f)
        {
            // �ε� ����� ������Ʈ ���� (0.0 ~ 0.9���� ��)
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

            // (�ʿ��ϸ� ���⿡ �ε� UI ������Ʈ �ڵ� ����)

            yield return null;
        }

        // �ε� �Ϸ� ����(progress == 0.9) ���������Ƿ�, ���� �� Ȱ��ȭ�� ���
        asyncOperation.allowSceneActivation = true;

        // ���� �߿�: ���� ������ "Ȱ��ȭ"�� ������ �� ������ ���
        yield return null;

        // ���⼭���ʹ� �� ���� ������Ʈ���� ���� ����ִ� ����
        CurSceneName = sceneName;
        OnSceneLoaded?.Invoke(); // �����ϰ� ȣ��: ���� FindWithTag() ������

        // (�ʿ��ϸ� ���� ���Ŀ� �߰� ó�� ����)

        // asyncOperation�� ������ ���� ������ ��ٸ� ���� ����
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
