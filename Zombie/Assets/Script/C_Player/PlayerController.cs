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

    private Vector3 dir;



    [SerializeField]
    private Camera MainCam; //카메라 회전도 플레이어와 동일하게 적용할것

    private Rigidbody PlayerRigd;


    private LayerMask layer;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigd = GetComponent<Rigidbody>(); // 물리 관련 컴포넌트 
        PlayerRigd.freezeRotation = true; //회전 고정
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;  //마우스 고정
        UnityEngine.Cursor.visible = false; //마우스 안보이게 하기 
        MainCam = this.GetComponentInChildren<Camera>();

    }



    // Update is called once per frame
    void Update()
    {

        isground = Physics.Raycast(transform.position, Vector3.down, 1.5f);
        PlayerMove(); //움직임
        Rotate();
        Jump();

        // Debug.Log(Input.mousePosition); //마우스 움직임 표시
    }




    private void PlayerMove()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.z = Input.GetAxisRaw("Vertical");

        Vector3 movedir = transform.forward * dir.z + transform.right * dir.x;

        transform.position += (movedir.normalized * MoveSpeed * Time.deltaTime);

    }

    private void Jump()
    {
       
        if (isground == true && Input.GetKeyDown(KeyCode.Space))
        { 
        PlayerRigd.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    private void GroundCheck()
    {
       
        


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
