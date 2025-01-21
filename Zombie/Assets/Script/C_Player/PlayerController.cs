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
  



    [Header("�̵�")]
    [SerializeField]
    private float JumpForce = 0.0f;
    [SerializeField]
    private float MoveSpeed = 0.0f;
    [SerializeField]
    private float MouseSpeed = 0.0f;
    [SerializeField]
    private bool isground = false;
    

    [Header("ȸ��")]
    [SerializeField]
    private float XRoatate = 0.0f;
    [SerializeField]
    private float YRoatate = 0.0f;

    [Header("�����")]
    [SerializeField]
    private AudioClip Walk_Audio;


    private Vector3 dir; //����

    private WeaponAssaultRifle weapon; //���� ���� 
   
   

    [SerializeField]
    private Camera MainCam; //ī�޶� ȸ���� �÷��̾�� �����ϰ� �����Ұ�
    
    private Rigidbody PlayerRigd; // �÷��̾� ������ ���� (����)
    private PlayerAnimator animator; //�ִϸ��̼�
    private AudioSource audiosource;

    private LayerMask layer; //Ray���� ó�� layer�� �������� ó���ҵ�

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
        PlayerRigd.freezeRotation = true; //ȸ�� ����
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;  //���콺 ����
        UnityEngine.Cursor.visible = false; //���콺 �Ⱥ��̰� �ϱ� 
        MainCam = this.GetComponentInChildren<Camera>();

    }



    // Update is called once per frame
    void Update()
    {

        isground = Physics.Raycast(transform.position, Vector3.down, 1.5f);
        Rotate();
        PlayerMove(); //������
        Jump();

        UpdateWeaponAction();
        // Debug.Log(Input.mousePosition); //���콺 ������ ǥ��
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
                audiosource.Stop(); //���� ������� �������� ��� ���°�
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
        XRoatate = Mathf.Clamp(XRoatate, -90, 90); //���� ���� . �� �Ʒ� 
        MainCam.transform.rotation = Quaternion.Euler(XRoatate, YRoatate, 0);
        transform.rotation = Quaternion.Euler(0, YRoatate, 0); //�̰͵� �������� �����ִ� ��ǥ���� ���� ���� ���� ��ȯ �����ָ� ���� ���� �����.
        //���� transform�� �ٲ��ָ� ���� �������� ���� ���� ������ �����ϸ� ���ߺξ��� �ȴ�.  �׷��� ���ƾ��Ѵ�. 

    }
}
