using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager_OG : MonoBehaviour
{
    #region 변수들
    /// <summary>
    /// 씬이 완전히 로딩되었을 때, 호출되는 이벤트
    /// </summary>
    public event Action OnSceneLoaded;

    /// <summary>
    /// 현재 씬 이름을 반환하는 변수 (씬이 변경되는 도중에는, 로딩이 끝나기 전에는 변경 당하고 있는 씬의 이름이 나옴)
    /// </summary>
    public string CurSceneName;
    #endregion 

    /// <summary>
    /// 씬을 비동기로 로드합니다.
    /// </summary>
    /// <param name="sceneName">로드할 씬 이름</param>
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        // 씬 비동기 로드 시작 (LoadSceneMode.Single로 현재 씬을 덮어쓴다)
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        // 씬이 바로 활성화되는 걸 막는다
        asyncOperation.allowSceneActivation = false;

        // 로딩이 완료될 때까지 대기
        while (asyncOperation.progress < 0.9f)
        {
            // 로딩 진행률 업데이트 가능 (0.0 ~ 0.9까지 감)
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

            // (필요하면 여기에 로딩 UI 업데이트 코드 삽입)

            yield return null;
        }

        // 로딩 완료 상태(progress == 0.9) 도달했으므로, 이제 씬 활성화를 허용
        asyncOperation.allowSceneActivation = true;

        // 아주 중요: 씬이 실제로 "활성화"될 때까지 한 프레임 대기
        yield return null;

        // 여기서부터는 새 씬의 오브젝트들이 전부 살아있는 상태
        CurSceneName = sceneName;
        OnSceneLoaded?.Invoke(); // 안전하게 호출: 이제 FindWithTag() 성공함

        // (필요하면 여기 이후에 추가 처리 가능)

        // asyncOperation이 완전히 끝날 때까지 기다릴 수도 있음
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
