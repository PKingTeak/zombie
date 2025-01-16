using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponName { AssaultRifle  = 0 }; //������ ���ڷ� �� �����ϱ� ������ �ϴ� �ϳ��� �ִٰ� �����Ѵ�. 


[System.Serializable]
public struct WeaponSetting

{
    public WeaponName weaPonName; // �̸� 
    public int currentAmmo; //���� ź���
    public int maxAmmo; // �ִ� ź��
    public float attackRate; // ���� �ӵ�
    public float attackDistance; // ��Ÿ�
    public bool isAutomaticAttack; //���� ����
    public float Damage;

}
