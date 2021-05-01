using UnityEngine;

public class Bullet : MonoBehaviour {
    private Gun gun;
    private Player player;
    private MeshRenderer meshR;
    int hits = 0;
    int maxHits = 3;
    private void Start() {
        gun = FindObjectOfType<Gun>();
        meshR = GetComponent<MeshRenderer>();
        player = FindObjectOfType<Player>();

    }
    private void OnCollisionEnter(Collision collision) {
        if (GameManager.Instance.IsDead()) {
            return;
        }
        if (collision.collider.tag == "Player") {
            Destroy(gameObject);
            gun.AddBullet();
        }
        else if (collision.collider.tag == "Enemy") {
            Enemy enemy = collision.collider.GetComponent<Enemy>();
            if (hits < maxHits) {
                enemy.BulletHit();
                hits++;
            }
            if (hits == maxHits) {
                meshR.material.color = Color.green;
            }
        }
    }



}
