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
    
    }




}
