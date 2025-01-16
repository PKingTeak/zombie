using System.Collections; //코루틴 사용을 위해 참조 
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;




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
    private PlayerAnimator playerAnimator; // 플레이어 애니메이션 재생 할 것


    [Header("WeaponSetting")]
    [SerializeField]
    private WeaponSetting weaponSetting; //무기 셋팅

    private float LastAttackTime = 0.0f; // 마지막 발사 시간 채크



    private void OnEnable()
    { 
        PlaySound(audioClipTakeOutWeapon);
        muzzleFlashEffect.SetActive(false); // 일단 꺼두자 
        
    }



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
            StartCoroutine("OnMuzzleFlashEffect"); // 이펙트 코루틴으로 호출할 것이다. 
            PlaySound(audioClipFire);

        }
    
    }

    private IEnumerator OnMuzzleFlashEffect()
    { 
        muzzleFlashEffect.SetActive(true); // 켜야하니까 
        yield return new WaitForSeconds(weaponSetting.attackRate * 0.3f); // 쏠때 사격 시간보다 0.3초를만큼 더 빠르게 끄고 다시 오브젝트를 끌것이다. 
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
