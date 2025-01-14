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
    //이거 문법에 대해서 공부좀 해야될듯 


    // Update is called once per frame
    public void AnimationPlay(string stateName, int layer, float normalizedTime)
    {
        P_Animator.Play(stateName, layer, normalizedTime);  


    }
}
