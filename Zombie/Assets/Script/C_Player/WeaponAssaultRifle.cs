using System.Collections; //�ڷ�ƾ ����� ���� ���� 
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class AmmoEvent : UnityEngine.Events.UnityEvent<int, int> { };
//���� 
/*
 * Unity EventŬ������ �Ϲ�ȭ ���ǿ� ���� ȣ���� �� �ִ� �̺�Ʈ �޼ҵ���� �Ű������� �����ȴ�. 
 * C++ �� ������ ���ø� Ŭ������ �����ϴ°Ͱ� ����. 
 */ 


public class WeaponAssaultRifle : MonoBehaviour
{
    [Header("Fire Effect")]
    [SerializeField]
    private GameObject muzzleFlashEffect;

    [Header("Audio Clip")]
    [SerializeField]
    private AudioClip audioClipTakeOutWeapon;
    [SerializeField]
    private AudioClip audioClipFire;

    private AudioSource audioSource;
    private PlayerAnimator playerAnimator; // �÷��̾� �ִϸ��̼� ��� �� ��


    [Header("WeaponSetting")]
    [SerializeField]
    private WeaponSetting weaponSetting; //���� ����

    private float LastAttackTime = 0.0f; // ������ �߻� �ð� äũ



    private void OnEnable()
    { 
        PlaySound(audioClipTakeOutWeapon);
        muzzleFlashEffect.SetActive(false); // �ϴ� ������ 
        
    }



    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerAnimator = GetComponentInParent<PlayerAnimator>();

        weaponSetting.currentAmmo = weaponSetting.maxAmmo; //ó���� Ǯ���� 
    }

    public void StartWeaponAciton(int type = 0)
    {
        if (type == 0)
        {
            if (weaponSetting.isAutomaticAttack == true)
            {
                StartCoroutine("OnAttackLoop");

            }
            else
            {
               OnAttack();
            }

        }
    }

    public void StopWeaponAction(int type = 0)
    { 
        if(type == 0)
        {
            StopCoroutine("OnAttackLoop");
        }
        
    }

    private IEnumerator OnAttackLoop()
    {
        while (true)
        {
            OnAttack();

            yield return null;
        }
    }

    public void OnAttack()
    { 
        if(Time.time - LastAttackTime > weaponSetting.attackRate)
        {
            if (weaponSetting.currentAmmo <= 0)
            {
                return; //  ź���� ������ ������ ���Ѵ�. 
            }
            //�ٰ������� ���� �����ϰ� �ұ�? ����? �ϴ� �׳� �ϴ°ɷ�
            //�۶� ����°ɷ� �����Ϸ��� movement�� state�� ������ �����϶� return �ϴ� ����� ����ؾ��� 
            LastAttackTime = Time.time;
            playerAnimator.AnimationPlay("Fire", -1, 0);
            StartCoroutine("OnMuzzleFlashEffect"); // ����Ʈ �ڷ�ƾ���� ȣ���� ���̴�. 
            PlaySound(audioClipFire);
            weaponSetting.currentAmmo--; //ź��� �ѹ߾� ������ 


            
        }
    
    }

    private IEnumerator OnMuzzleFlashEffect()
    { 
        muzzleFlashEffect.SetActive(true); // �Ѿ��ϴϱ� 
        yield return new WaitForSeconds(weaponSetting.attackRate * 0.3f); // �� ��� �ð����� 0.3�ʸ���ŭ �� ������ ���� �ٽ� ������Ʈ�� �����̴�. 
        muzzleFlashEffect.SetActive(false);

    }

    public void AimOn()
    {
        playerAnimator.AnimationPlay("AimOn", -1, 0);
        
    }





    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();

    }

 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
