using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// OpenList_Button에 달아야 하는 스크립트 컴포넌트로  <br/>
/// 해상도 크기를 줄이거나 늘리는 기능을 하는 GameManager속 기능을 버튼에서 실행할 수 있도록 만들어줌
/// </summary>
public class OpenList_Button : MonoBehaviour
{
    public void OpenClose_List()
    {
        GameManager.Instance.ScreenSizeManagerSC.OpenClose_List();
    }
}
