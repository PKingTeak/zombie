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


    [Header("ȸ��")]
    [SerializeField]
    private float XRoatate = 0.0f;
    [SerializeField]
    private float YRoatate = 0.0f;



    [SerializeField]
    private Camera MainCam; //ī�޶� ȸ���� �÷��̾�� �����ϰ� �����Ұ�

    private Rigidbody PlayerRigd;



    // Start is called before the first frame update
    void Start()
    {
        PlayerRigd = GetComponent<Rigidbody>(); // ���� ���� ������Ʈ 
        PlayerRigd.freezeRotation = true; //ȸ�� ����
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;  //ȭ�� ����
        UnityEngine.Cursor.visible = false; //���콺 �Ⱥ��̰� �ϱ� 
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
            //�߷� �ۿ� �� ����
       //     PlayerRigd.useGravity = true;
        }

    }


    // Update is called once per frame
    void Update()
    {

        PlayerMove(PlayerRigd); //������
        Rotate();

        // Debug.Log(Input.mousePosition); //���콺 ������ ǥ��
    }

    


    private void PlayerMove(Rigidbody _PlayerRigd)
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");


        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * MoveSpeed ; //������ ���� 

        PlayerRigd.MovePosition(transform.position + _velocity * Time.deltaTime);

    }

    private void Rotate()
    {
        float horizontRotat = Input.GetAxis("Mouse X") * MouseSpeed * Time.deltaTime;
        float verticalRotat = Input.GetAxis("Mouse Y") * MouseSpeed * Time.deltaTime;
        YRoatate += horizontRotat;
        XRoatate -= verticalRotat;
        XRoatate = Mathf.Clamp(XRoatate, -90, 90); //���� ���� . �� �Ʒ� 
        MainCam.transform.rotation = Quaternion.Euler(XRoatate, YRoatate, 0);
        transform.rotation = Quaternion.Euler(XRoatate, YRoatate, 0); //�̰͵� �������� �����ִ� ��ǥ���� ���� ���� ���� ��ȯ �����ָ� ���� ���� �����.
    }
}
