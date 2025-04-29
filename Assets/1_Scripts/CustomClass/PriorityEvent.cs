using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 직접 만든 우선순위 기반 이벤트 관리 (매개변수 없음)
/// <para>
/// 클래스 설명 <br/>
///  이 클래스를 사용하면 일반 Event Action과 다르게, 구독한 시점에 따라, 실행 순서가 바뀌는 것이 아닌, 구독할때 사용한 매개변수에 따라 실행 순서가 달라집니다 <br/>
///  단, 구독할 때, 동일한 Priority 매개변수를 사용한 경우에는, 여전히 구독한 시점이 빠른 순서로 실행 순서가 결정됩니다
/// </para>
/// <para>
/// 클래스 호출 관련 설명 <br/>
///  이 클래스를 통해 선언한 이벤트는, 호출할 때 "이벤트_이름"?.Invoke() 과 같은 형태로 작성할 필요가 없습니다. <br/>
/// 그냥 .Invoke()를 하더라도 ?.Invoke와 동일한 기능을 합니다
/// </para>
/// </summary>
[Serializable]
[Tooltip ("인스펙터에서 보이는 함수 이름은 보기 전용이라 수정하여도 문제가 없지만, Priority는 수정하면 작동에 변화가 생깁니다. 수정하지 마십시오")]
public class PriorityEvent
{
    [Serializable]
    private class Listener
    {
        public string functionName;  // 인스펙터 표시용
        public Action callback;      // 실제 실행할 함수
        public int priority;         // 우선순위
    }

    [SerializeField] private List<Listener> listeners = new();

    /// <summary>
    /// 이벤트에 함수 추가할 때 사용하는 함수
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
        listeners.Sort((a, b) => a.priority.CompareTo(b.priority)); // 우선순위 낮은 -> 높은 순 정렬
    }

    /// <summary>
    /// 이벤트에 함수를 제거할 때 사용하는 함수
    /// </summary>
    public void RemoveListener(Action callbackToRemove)
    {
        listeners.RemoveAll(x => x.callback == callbackToRemove);
    }

    /// <summary>
    /// 이벤트 호출
    /// </summary>
    public void Invoke()
    {
        foreach (var Addedlistener in listeners)
        {
            Addedlistener.callback?.Invoke();
        }
    }
}
