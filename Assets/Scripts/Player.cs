using UnityEngine;
public class Player : MonoBehaviour {
    [SerializeField] float playerMoveSpeed = 20f;
    [SerializeField] float turnSpeed = 4f;
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] GameObject grenadeLauncher;
    [SerializeField] float throwSpeed;
    Rigidbody rb;
    Vector3 movement;
    private float health;

    Vector3 mousePos;
    void Start() {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        health = 100;
    }

    void Update() {
        movement = new Vector3(-Input.GetAxisRaw("Vertical"), 0f, Input.GetAxisRaw("Horizontal")).normalized;
        if (Input.GetButtonDown("Fire2") && GameManager.Instance.IsGrenadeActive() && !GameManager.Instance.IsDead() && !GameManager.Instance.IsPaused()) {
            ThrowGrenade();
        }
    }



    private void FixedUpdate() {
        rb.AddForce(movement * playerMoveSpeed * 10 * Time.deltaTime - rb.velocity, ForceMode.VelocityChange);
        LookAtMouse();
    }
    private void LookAtMouse() {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitdist = 0.0f;
        if (playerPlane.Raycast(ray, out hitdist)) {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
    private void OnCollisionStay(Collision collision) {
        if (collision.collider.tag == "Enemy") {
            health -= 0.2f;
            GameManager.Instance.UpdateHealth(health);
        }

    }


    private void ThrowGrenade() {
        GameObject grenade = Instantiate(grenadePrefab, grenadeLauncher.transform.position, transform.localRotation);
        grenade.GetComponent<Rigidbody>().AddForce(grenadeLauncher.transform.forward * throwSpeed, ForceMode.Impulse);
        GameManager.Instance.SetGrenadeActive(false);
    }

}
