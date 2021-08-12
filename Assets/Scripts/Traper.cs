using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class Traper : MonoBehaviour
{
    public SkeletonAnimation anim;

    [SpineAtlasRegion] public SpineSkin skin;

    [SpineAnimation] public string attack;

    public void PlayAninmationAttack(string _strAnim)
    {
        if (!anim.AnimationName.Equals(_strAnim))
        {

            anim.AnimationState.SetAnimation(0, _strAnim, false);
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            PlayAninmationAttack( attack);
           // AdsManager.Instance.traped = true;
        }
    }
    
}
