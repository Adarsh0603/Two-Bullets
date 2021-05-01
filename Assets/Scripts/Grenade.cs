using UnityEngine;

public class Grenade : MonoBehaviour {

    [SerializeField] GameObject explosionFX;
    [SerializeField] AudioClip explosionSound;

    private void Start() {
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Ground") {
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position);
            Blast();
        }
    }

    public void Blast() {
        if (GameManager.Instance.IsDead()) {
            return;
        }
        GameObject explosion = Instantiate(explosionFX, transform.position, Quaternion.identity);
        Collider[] explosionColliders = Physics.OverlapSphere(transform.position, 3f);
        foreach (Collider collider in explosionColliders) {
            if (collider.gameObject.tag == "Enemy") {
                Enemy enemy = collider.gameObject.GetComponent<Enemy>();
                enemy.GrenadeHit();
            }
        }
        Destroy(explosion, 2f);
        Destroy(gameObject);
    }
}
