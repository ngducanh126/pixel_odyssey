using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour {


    private float timer;
    public float minTime;
    public float maxTime;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        timer = Random.Range(minTime, maxTime);
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (timer <= 0)
        {
            animator.SetTrigger("jump");
        }
        else {
            timer -= Time.deltaTime;
        }
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}


}
public void TriggerStretch(Animator animator) {
    animator.SetTrigger("stretch");
}

public void LookAround(Animator animator) {
    animator.SetTrigger("lookAround");
}

public AudioClip whistleClip;
public void PlayIdleWhistle(AudioSource source) {
    if (whistleClip != null) source.PlayOneShot(whistleClip);
}

public void TriggerYawn(Animator animator) {
    animator.SetTrigger("yawn");
}

public void FootTap(Animator animator) {
    animator.SetTrigger("footTap");
}

public void HeadScratch(Animator animator) {
    animator.SetTrigger("headScratch");
}

public AudioClip sighClip;
public void PlayIdleSigh(AudioSource source) {
    if (sighClip != null) source.PlayOneShot(sighClip);
}

