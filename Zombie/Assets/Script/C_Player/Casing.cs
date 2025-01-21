using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casing : MonoBehaviour
{
    [SerializeField]
    private float deactiveTime = 5.0f;
    [SerializeField]
    private float casingSpin = 1.0f; //탄피 회전
    [SerializeField]
    private AudioClip[] audioClips; //탄피가 부딪쳤을때 재생 


    private Rigidbody rigid;
    private AudioSource audiosource;
    private MemoryPool memoryPool; //메모리 풀 



    public void Setup(MemoryPool memorypool , Vector3 dir)
    {
        rigid = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
        memoryPool = memorypool;


        //탄피 떨어지는거 구현
        rigid.velocity = new Vector3(dir.x, 1.0f, dir.z);
        rigid.angularVelocity = new Vector3(Random.Range(-casingSpin, casingSpin), Random.Range(-casingSpin, casingSpin), Random.Range(-casingSpin, casingSpin));
        //회전은 아무렇게나 회전이 가능


        StartCoroutine("DeactivateAfterTime");


    }


    private void OnCollisionEnder(Collision collision)
    {
       // 충돌 했을때 소리 판정 
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
