using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���� �켱���� ��� �̺�Ʈ ���� (�Ű����� ����)
/// <para>
/// Ŭ���� ���� <br/>
///  �� Ŭ������ ����ϸ� �Ϲ� Event Action�� �ٸ���, ������ ������ ����, ���� ������ �ٲ�� ���� �ƴ�, �����Ҷ� ����� �Ű������� ���� ���� ������ �޶����ϴ� <br/>
///  ��, ������ ��, ������ Priority �Ű������� ����� ��쿡��, ������ ������ ������ ���� ������ ���� ������ �����˴ϴ�
/// </para>
/// <para>
/// Ŭ���� ȣ�� ���� ���� <br/>
///  �� Ŭ������ ���� ������ �̺�Ʈ��, ȣ���� �� "�̺�Ʈ_�̸�"?.Invoke() �� ���� ���·� �ۼ��� �ʿ䰡 �����ϴ�. <br/>
/// �׳� .Invoke()�� �ϴ��� ?.Invoke�� ������ ����� �մϴ�
/// </para>
/// </summary>
[Serializable]
[Tooltip ("�ν����Ϳ��� ���̴� �Լ� �̸��� ���� �����̶� �����Ͽ��� ������ ������, Priority�� �����ϸ� �۵��� ��ȭ�� ����ϴ�. �������� ���ʽÿ�")]
public class PriorityEvent
{
    [Serializable]
    private class Listener
    {
        public string functionName;  // �ν����� ǥ�ÿ�
        public Action callback;      // ���� ������ �Լ�
        public int priority;         // �켱����
    }

    [SerializeField] private List<Listener> listeners = new();

    /// <summary>
    /// �̺�Ʈ�� �Լ� �߰��� �� ����ϴ� �Լ�
    /// </summary>
    public void AddListener(Action callback, int priority = 0)
    {
        if (callback == null) return;

        Listener listener = new Listener
        {
            functionName = callback.Method.Name,
            callback = callback,
            priority = priority
        };

        listeners.Add(listener);
        listeners.Sort((a, b) => a.priority.CompareTo(b.priority)); // �켱���� ���� -> ���� �� ����
    }

    /// <summary>
    /// �̺�Ʈ�� �Լ��� ������ �� ����ϴ� �Լ�
    /// </summary>
    public void RemoveListener(Action callbackToRemove)
    {
        listeners.RemoveAll(x => x.callback == callbackToRemove);
    }

    /// <summary>
    /// �̺�Ʈ ȣ��
    /// </summary>
    public void Invoke()
    {
        foreach (var Addedlistener in listeners)
        {
            Addedlistener.callback?.Invoke();
        }
    }
}
