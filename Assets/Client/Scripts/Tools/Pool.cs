using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    public T Prefab { get; }
    public bool AutoExpand { get; set; } //Автоматическое расширение пула
    public Transform PoolsParent { get; }

    private Queue<T> pool = new Queue<T>();

    public Pool(T prefab, int count, Transform poolsParent, bool autoExpand)
    {
        this.Prefab = prefab;
        this.PoolsParent = poolsParent;
        this.AutoExpand = autoExpand;

        this.Init(count);
    }
    //Создаём пул размером count.
    private void Init(int count)
    {
        for (int i = 0; i < count; i++)
            CreateObject();
    }
    //Создаём и добавляем object в очередь.
    private T CreateObject(bool activity = false)
    {
        var obj = UnityEngine.Object.Instantiate(Prefab, PoolsParent);

        obj.gameObject.SetActive(activity);

        pool.Enqueue(obj);

        return obj;
    }
    private bool FindFreeObj(out T _object)
    {
        foreach(var obj in pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                _object = obj;
                obj.gameObject.transform.position = PoolsParent.position;
                obj.gameObject.SetActive(true);
                return true;
            }
        }

        _object = null;
        return false;
    }
    public T GetFreeObject()
    {
        if(FindFreeObj(out var _object))
            return _object;

        //Если предполагалось автоматическое расширение пула.
        if (AutoExpand)
            return CreateObject(true);
        throw new Exception($"No free objects in pool of type {typeof(T)}");
    }
}
