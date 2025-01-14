using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeaponSetting

{
    public float attackRate; // 공격 속도
    public float attackDistance; // 사거리
    public bool isAutomaticAttack; //연속 공격
    public float Damage;

}
