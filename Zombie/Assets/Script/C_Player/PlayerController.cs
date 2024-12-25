using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.IO;
using UnityEngine.WSA;
using UnityEngine.UIElements;



public class PlayerM : MonoBehaviour
{

    [SerializeField]
    private float JumpForce = 0.0f;
    [SerializeField]
    private float MoveSpeed = 0.0f;
    [SerializeField]
    private float MouseSpeed = 0.0f;


    [Header("회전")]
    [SerializeField]
    private float XRoatate = 0.0f;
    [SerializeField]
    private float YRoatate = 0.0f;



    [SerializeField]
    private Camera MainCam; //카메라 회전도 플레이어와 동일하게 적용할것

    private Rigidbody PlayerRigd;



    // Start is called before the first frame update
    void Start()
    {
        PlayerRigd = GetComponent<Rigidbody>(); // 물리 관련 컴포넌트 
        PlayerRigd.freezeRotation = true; //회전 고정
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;  //화면 고정
        UnityEngine.Cursor.visible = false; //마우스 안보이게 하기 
        MainCam = this.GetComponentInChildren<Camera>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "broad")
        {
            PlayerRigd.useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "broad")
        {
            //중력 작용 및 해제
       //     PlayerRigd.useGravity = true;
        }

    }


    // Update is called once per frame
    void Update()
    {

        PlayerMove(PlayerRigd); //움직임
        Rotate();

        // Debug.Log(Input.mousePosition); //마우스 움직임 표시
    }

    


    private void PlayerMove(Rigidbody _PlayerRigd)
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");


        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * MoveSpeed ; //움직일 방향 

        PlayerRigd.MovePosition(transform.position + _velocity * Time.deltaTime);

    }

    private void Rotate()
    {
        float horizontRotat = Input.GetAxis("Mouse X") * MouseSpeed * Time.deltaTime;
        float verticalRotat = Input.GetAxis("Mouse Y") * MouseSpeed * Time.deltaTime;
        YRoatate += horizontRotat;
        XRoatate -= verticalRotat;
        XRoatate = Mathf.Clamp(XRoatate, -90, 90); //범위 제한 . 위 아래 
        MainCam.transform.rotation = Quaternion.Euler(XRoatate, YRoatate, 0);
        transform.rotation = Quaternion.Euler(XRoatate, YRoatate, 0); //이것도 움직여야 위에있는 목표물을 볼수 있음 같이 변환 안해주면 위에 절때 못쏜다.
    }
}
