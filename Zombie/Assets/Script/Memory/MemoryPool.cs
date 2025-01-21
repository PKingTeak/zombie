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
        this.poolObject = poolObject;

        poolItemList = new List<PoolItem>();

        InstantiateObject();

    }

    /// <summary>
    /// increaseCount 단위로 오브젝트를 생성한다. 
    /// </summary>

    public void InstantiateObject()
    {
        maxCount += increaseCount;

        for (int i = 0; i < increaseCount; i++)
        {

            PoolItem poolitem = new PoolItem();

            poolitem.isActive = false;
            poolitem.gameobject = GameObject.Instantiate(poolObject); //동적 할당 5개 씩 만들어서 사용한다. 
            poolitem.gameobject.SetActive(false); //일단 안보이게 설정을 해 놓을것이다. 

            poolItemList.Add(poolitem);


        }


    }

    /// <summary>
    /// 현재 관리중인(활성/비활성) 모든 오브젝트를 삭제할것
    /// </summary>

    public void DestoryObject()
    {
        if (poolItemList == null)
        {
            return;
        }

        int count = poolItemList.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject.Destroy(poolItemList[i].gameobject); //삭제 리스트를 비울것이다. (씬이 바뀌거나 게임이 종료될때 한번만 호출하여 삭제를 진행할 것이다. 
        }

        poolItemList.Clear(); // 깨끗한상태로 초기화 해준다. 

    }


    public GameObject ActivatePoolItem()//활성화
    {
        if (poolItemList == null)
        {
            return null;
        }
        if (maxCount == activeCount)
        {

            InstantiateObject(); //비활성화 오브젝트가 없으면 동적으로 생성해 줄것
        }

        int count = poolItemList.Count;
        for (int i = 0; i < count; i++)
        {
            PoolItem poolitem = poolItemList[i];

            if (!poolitem.isActive)
            {
                activeCount++;

                poolitem.isActive = true;
                poolitem.gameobject.SetActive(true); //활성화


                return poolitem.gameobject; //게임오브젝트를 리턴해야 
            }

        }

        return null; // 아무것도 없으면 null반환

    }


    /// <summary>
    /// 현재 사용이 완료된 오브젝트 비활성화 상태로 전환
    /// </summary>



    public void DeactivatePoolItem(GameObject removeObject)
    {

        if (poolItemList == null || removeObject == null)
        {
            return;
        }

        int count = poolItemList.Count;

        for (int i = 0; i < count; i++)
        { 
        PoolItem poolitem = poolItemList[i];
            activeCount--;

            poolitem.isActive = false;
            poolitem.gameobject.SetActive(false); //비활성화 

            return;
        }

    }


    /// <summary>
    /// 게임에 사용중인 모든 오브젝트를 비활성화 상태로 만듬
    /// </summary>
    /// 

    public void DeactiveAllpoolItems()
    {
        if (poolItemList == null) 
        {
            return;
        
        }
        int count = poolItemList.Count;
        for(int i = 0; i < count; i++)
        {

            PoolItem poolitem = poolItemList[i];
            if(poolitem.gameobject !=  null && poolitem.isActive ==true)
            {

                   poolitem.isActive=false;
                poolitem.gameobject.SetActive(false);

            }

        }
        activeCount = 0; //전부 끔


    
    }



}