using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Collections;
using System;

public abstract class QTEBase : MonoBehaviour
{
    public bool isRunning { get; protected set; }

    protected float duration;
    protected Action<bool> onQTEComplete;

    public virtual void Init(float duration, Action<bool> onComplete)
    {
        this.duration = duration;
        this.onQTEComplete = onComplete;
    }

    public abstract void StartQTE();
    public abstract void EndQTE(bool success);

    protected IEnumerator TimerCoroutine()
    {
        float time = duration;
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        if (isRunning)
        {
            EndQTE(false);
        }
    }

}
