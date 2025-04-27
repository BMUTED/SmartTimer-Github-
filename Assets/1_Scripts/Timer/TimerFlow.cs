using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerFlow : MonoBehaviour
{
    public bool IsTimeFlowing; //Ư�� ���α׷��� �ѳ���, Ÿ�̸��� �ð��� �귯�� �ϴ� ���

    //Ư�� ���α׷��� �ѳ��� �ð��� ������ ������
    public float HourTime;
    public float MinuteTime;
    public float SecondTime;

    private void Update()
    {
        if (IsTimeFlowing)
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
        SecondTime += Time.deltaTime;

        //�ð��� 60�ʸ� �Ѿ ���
        if(SecondTime >= 60)
        {
            SecondTime -= 60; //60��ŭ ���� (�Ҽ����� ����� �� ������ 0 ���� ���� �ʱ�ȭ���� ��������)
            MinuteTime += 1;
            if (MinuteTime >= 60)
            {
                MinuteTime -= 60;
                HourTime += 1;
            }
        }
    }

    /// <summary>
    /// IsTimeFlowing�� ���� �ٲٰ� ������ ȣ���ϴ� �Լ�
    /// IsTimeFlowing�� ���� ����, Ÿ�̸��� ���� ���ؾ� �ϱ� ������ �Լ��� �������
    /// </summary>
    /// <param name="Value"></param>
    public void ChangeTimeFlowing(bool Value)
    {
        IsTimeFlowing = Value;

        //���� ���� �ð��� �帣���� �ƴ��� ���ο� ���� ����
        if (Value) //IsTimeFlowing == True �϶�
        {
            TimerManager.Instance.ShowTimerSC.TimerText.color = TimerManager.Instance.TimerColorSC.TimeFlowingColor;
        }
        else //IsTimeFlowing == False �϶�
        {
            TimerManager.Instance.ShowTimerSC.TimerText.color = TimerManager.Instance.TimerColorSC.TimeStopColor;
        }
    }
}
