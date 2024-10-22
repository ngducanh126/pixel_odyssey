using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    private int weaponLevel = 1;
    public void UpgradeWeapon() {
        weaponLevel++;
        Debug.Log($"Weapon upgraded to level {weaponLevel}");
    }
        private bool isJammed = false;
    public void JamWeapon() {
        isJammed = true;
        Debug.Log("Weapon jammed!");
    }
    public void UnjamWeapon() {
        isJammed = false;
        Debug.Log("Weapon unjammed");
    }
        public void PlayFireSound(AudioSource fireSound) {
        if (fireSound != null) fireSound.Play();
        Debug.Log("Weapon fire sound played");
    }
        public void PlayMuzzleFlash(ParticleSystem muzzleFlash) {
        if (muzzleFlash != null) muzzleFlash.Play();
        Debug.Log("Weapon muzzle flash effect played");
    }
        public void AlternateFire() {
        camAnim.SetTrigger("shake");
        Instantiate(projectile, shotPoint.position, transform.rotation * Quaternion.Euler(0,0,15));
        Instantiate(projectile, shotPoint.position, transform.rotation * Quaternion.Euler(0,0,-15));
        Debug.Log("Weapon alternate fire triggered");
    }
        private bool isReloading = false;
    public void Reload(float reloadTime) {
        if (!isReloading) {
            isReloading = true;
            StartCoroutine(ReloadRoutine(reloadTime));
            Debug.Log("Weapon reload started");
        }
    }
    private IEnumerator ReloadRoutine(float reloadTime) {
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        Debug.Log("Weapon reload finished");
    }
    
    public GameObject projectile;
    public Transform shotPoint;
    public Animator camAnim;

    public int rotationOffset;


    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);

        if (Input.GetMouseButtonDown(0)) {
            camAnim.SetTrigger("shake");
            Instantiate(projectile, shotPoint.position, transform.rotation);
        }
    }
}
