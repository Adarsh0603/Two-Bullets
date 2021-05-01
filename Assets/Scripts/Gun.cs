using UnityEngine;

public class Gun : MonoBehaviour {
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce = 20f;
    [SerializeField] SpriteRenderer crosshair;
    [SerializeField] AudioClip[] audios;
    AudioSource audioSource;

    int maxBulletCount = 2;
    int shotBulletCount = 0;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update() {
        if (Input.GetButtonDown("Fire1") && !GameManager.Instance.IsDead() && !GameManager.Instance.IsPaused()) {
            Shoot();
        }
        if (shotBulletCount == 2) {
            crosshair.color = Color.grey;
        }
        else {
            crosshair.color = Color.white;
        }
    }
    private void Shoot() {

        if (shotBulletCount < maxBulletCount) {
            shotBulletCount++;
            audioSource.clip = audios[1];
            audioSource.Play();

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();


            bulletRb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        }
        else {
            audioSource.clip = audios[0];
            audioSource.Play();

        }

    }
    public void AddBullet() {
        audioSource.clip = audios[2];
        audioSource.Play();
        shotBulletCount--;
    }
}
