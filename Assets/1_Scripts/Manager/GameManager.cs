using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̱���ȭ �� �ٸ� �Ŵ��� ��ũ��Ʈ���� ĳ���صδ� �뵵�� ��ũ��Ʈ
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    //���� ������ �ٸ� ������Ʈ���� ������ Ŭ���� ������ �����Ҷ�,�̹� ����Ǿ��ִٸ� ���� ����Ǿ� �ִ� ���� ������ �״�� �����Ѵ�.
    //(�ܼ��� ����, Static(���� ������) Ŭ���� �ȿ� ���� ����ȴٰ� �����ϸ� ��)
    //���� ���뿡 ���� �� ���� ������ ���ӸŴ��� �ν��Ͻ��� �� instance�� ��� �༮�� �����ϰ� �� ���̴�.

    public TimerManager TimerManagerSC;
    public SceneChangeManager SceneChangeManagerSC;
    public SaveManager SaveManagerSC;
    public ScreenSizeManager ScreenSizeManagerSC;

    [Space(10f)]
    public TimerScene_UI_Data TimerSceneUI_Data;

    private void Awake()
    {
        //�̱��� ���� �ڵ�
        if (null == Instance) //�� Ŭ���� �ν��Ͻ��� ź������ �� �������� instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�
        {
            Instance = this; // �ڽ��� �־��ش�.
            DontDestroyOnLoad(this.gameObject); //�� ��ȯ�� �Ǵ��� �ı����� �ʰ� �Ѵ�.
        }
        //���� �� �̵��� �Ǿ��µ� �� ������ Hierarchy�� GameManager ������ ���� �ִ�.
        else// ���� ���������� instance�� �̹� �ٸ� ������Ʈ�� ����Ǿ��ִٸ�
        {
            //Debug.Log("���ο� GameManager �̱��濡 ���� ������");
            Destroy(this.gameObject); // �ڽ�(���ο� ���� GameManager)�� �������ش�.
        }
    }
}
