using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerFlow : MonoBehaviour
{
    SaveDatas SaveData;

    public bool IsTimeFlowing { get; private set; }
    public bool TryTimeFlowing { get; private set; } //Ư�� ���α׷��� �ѳ���, Ÿ�̸��� �ð��� �귯�� �ϴ� ���

    public bool IsTimerForceStopped;

    private void Start()
    {
        GameManager.Instance.SceneChangeManagerSC.OnSceneLoaded.AddListener(SyncronizeTimeColor, 11);

        SaveData = GameManager.Instance.SaveManagerSC.SaveData;
    }

    private void Update()
    {
        if (IsTimeFlowing && !IsTimerForceStopped)
        {
            TimeFlowing();
        }
    }

    /// <summary>
    /// Ÿ�̸��� �ð��� �帣�� ����� �Լ�
    /// �� �� �� ������ ������, 60���� ���, 1������ �ٲ�� �׷� ��ɵ���� ������
    /// </summary>
    void TimeFlowing()
    {
        SaveData.SecondTime += Time.deltaTime;

        //�ð��� 60�ʸ� �Ѿ ���
        if(SaveData.SecondTime >= 60)
        {
            SaveData.SecondTime -= 60; //60��ŭ ���� (�Ҽ����� ����� �� ������ 0 ���� ���� �ʱ�ȭ���� ��������)
            SaveData.MinuteTime += 1;
            if (SaveData.MinuteTime >= 60)
            {
                SaveData.MinuteTime -= 60;
                SaveData.HourTime += 1;
            }
        }
    }

    /// <summary>
    /// IsTimeFlowing�� ���� �ٲٰ� ������ ȣ���ϴ� �Լ�
    /// IsTimeFlowing�� ���� ����, Ÿ�̸��� ���� ���ؾ� �ϱ� ������ �Լ��� �������
    /// </summary>
    /// <param name="Value"></param>
    public void TryChangeTimeFlowing(bool Value)
    {
        TryTimeFlowing = Value;

        TimeFlowingCheck();
    }

    /// <summary>
    /// TimeFlowing�� AFKChecker �� IsTimerStopBeingAFK�� ���� �¾ƶ�������, IsTimeFlowing�� True�� �� �������� Ȯ���� ��, ȣ���ϴ� �Լ� <br/>
    /// TimeFlowSC �� TryTimeFlowing�� �����ϰų�, AFK_Checker�� IsTimerStopBeingAFK�� ������ ��, ������������ ȣ���ؾ��ϴ� �Լ��̴�
    /// </summary>
    public void TimeFlowingCheck()
    {
        //��� ���°� �ƴϰ�, ��Ŀ���� â�� ��ϵǾ��ִ� â�϶� (��, Ÿ�̸Ӱ� �귯���� ��Ȳ�� ��)
        if (TryTimeFlowing && GameManager.Instance.TimerManagerSC.AFK_CheckerSC.IsTimerStopBeingAFK == false)
        {
            IsTimeFlowing = true;
        }
        else
        {
            IsTimeFlowing = false;
        }

        if (GameManager.Instance.SceneChangeManagerSC.CurSceneName == "TimerScene")
        {
            //���� ���� �ð��� �帣���� �ƴ��� ���ο� ���� ����
            if (IsTimeFlowing) //IsTimeFlowing == True �϶�
            {
                ChangeTimerColor(true);
            }

            else //IsTimeFlowing == False �϶�
            {
                ChangeTimerColor(false);
            }
        }
    }

    /// <summary>
    /// �ؽ�Ʈ ���� Ÿ�̸Ӱ� �帣���ִ����� ���� ���ο� ���� �����ϴ� �Լ�
    /// SceneChangeManager �� OnSceneLoaded �̺�Ʈ�� �����Ͽ� ���
    /// </summary>
    void SyncronizeTimeColor()
    {
        //���� ���� �ð��� �帣���� �ƴ��� ���ο� ���� ����
        if (TryTimeFlowing) //IsTimeFlowing == True �϶�
        {
            ChangeTimerColor(true);
        }
        else //IsTimeFlowing == False �϶�
        {
            ChangeTimerColor(false);
        }
    }

    /// <summary>
    /// Ÿ�̸��� ���� �����Ҷ�, ����ϴ� �Լ�
    /// </summary>
    /// <param name="IsTimerFlowing">True == Ǫ���� (Ÿ�̸� �带��) / False == ������ (Ÿ�̸� ����)</param>
    public void ChangeTimerColor(bool IsTimerFlowing)
    {
        if (IsTimerFlowing)
        {
            GameManager.Instance.TimerManagerSC.ShowTimerSC.TimerText.color = GameManager.Instance.TimerManagerSC.TimerColorSC.TimeFlowingColor;
        }
        else
        {
            GameManager.Instance.TimerManagerSC.ShowTimerSC.TimerText.color = GameManager.Instance.TimerManagerSC.TimerColorSC.TimeStopColor;
        }
    }
}
