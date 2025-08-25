using System;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// 게임 전체의 이벤트를 관리하는 싱글톤 클래스.
/// </summary>
public class GameEventManager : MonoBehaviour
{
    #region Singleton

    private static GameEventManager _instance;

    public static GameEventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameEventManager>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject("GameEventManager");
                    _instance = obj.AddComponent<GameEventManager>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Public, Serialized Fields
    #endregion

    #region Private Fields

    private Dictionary<Type, Delegate> eventListeners = new Dictionary<Type, Delegate>();

    #endregion

    #region Properties
    #endregion

    #region Public Methods

    /// <summary>
    /// 특정 이벤트에 리스너(콜백 메소드)를 구독합니다.
    /// </summary>
    /// <typeparam name="_T">이벤트 구조체 타입</typeparam>
    /// <param name="listener">이벤트 발생 시 호출될 콜백 메소드</param>
    public void Subscribe<T>(Action<T> _listener) where T : struct
    {
        Type eventType = typeof(T);

        if (eventListeners.TryGetValue(eventType, out Delegate existingDelegate))
        {
            eventListeners[eventType] = Delegate.Combine(existingDelegate, _listener);
        }
        else
        {
            eventListeners[eventType] = _listener;
        }
    }

    /// <summary>
    /// 특정 이벤트의 리스너 구독을 취소합니다.
    /// </summary>
    /// <typeparam name="_T">이벤트 구조체 타입</typeparam>
    /// <param name="_listener">취소할 콜백 메소드</param>
    public void Unsubscribe<T>(Action<T> _listener) where T : struct
    {
        Type eventType = typeof(T);

        if (eventListeners.TryGetValue(eventType, out Delegate existingDelegate))
        {
            Delegate result = Delegate.Remove(existingDelegate, _listener);
            if (result == null)
            {
                eventListeners.Remove(eventType);
            }
            else
            {
                eventListeners[eventType] = result;
            }
        }
    }

    public void Publish<T>(T _eventArgs) where T : struct
    {
        Type eventType = typeof(T);

        if (eventListeners.TryGetValue(eventType, out Delegate existingDelegate))
        {
            Action<T> action = existingDelegate as Action<T>;
            action?.Invoke(_eventArgs);
        }
    }

    #endregion
}
