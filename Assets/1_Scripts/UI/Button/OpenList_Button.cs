using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// OpenList_Button�� �޾ƾ� �ϴ� ��ũ��Ʈ ������Ʈ��  <br/>
/// �ػ� ũ�⸦ ���̰ų� �ø��� ����� �ϴ� GameManager�� ����� ��ư���� ������ �� �ֵ��� �������
/// </summary>
public class OpenList_Button : MonoBehaviour
{
    public void OpenClose_List()
    {
        GameManager.Instance.ScreenSizeManagerSC.OpenClose_List();
    }
}
