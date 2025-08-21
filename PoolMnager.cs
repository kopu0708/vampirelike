using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMnager : MonoBehaviour
{
    public GameObject[] prefabs; // Prefab to pool
    List<GameObject>[] pools; // List to hold pooled objects

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        for (int i = 0; i < prefabs.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }

    }
    public GameObject GetObject(int index)
    {
        GameObject select = null;


        foreach(GameObject item in pools[index])
        {
           if(item.activeSelf == false)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        if(select == null)
        {
            select = Instantiate(prefabs[index],transform);
            pools[index].Add(select);
        }




        return select;
    }
}
