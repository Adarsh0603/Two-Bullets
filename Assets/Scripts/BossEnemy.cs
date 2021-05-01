using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class BossEnemy : Enemy {

    float life;
    [SerializeField] Slider bossHealthSlider;


    public override void Start() {
        base.Start();
        base.agent.speed = 3f;

        life = 100f;
        StartCoroutine(BossChildEnemyRoutine());
    }


    protected void Update() {
        if (base.agent != null) {
            base.agent.SetDestination(base.player.transform.position);
        }
    }



    public override void BulletHit() {
        BossHit(20f);
    }
    public override void GrenadeHit() {
        BossHit(40f);

    }

    private void BossHit(float damage) {
        life -= damage;
        bossHealthSlider.value = life;
        BloodSplash();

        if (life <= 0) {
            base.Death();
            GameManager.Instance.AddKill(30);
        }
    }

    IEnumerator BossChildEnemyRoutine() {
        while (true) {
            yield return new WaitForSeconds(12f);
            BossChildEnemySpawner();
        }
    }

    private void BossChildEnemySpawner() {
        for (int i = 1; i < transform.childCount; i++) {
            Spawner spawner = transform.GetChild(i).GetComponent<Spawner>();
            GameObject enemy = Instantiate(spawner.itemToSpawn, spawner.transform.position, spawner.transform.rotation);
        }

    }
}
