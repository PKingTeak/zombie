using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //직렬화 하여 다른 클래스의 변수로 새성 되었을때 Inspector View에 멤버 변수 목록이 뜨게 하기 위해서 
public struct WeaponSetting

{
    public float attackRate; // 공격 속도
    public float attackDistance; // 사거리
    public float isAutomaticAttack; //연속 공격
    public float Damage;
    
}
