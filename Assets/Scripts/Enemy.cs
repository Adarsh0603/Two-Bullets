using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour {

    protected NavMeshAgent agent;
    [SerializeField] protected GameObject _bloodSplash;
    protected Player player;
    [SerializeField]
    private AudioClip deathAudio;


    public virtual void Start() {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
        agent.speed = Random.Range(8, 11);
    }

    void Update() {
        if (agent != null)
            agent.SetDestination(player.transform.position);

    }

    public virtual void BulletHit() {
        Death();
    }
    public virtual void GrenadeHit() {
        Death();

    }
    protected void Death() {
        AudioSource.PlayClipAtPoint(deathAudio, player.transform.position);
        GameManager.Instance.AddKill(1);
        Destroy(gameObject);
        BloodSplash();

    }
    protected void BloodSplash() {
        GameObject bloodSplash = Instantiate(_bloodSplash, transform.position, Quaternion.identity);
        Destroy(bloodSplash, 1.5f);
    }
}
