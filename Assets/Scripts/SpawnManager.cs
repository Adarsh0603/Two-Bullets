using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] Spawner[] enemySpawners;
    [SerializeField] float secondsPerEnemySpawn = 1;
    [SerializeField] Transform enemyParent;
    [SerializeField] GameObject bossEnemy;

    private void Start() {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(DifficultyRoutine());
        StartCoroutine(BossSpawnRoutine());
    }



    IEnumerator EnemySpawnRoutine() {
        while (!GameManager.Instance.IsDead()) {
            int spawnerId = Random.Range(0, enemySpawners.Length);
            Spawner selectedSpawner = enemySpawners[spawnerId];
            GameObject enemy = Instantiate(selectedSpawner.itemToSpawn, selectedSpawner.transform.position, selectedSpawner.transform.rotation);

            enemy.transform.SetParent(enemyParent);
            GameManager.Instance.AddActiveEnemyCount();
            yield return new WaitForSeconds(secondsPerEnemySpawn);
        }
    }
    IEnumerator BossSpawnRoutine() {
        while (!GameManager.Instance.IsDead()) {

            yield return new WaitForSeconds(60f);
            GameManager.Instance.BossIncoming(true);
            yield return new WaitForSeconds(3f);
            GameManager.Instance.BossIncoming(false);
            int spawnerId = Random.Range(0, enemySpawners.Length);
            Spawner selectedSpawner = enemySpawners[spawnerId];
            GameObject boss = Instantiate(bossEnemy, selectedSpawner.transform.position, selectedSpawner.transform.rotation);
        }
    }
    IEnumerator DifficultyRoutine() {
        while (secondsPerEnemySpawn >= 0.75f) {
            if (secondsPerEnemySpawn > 1)
                yield return new WaitForSeconds(5f);
            else
                yield return new WaitForSeconds(20f);
            secondsPerEnemySpawn -= 0.20f;
        }
    }
}
