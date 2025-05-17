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

public void SitDown(Animator animator) {
    animator.SetTrigger("sitDown");
}

public void Blink(Animator animator) {
    animator.SetTrigger("blink");
}

public void HandWave(Animator animator) {
    animator.SetTrigger("handWave");
}

public AudioClip humClip;
public void PlayIdleHum(AudioSource source) {
    if (humClip != null) source.PlayOneShot(humClip);
}

public void EarTwitch(Animator animator) {
    animator.SetTrigger("earTwitch");
}

public void TailWag(Animator animator) {
    animator.SetTrigger("tailWag");
}

public void Shiver(Animator animator) {
    animator.SetTrigger("shiver");
}

public void Sneeze(Animator animator, ParticleSystem sneezeEffect) {
    animator.SetTrigger("sneeze");
    if (sneezeEffect != null) sneezeEffect.Play();
}

public void StretchAndYawn(Animator animator) {
    animator.SetTrigger("stretchAndYawn");
}

public void LookAtCamera(Animator animator) {
    animator.SetTrigger("lookAtCamera");
}

public void IdleJumpScare(Animator animator) {
    animator.SetTrigger("jumpScare");
}

public void CoinFlip(Animator animator) {
    animator.SetTrigger("coinFlip");
}

public void Juggle(Animator animator) {
    animator.SetTrigger("juggle");
}

public void IdleDance(Animator animator) {
    animator.SetTrigger("idleDance");
}

