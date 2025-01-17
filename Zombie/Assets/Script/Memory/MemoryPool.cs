using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPool : MonoBehaviour
{
    //메모리 풀로 관리하는 오브젝트 정보
    public class PoolItem
    {

        public bool isActive;

        public GameObject gameobject; 

    }

    private int increaseCount = 5; //오브젝트가 부족할 때 Instantiate()로 추가 생성되는 오브젝트 개수
    private int maxCount; //현재 리스트에 등록되어 있는 오브젝트 개수
    private int activeCount; //현재 게임에 사용되고 있는 (활성화) 오브젝트 개수 

    private GameObject poolObject; // 오브젝트 풀링에서 관리하는 게임 오브젝트 프리팹
    private List<PoolItem> poolItemList; //관리되는 모든 오브젝트를 저장하는 리스트

    public int MaxCount => maxCount; //외부에서 현재 리스트에 등록되어 있는 오브젝트 개수 확인을 위한 프로퍼티
    public int ActiveCount => activeCount; //외부에서 현재 활성화 되어 있는 오브젝트 개수 확인을 위한 프로퍼티

    public MemoryPool(GameObject poolObject) //생성자
    {
        maxCount = 0;
        activeCount = 0;
    
    }




}
