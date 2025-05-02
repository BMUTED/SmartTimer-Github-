using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerPauseButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ButtonText;

    /// <summary>
    /// Ÿ�̸Ӹ� �Ͻ������ϰų�, �Ͻ������� Ǯ �� ȣ���ϴ� �Լ�
    /// </summary>
    public void PausePlay_TimerButton()
    {
        //Ÿ�̸Ӱ� �Ͻ����� �Ǿ��ִ� ���¿��� �Լ��� ȣ������ �� (����ڰ�, Ÿ�̸Ӹ� ��� ��Ű���� �Լ��� ȣ���� ���)
        if (GameManager.Instance.TimerManagerSC.TimerFlowSC.IsTimerForceStopped)
        {
            ButtonText.text = ">>"; //��� ���� ����� >>�ڷ� �ؽ�Ʈ ǥ�� ����
        }
        //Ÿ�̸Ӱ� �Ͻ����� �Ǿ����� ���� ���¿��� �Լ��� ȣ������ �� (����ڰ�, Ÿ�̸Ӹ� ������Ű���� �Լ��� ȣ���� ���)
        else
        {
            ButtonText.text = "ll"; //���� ���� ����� ll�ڷ� �ؽ�Ʈ ǥ�� ����
        }

        GameManager.Instance.TimerManagerSC.TimerFlowSC.IsTimerForceStopped = !GameManager.Instance.TimerManagerSC.TimerFlowSC.IsTimerForceStopped;
    }
}
