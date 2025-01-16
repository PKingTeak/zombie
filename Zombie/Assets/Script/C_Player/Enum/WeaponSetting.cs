using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponName { AssaultRifle  = 0 }; //라이플 한자루 만 존재하기 때문에 일단 하나만 있다고 생각한다. 


[System.Serializable]
public struct WeaponSetting

{
    public WeaponName weaPonName; // 이름 
    public int currentAmmo; //현재 탄약수
    public int maxAmmo; // 최대 탄약
    public float attackRate; // 공격 속도
    public float attackDistance; // 사거리
    public bool isAutomaticAttack; //연속 공격
    public float Damage;

}
