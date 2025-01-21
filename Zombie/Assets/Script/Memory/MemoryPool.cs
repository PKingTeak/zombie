using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPool : MonoBehaviour
{
    //�޸� Ǯ�� �����ϴ� ������Ʈ ����
    public class PoolItem
    {

        public bool isActive;
        public GameObject gameobject;

    }

    private int increaseCount = 5; //������Ʈ�� ������ �� Instantiate()�� �߰� �����Ǵ� ������Ʈ ����
    private int maxCount; //���� ����Ʈ�� ��ϵǾ� �ִ� ������Ʈ ����
    private int activeCount; //���� ���ӿ� ���ǰ� �ִ� (Ȱ��ȭ) ������Ʈ ���� 

    private GameObject poolObject; // ������Ʈ Ǯ������ �����ϴ� ���� ������Ʈ ������
    private List<PoolItem> poolItemList; //�����Ǵ� ��� ������Ʈ�� �����ϴ� ����Ʈ

    public int MaxCount => maxCount; //�ܺο��� ���� ����Ʈ�� ��ϵǾ� �ִ� ������Ʈ ���� Ȯ���� ���� ������Ƽ
    public int ActiveCount => activeCount; //�ܺο��� ���� Ȱ��ȭ �Ǿ� �ִ� ������Ʈ ���� Ȯ���� ���� ������Ƽ

    public MemoryPool(GameObject poolObject) //������
    {
        maxCount = 0;
        activeCount = 0;
        this.poolObject = poolObject;

        poolItemList = new List<PoolItem>();

        InstantiateObject();

    }

    /// <summary>
    /// increaseCount ������ ������Ʈ�� �����Ѵ�. 
    /// </summary>

    public void InstantiateObject()
    {
        maxCount += increaseCount;

        for (int i = 0; i < increaseCount; i++)
        {

            PoolItem poolitem = new PoolItem();

            poolitem.isActive = false;
            poolitem.gameobject = GameObject.Instantiate(poolObject); //���� �Ҵ� 5�� �� ���� ����Ѵ�. 
            poolitem.gameobject.SetActive(false); //�ϴ� �Ⱥ��̰� ������ �� �������̴�. 

            poolItemList.Add(poolitem);


        }


    }

    /// <summary>
    /// ���� ��������(Ȱ��/��Ȱ��) ��� ������Ʈ�� �����Ұ�
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
            GameObject.Destroy(poolItemList[i].gameobject); //���� ����Ʈ�� �����̴�. (���� �ٲ�ų� ������ ����ɶ� �ѹ��� ȣ���Ͽ� ������ ������ ���̴�. 
        }

        poolItemList.Clear(); // �����ѻ��·� �ʱ�ȭ ���ش�. 

    }


    public GameObject ActivatePoolItem()//Ȱ��ȭ
    {
        if (poolItemList == null)
        {
            return null;
        }
        if (maxCount == activeCount)
        {

            InstantiateObject(); //��Ȱ��ȭ ������Ʈ�� ������ �������� ������ �ٰ�
        }

        int count = poolItemList.Count;
        for (int i = 0; i < count; i++)
        {
            PoolItem poolitem = poolItemList[i];

            if (!poolitem.isActive)
            {
                activeCount++;

                poolitem.isActive = true;
                poolitem.gameobject.SetActive(true); //Ȱ��ȭ


                return poolitem.gameobject; //���ӿ�����Ʈ�� �����ؾ� 
            }

        }

        return null; // �ƹ��͵� ������ null��ȯ

    }


    /// <summary>
    /// ���� ����� �Ϸ�� ������Ʈ ��Ȱ��ȭ ���·� ��ȯ
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
            poolitem.gameobject.SetActive(false); //��Ȱ��ȭ 

            return;
        }

    }


    /// <summary>
    /// ���ӿ� ������� ��� ������Ʈ�� ��Ȱ��ȭ ���·� ����
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
        activeCount = 0; //���� ��


    
    }



}