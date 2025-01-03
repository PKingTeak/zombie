using System.Collections; //코루틴 사용을 위해 참조 
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class WeaponAssaultRifle : MonoBehaviour
{
    [Header("Audio Clip")]
    [SerializeField]
    private AudioClip audioClipTakeOutWeapon;

    private AudioSource audioSource;
    private PlayerAnimator playerAnimator; // 플레이어 애니메이션 재생 할 것


    [Header("WeaponSetting")]
    [SerializeField]
    private WeaponSetting weaponSetting;

    private float LastAttackTime = 0.0f;


    



    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();

    }

    private void OnEnable()
    {
        PlaySound(audioClipTakeOutWeapon);
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
