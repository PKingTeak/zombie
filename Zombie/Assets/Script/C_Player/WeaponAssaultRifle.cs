using System.Collections; //코루틴 사용을 위해 참조 
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;




public class WeaponAssaultRifle : MonoBehaviour
{
    [Header("Audio Clip")]
    [SerializeField]
    private AudioClip audioClipTakeOutWeapon;

    private AudioSource audioSource;
    private PlayerAnimator playerAnimator; // 플레이어 애니메이션 재생 할 것


    [Header("WeaponSetting")]
    [SerializeField]
    private WeaponSetting weaponSetting; //무기 셋팅

    private float LastAttackTime = 0.0f; // 마지막 발사 시간 채크






    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerAnimator = GetComponentInParent<PlayerAnimator>();
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
            //뛰고있을때 공격 가능하게 할까? 말까? 일단 그냥 하는걸로
            //뛸때 못쏘는걸로 제어하려면 movement를 state를 나눠서 움직일때 return 하는 방식을 사용해야함 
            LastAttackTime = Time.time;
            playerAnimator.AnimationPlay("Fire", -1, 0);


        }
    
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

    private void OnEnable()
    {
      //  PlaySound(audioClipTakeOutWeapon);
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
