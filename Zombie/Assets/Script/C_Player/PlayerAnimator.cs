using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator P_Animator;
    // Start is called before the first frame update
    private void Awake()
    {
        P_Animator = GetComponentInChildren<Animator>();
    }

  
    public float MovementAnimation
    {
        set => P_Animator.SetFloat("MovementParameter",value);
        get => P_Animator.GetFloat("MovementParameter");
    }
    //�̰� ������ ���ؼ� ������ �ؾߵɵ� 


    // Update is called once per frame
    public void AnimationPlay(string stateName, int layer, float normalizedTime)
    {
        P_Animator.Play(stateName, layer, normalizedTime);  


    }
}
