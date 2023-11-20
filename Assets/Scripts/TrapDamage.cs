using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private AudioSource hitTrapAudioSource;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TrapDamage calling");
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(25f);
            hitTrapAudioSource.Play();
        }
    }
}