using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolingSystem <T> 
    where T : MonoBehaviour
{
    private T _prefab;
    private List<T> _objects;

    public ObjectPoolingSystem(T prefab, int count)
    {
        _prefab = prefab;
        _objects = new List<T>();

        for(int i = 0; i < count; i++)
        {
            var obj = GameObject.Instantiate(_prefab);
            obj.gameObject.SetActive(false);
            _objects.Add(obj);
        }
    }

    public T Get()
    {
        var obj = _objects.FirstOrDefault(x => !x.isActiveAndEnabled);

        if (obj == null) obj = Create();

        obj.gameObject.SetActive(true);
        return obj;
    }

    public T Create()
    {
        var obj = GameObject.Instantiate(_prefab);
        _objects.Add(obj);

        return obj;
    }

    public void Release(T obj)
    {
        obj.gameObject.SetActive(false);
    }
}
