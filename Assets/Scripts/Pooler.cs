using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public string _tag;
    public GameObject prefab;
    public int size;

    public Pool(string tag, GameObject prefab, int size)
    {
        this._tag = tag;
        this.prefab = prefab;
        this.size = size;
    }
}

public class Pooler : MonoBehaviour
{
    public static Pooler Instance { get; private set; }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this.gameObject);

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        if(pools != null)
        {
            foreach (Pool pool in pools)
                FillPoolDictionary(pool);
        }
    }

    private void FillPoolDictionary(Pool pool)
    {
        if (poolDictionary.ContainsKey(pool._tag))
            throw new System.InvalidOperationException("You try to asign pool tag, which already contains in poolDictionary! Try rename tag");

        Queue<GameObject> objectPool = new Queue<GameObject>();
        for (int i = 0; i < pool.size; i++)
        {
            GameObject obj = Instantiate(pool.prefab, transform);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }

        poolDictionary.Add(pool._tag, objectPool);
        pools.Add(pool);

    }

    public void AddPool(string tag ,GameObject prefab, int size)
    {
        Pool pool = new Pool(tag, prefab, size);
        FillPoolDictionary(pool);
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
            return null;

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
