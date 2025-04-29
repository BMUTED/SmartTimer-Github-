using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 싱글톤화 및 다른 매니저 스크립트들을 캐싱해두는 용도의 스크립트
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    //정적 변수는 다른 오브젝트더라도 동일한 클래스 내에서 선언할때,이미 선언되어있다면 전에 선언되어 있던 정적 변수를 그대로 저장한다.
    //(단순히 말해, Static(정적 변수는) 클래스 안에 따로 저장된다고 생각하면 됨)
    //따라서 내용에 의해 이 게임 내에서 게임매니저 인스턴스는 이 instance에 담긴 녀석만 존재하게 할 것이다.

    public TimerManager TimerManagerSC;
    public SceneChangeManager SceneChangeManagerSC;
    public SaveManager SaveManagerSC;
    public ScreenSizeManager ScreenSizeManagerSC;

    [Space(10f)]
    public TimerScene_UI_Data TimerSceneUI_Data;

    private void Awake()
    {
        //싱글톤 패턴 코드
        if (null == Instance) //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면
        {
            Instance = this; // 자신을 넣어준다.
            DontDestroyOnLoad(this.gameObject); //씬 전환이 되더라도 파괴되지 않게 한다.
        }
        //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameManager 존재할 수도 있다.
        else// 따라서 전역변수인 instance에 이미 다른 오브젝트가 저장되어있다면
        {
            //Debug.Log("새로운 GameManager 싱글톤에 의해 삭제됨");
            Destroy(this.gameObject); // 자신(새로운 씬의 GameManager)을 삭제해준다.
        }
    }
}
