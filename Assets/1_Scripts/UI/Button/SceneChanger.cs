using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 씬을 변경할 버튼에 부착해놓고 사용하는 스크립트
/// 함수들을 호출하는 건, Button의 Onclick 이벤트에서 실행함
/// </summary>
public class SceneChanger : MonoBehaviour
{
    public void ChangeSceneTo(string SceneName)
    {
        GameManager.Instance.SceneChangeManagerSC.ChangeScene(SceneName);
    }
}
