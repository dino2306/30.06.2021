using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private AnimationNvNu spriteAnimation;
    [SerializeField] private Sprite[] idleAnimationFrameArray;
    [SerializeField] private Sprite[] walkAnimationFrameArray;
    [SerializeField] private Sprite[] jumpAnimationFreamArray;

    private enum AnimationType
    {
        Idle,
        walk,
        jump,
    }
    private AnimationType activeAnimationType;
    // Start is called before the first frame update
    void Start()
    {
        //spriteAnimation.OnAnimation += SpriteAnimation_OnAnimation;
        //spriteAnimation.OnAnimationloopedFirstTime += SpriteAnimation_OnAnimationloopedFirstTime;
        PlayAnimations(AnimationType.Idle);
    }

    private void SpriteAnimation_OnAnimationloopedFirstTime(object sender, System.EventArgs e)
    {
        Debug.Log("Fisttime");
    }

    private void SpriteAnimation_OnAnimation(object sender, System.EventArgs e)
    {
        Debug.Log("lopppped");
    }

    // Update is called once per frame
    void Update()
    {
        bool isMoving = false;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Space))
        {
            isMoving = true;
            
        }

        if (isMoving)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                PlayAnimations(AnimationType.walk);
               

            }
            if(Input.GetKey(KeyCode.Space))
            {
                PlayAnimations(AnimationType.jump);
            }
        }
        else
        {
            PlayAnimations(AnimationType.Idle);
        }
       
    }
    private void PlayAnimations(AnimationType animationType)
    {
        if (animationType != activeAnimationType)
        {
            activeAnimationType = animationType;
            switch (animationType)
            {
                case AnimationType.Idle:
                    spriteAnimation.PlayAnimation(idleAnimationFrameArray, .2f);
                    break;
                case AnimationType.walk:
                    spriteAnimation.PlayAnimation(walkAnimationFrameArray, .1f);
                    break;
                case AnimationType.jump:
                    spriteAnimation.PlayAnimation(jumpAnimationFreamArray, 1f);
                    break;
            }
        }
    }
   
}


