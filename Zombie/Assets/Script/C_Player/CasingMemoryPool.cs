using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CasingMemoryPool : MonoBehaviour
{
    [SerializeField]
    private GameObject casingPrefabs;
    [SerializeField]
    private MemoryPool memory;


    private void Awake()
    {
        memory = new MemoryPool(casingPrefabs); //ź�� �������� ����Ͽ� ������ ��
    }


    public void SpawnCasing(Vector3 pos, Vector3 dir)
    {
        GameObject item = memory.ActivatePoolItem();
        item.transform.position = pos;
        item.transform.rotation = Random.rotation;
        item.GetComponent<Casing>().Setup(memory, dir);

    
    }



}
