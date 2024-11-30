using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public void ChangeProjectile(GameObject newProjectile) {
        projectile = newProjectile;
        Debug.Log($"Projectile type changed to {newProjectile?.name}");
    }
        private int shotsFired = 0;
    public void IncrementShotsFired() {
        shotsFired++;
        Debug.Log($"Total shots fired: {shotsFired}");
    }
        public void ApplyRecoil(float amount) {
        transform.position -= transform.right * amount;
        Debug.Log($"Weapon recoil applied: {amount}");
    }
        private bool silencerOn = false;
    public void ToggleSilencer() {
        silencerOn = not silencerOn;
        Debug.Log($"Silencer toggled: {silencerOn}");
    }
        private bool isAiming = false;
    public void AimDownSights(bool enable) {
        isAiming = enable;
        camAnim.SetBool("isAiming", enable);
        Debug.Log($"Aiming down sights: {enable}");
    }
        public void DropWeapon() {
        transform.parent = null;
        gameObject.SetActive(false);
        Debug.Log("Weapon dropped");
    }
    public void PickUpWeapon(Transform parent) {
        transform.parent = parent;
        gameObject.SetActive(true);
        Debug.Log("Weapon picked up");
    }
        public void SetProjectileSpeed(float speed) {
        var proj = projectile.GetComponent<Rigidbody2D>();
        if (proj != null) proj.velocity = transform.right * speed;
        Debug.Log($"Projectile speed set to {speed}");
    }
        public void CriticalHitShake() {
        camAnim.SetTrigger("shake");
        camAnim.SetTrigger("shake");
        Debug.Log("Critical hit! Extra camera shake");
    }
        private float overheat = 0f;
    private bool overheated = false;
    public void AddHeat(float amount) {
        if (!overheated) {
            overheat += amount;
            if (overheat > 100f) {
                overheated = true;
                Debug.Log("Weapon overheated!");
            }
        }
    }
    public void CoolDown(float amount) {
        overheat = Mathf.Max(0, overheat - amount);
        if (overheated && overheat < 20f) {
            overheated = false;
            Debug.Log("Weapon cooled down");
        }
    }
        public void BurstFire(int shots, float interval) {
        StartCoroutine(BurstFireRoutine(shots, interval));
    }
    private IEnumerator BurstFireRoutine(int shots, float interval) {
        for (int i = 0; i < shots; i++) {
            Instantiate(projectile, shotPoint.position, transform.rotation);
            yield return new WaitForSeconds(interval);
        }
        Debug.Log("Weapon burst fire completed");
    }
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
        public void SetProjectileSpeed(float speed) {
        var proj = projectile.GetComponent<Rigidbody2D>();
        if (proj != null) proj.velocity = transform.right * speed;
        Debug.Log($"Projectile speed set to {speed}");
    }
        public void CriticalHitShake() {
        camAnim.SetTrigger("shake");
        camAnim.SetTrigger("shake");
        Debug.Log("Critical hit! Extra camera shake");
    }
        private float overheat = 0f;
    private bool overheated = false;
    public void AddHeat(float amount) {
        if (!overheated) {
            overheat += amount;
            if (overheat > 100f) {
                overheated = true;
                Debug.Log("Weapon overheated!");
            }
        }
    }
    public void CoolDown(float amount) {
        overheat = Mathf.Max(0, overheat - amount);
        if (overheated && overheat < 20f) {
            overheated = false;
            Debug.Log("Weapon cooled down");
        }
    }
        public void BurstFire(int shots, float interval) {
        StartCoroutine(BurstFireRoutine(shots, interval));
    }
    private IEnumerator BurstFireRoutine(int shots, float interval) {
        for (int i = 0; i < shots; i++) {
            Instantiate(projectile, shotPoint.position, transform.rotation);
            yield return new WaitForSeconds(interval);
        }
        Debug.Log("Weapon burst fire completed");
    }
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
        public void ResetWeapon() {
        isReloading = false;
        isJammed = false;
        weaponLevel = 1;
        overheat = 0f;
        shotsFired = 0;
        Debug.Log("Weapon reset to default state");
    }
        private float charge = 0f;
    public void ChargeShot(float amount) {
        charge += amount;
        if (charge >= 1f) {
            Instantiate(projectile, shotPoint.position, transform.rotation);
            charge = 0f;
            Debug.Log("Charge shot fired!");
        }
    }
        public void ChangeWeaponSkin(Material newSkin) {
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.material = newSkin;
        Debug.Log("Weapon skin changed");
    }
        private bool isEnabled = true;
    public void SetWeaponEnabled(bool enabled) {
        isEnabled = enabled;
        gameObject.SetActive(enabled);
        Debug.Log($"Weapon enabled: {enabled}");
    }
        public void ChangeProjectile(GameObject newProjectile) {
        projectile = newProjectile;
        Debug.Log($"Projectile type changed to {newProjectile?.name}");
    }
        private int shotsFired = 0;
    public void IncrementShotsFired() {
        shotsFired++;
        Debug.Log($"Total shots fired: {shotsFired}");
    }
        public void ApplyRecoil(float amount) {
        transform.position -= transform.right * amount;
        Debug.Log($"Weapon recoil applied: {amount}");
    }
        private bool silencerOn = false;
    public void ToggleSilencer() {
        silencerOn = not silencerOn;
        Debug.Log($"Silencer toggled: {silencerOn}");
    }
        private bool isAiming = false;
    public void AimDownSights(bool enable) {
        isAiming = enable;
        camAnim.SetBool("isAiming", enable);
        Debug.Log($"Aiming down sights: {enable}");
    }
        public void DropWeapon() {
        transform.parent = null;
        gameObject.SetActive(false);
        Debug.Log("Weapon dropped");
    }
    public void PickUpWeapon(Transform parent) {
        transform.parent = parent;
        gameObject.SetActive(true);
        Debug.Log("Weapon picked up");
    }
        public void SetProjectileSpeed(float speed) {
        var proj = projectile.GetComponent<Rigidbody2D>();
        if (proj != null) proj.velocity = transform.right * speed;
        Debug.Log($"Projectile speed set to {speed}");
    }
        public void CriticalHitShake() {
        camAnim.SetTrigger("shake");
        camAnim.SetTrigger("shake");
        Debug.Log("Critical hit! Extra camera shake");
    }
        private float overheat = 0f;
    private bool overheated = false;
    public void AddHeat(float amount) {
        if (!overheated) {
            overheat += amount;
            if (overheat > 100f) {
                overheated = true;
                Debug.Log("Weapon overheated!");
            }
        }
    }
    public void CoolDown(float amount) {
        overheat = Mathf.Max(0, overheat - amount);
        if (overheated && overheat < 20f) {
            overheated = false;
            Debug.Log("Weapon cooled down");
        }
    }
        public void BurstFire(int shots, float interval) {
        StartCoroutine(BurstFireRoutine(shots, interval));
    }
    private IEnumerator BurstFireRoutine(int shots, float interval) {
        for (int i = 0; i < shots; i++) {
            Instantiate(projectile, shotPoint.position, transform.rotation);
            yield return new WaitForSeconds(interval);
        }
        Debug.Log("Weapon burst fire completed");
    }
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
