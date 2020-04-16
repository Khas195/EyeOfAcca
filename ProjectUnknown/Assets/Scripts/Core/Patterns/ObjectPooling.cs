using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public abstract class ObjectPooling<T> : MonoBehaviour
{

    [SerializeField]
    [ReadOnly]
    protected Queue<T> pool = new Queue<T>();
    public T RequestInstance()
    {
        LogHelper.GetInstance().Log(this + " got requested an instance of " + typeof(T).FullName + " in pool");
        T result = default(T);

        if (pool.Count <= 0)
        {
            LogHelper.GetInstance().Log(this + " no more " + typeof(T).FullName + " in pool, Creating a new one");
            result = CreateInstance();
        }
        else
        {
            LogHelper.GetInstance().Log(this + " found extra in pool, dequeueing it");
            result = pool.Dequeue();
        }
        LogHelper.GetInstance().Log(this + " activates " + result);
        ActivateOjbect(result);
        LogHelper.GetInstance().Log(this + " return requested instance of " + typeof(T).FullName);
        return result;
    }
    public void ReturnInstance(T returnee)
    {
        LogHelper.GetInstance().Log(this + " recieved an instance of " + typeof(T).FullName + " back");
        if (returnee == null)
        {
            LogHelper.GetInstance().LogWarning(this + " recieved a null in return instance");
        }
        LogHelper.GetInstance().Log(this + " deactivates " + returnee);
        DeactivateObject(returnee);
        this.pool.Enqueue(returnee);
        LogHelper.GetInstance().Log(this + " enqueues/add " + returnee + " to pool");
    }
    protected abstract T CreateInstance();
    protected abstract void ActivateOjbect(T target);

    protected abstract void DeactivateObject(T target);
}
