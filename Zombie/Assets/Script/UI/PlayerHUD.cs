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
    private TextMeshProUGUI textWeaponName; // �����̸�
    [SerializeField]
    private Image imageWeaponIcon; //���� ������
    [SerializeField]
    private Sprite[] spriteWeaonIcon; //���� �����ܿ� ���Ǵ� Sprite�迭 


    [Header("Ammo")]
    [SerializeField]
    private TextMeshProUGUI textAmmo;


    private void Awake()
    {
        SetupWeaPon();

        weapon.onAmmoEvent.AddListener(UpdateHUD);
        //�޼ҵ尡 ��ϵǾ� �ִ� �̺�Ʈ Ŭ����(weapon.xx)��
        //Invoke() �޼ҵ尡 ȣ��� �� ��ϵ� �޼ҵ�(�Ű�����)�� ����ȴ�. 
        
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
