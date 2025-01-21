using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.IO;
using UnityEngine.WSA;
using UnityEngine.UIElements;
using TreeEditor;
using Unity.VisualScripting;

public class PlayerM : MonoBehaviour
{
  



    [Header("이동")]
    [SerializeField]
    private float JumpForce = 0.0f;
    [SerializeField]
    private float MoveSpeed = 0.0f;
    [SerializeField]
    private float MouseSpeed = 0.0f;
    [SerializeField]
    private bool isground = false;
    

    [Header("회전")]
    [SerializeField]
    private float XRoatate = 0.0f;
    [SerializeField]
    private float YRoatate = 0.0f;

    [Header("오디오")]
    [SerializeField]
    private AudioClip Walk_Audio;


    private Vector3 dir; //방향

    private WeaponAssaultRifle weapon; //무기 제어 
   
   

    [SerializeField]
    private Camera MainCam; //카메라 회전도 플레이어와 동일하게 적용할것
    
    private Rigidbody PlayerRigd; // 플레이어 움직임 관련 (물리)
    private PlayerAnimator animator; //애니메이션
    private AudioSource audiosource;

    private LayerMask layer; //Ray관련 처리 layer로 구분지어 처리할듯

    private void Awake()
    {
        PlayerRigd = GetComponent<Rigidbody>();
        weapon = GetComponentInChildren<WeaponAssaultRifle>();
        animator = GetComponentInChildren<PlayerAnimator>();
        audiosource = GetComponent<AudioSource>();
       
    }



    // Start is called before the first frame update
    void Start()
    {
        PlayerRigd.freezeRotation = true; //회전 고정
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;  //마우스 고정
        UnityEngine.Cursor.visible = false; //마우스 안보이게 하기 
        MainCam = this.GetComponentInChildren<Camera>();

    }



    // Update is called once per frame
    void Update()
    {

        isground = Physics.Raycast(transform.position, Vector3.down, 1.5f);
        Rotate();
        PlayerMove(); //움직임
        Jump();

        UpdateWeaponAction();
        // Debug.Log(Input.mousePosition); //마우스 움직임 표시
    }




    private void PlayerMove()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.z = Input.GetAxisRaw("Vertical");

        Vector3 movedir = transform.forward * dir.z + transform.right * dir.x;

        transform.position += (movedir.normalized * MoveSpeed * Time.deltaTime);

        if (dir.x != 0 || dir.z != 0)
        {
         
            animator.MovementAnimation = 1;
            

        }
        else
        {
            animator.MovementAnimation = 0;

            if (audiosource.isPlaying == true)
            {
                audiosource.Stop(); //만약 오디오가 켜져있을 경우 끄는거
            }
            else
            {
                return;
            }
        
        }




    }

    private void Jump()
    {
       
        if (isground == true && Input.GetKeyDown(KeyCode.Space))
        { 
        PlayerRigd.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }


    private void UpdateWeaponAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapon.StartWeaponAciton();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            weapon.StopWeaponAction();
        }
    
    }




    private void Rotate()
    {
        float horizontRotat = Input.GetAxis("Mouse X") * MouseSpeed * Time.deltaTime;
        float verticalRotat = Input.GetAxis("Mouse Y") * MouseSpeed * Time.deltaTime;
        YRoatate += horizontRotat;
        XRoatate -= verticalRotat;
        XRoatate = Mathf.Clamp(XRoatate, -90, 90); //범위 제한 . 위 아래 
        MainCam.transform.rotation = Quaternion.Euler(XRoatate, YRoatate, 0);
        transform.rotation = Quaternion.Euler(0, YRoatate, 0); //이것도 움직여야 위에있는 목표물을 볼수 있음 같이 변환 안해주면 위에 절때 못쏜다.
        //수정 transform을 바꿔주면 축이 움직여서 위를 보고 앞으로 전진하면 공중부양이 된다.  그래서 막아야한다. 

    }
}
