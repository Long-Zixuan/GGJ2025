using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    [SerializeField] private uint initPoolSize;
    [SerializeField] private PooledObject objectToPool;
    // store the pooled objects in a collection
    private Stack<PooledObject> stack_;
    private void Start()
    {
        SetupPool();
    }
    // creates the pool (invoke when the lag is not noticeable)
    private void SetupPool()
    {
        stack_ = new Stack<PooledObject>();
        PooledObject instance = null;
        for (int i = 0; i < initPoolSize; i++)
        {
            instance = Instantiate(objectToPool);
            instance.Pool = this;
            instance.gameObject.SetActive(false);
            stack_.Push(instance);
        }
    }
    public PooledObject GetPooledObject()
    {
        // if the pool is not large enough, instantiate a new PooledOb jects
        if (stack_.Count == 0)
        {
            PooledObject newInstance = Instantiate(objectToPool);
            newInstance.Pool = this;
            return newInstance;
        }
        // otherwise, just grab the next one from the list
        PooledObject nextInstance = stack_.Pop();
        nextInstance.gameObject.SetActive(true);
        return nextInstance;
    }
    public void ReturnToPool(PooledObject pooledObject)
    {
        stack_.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
    }
}
