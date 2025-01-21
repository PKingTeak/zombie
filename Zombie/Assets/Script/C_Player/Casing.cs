using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casing : MonoBehaviour
{
    [SerializeField]
    private float deactiveTime = 5.0f;
    [SerializeField]
    private float casingSpin = 1.0f; //ź�� ȸ��
    [SerializeField]
    private AudioClip[] audioClips; //ź�ǰ� �ε������� ��� 


    private Rigidbody rigid;
    private AudioSource audiosource;
    private MemoryPool memoryPool; //�޸� Ǯ 



    public void Setup(MemoryPool memorypool , Vector3 dir)
    {
        rigid = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
        memoryPool = memorypool;


        //ź�� �������°� ����
        rigid.velocity = new Vector3(dir.x, 1.0f, dir.z);
        rigid.angularVelocity = new Vector3(Random.Range(-casingSpin, casingSpin), Random.Range(-casingSpin, casingSpin), Random.Range(-casingSpin, casingSpin));
        //ȸ���� �ƹ����Գ� ȸ���� ����


        StartCoroutine("DeactivateAfterTime");


    }


    private void OnCollisionEnder(Collision collision)
    {
       // �浹 ������ �Ҹ� ���� 
        int index = Random.Range(0, audioClips.Length);
        audiosource.clip = audioClips[index];
        audiosource.Play();

    }

    private IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(deactiveTime);

        memoryPool.DeactivatePoolItem(this.gameObject);
    
    }

}
