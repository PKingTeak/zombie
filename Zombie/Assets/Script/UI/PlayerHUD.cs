using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("Compoents")]
    [SerializeField]
    private WeaponAssaultRifle weapon;

    [Header("Weapon Base")]
    [SerializeField]
    private TextMeshProUGUI textWeaponName; // 무기이름
    [SerializeField]
    private Image imageWeaponIcon; //무기 아이콘
    [SerializeField]
    private Sprite[] spriteWeaonIcon; //무기 아이콘에 사용되는 Sprite배열 


    [Header("Ammo")]
    [SerializeField]
    private TextMeshProUGUI textAmmo;


    private void Awake()
    {
        SetupWeaPon();

        weapon.onAmmoEvent.AddListener(UpdateHUD);
        //메소드가 등록되어 있는 이벤트 클래스(weapon.xx)의
        //Invoke() 메소드가 호출될 때 등록된 메소드(매개변수)가 실행된다. 
        
    }



    private void SetupWeaPon()
    {
        textWeaponName.text = weapon.weaponName.ToString();
        imageWeaponIcon.sprite = spriteWeaonIcon[(int)weapon.weaponName]; 
    }

    private void UpdateHUD(int currentAmmo, int maxAmmo)
    {
        textAmmo.text = $"<size=40>{currentAmmo}/</size>{maxAmmo}";
        
    }

}
