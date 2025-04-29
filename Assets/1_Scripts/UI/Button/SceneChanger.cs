using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���� ������ ��ư�� �����س��� ����ϴ� ��ũ��Ʈ
/// �Լ����� ȣ���ϴ� ��, Button�� Onclick �̺�Ʈ���� ������
/// </summary>
public class SceneChanger : MonoBehaviour
{
    public void ChangeSceneTo(string SceneName)
    {
        GameManager.Instance.SceneChangeManagerSC.ChangeScene(SceneName);
    }
}
