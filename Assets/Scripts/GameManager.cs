using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    [SerializeField] Slider playerHealth;
    [SerializeField] TextMeshProUGUI killsText;
    [SerializeField] GameObject grenadeImage;
    [SerializeField] GameObject deathPanel;
    [SerializeField] TextMeshProUGUI killScore;
    [SerializeField] TextMeshProUGUI highScore;
    [SerializeField] GameObject bossText;
    [SerializeField] GameObject pausePanel;
    bool _isPaused = false;
    private AudioSource audioSource;
    private bool _grenadeActive = false;
    private int activeEnemyCount = 0;
    private int kills = 0;
    private bool isDead = false;
    private int highestKills;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }
    public void Start() {
        deathPanel.SetActive(false);
        playerHealth.value = 100;
        kills = 0;
        highestKills = PlayerPrefs.GetInt("kills", 0);

        grenadeImage.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.1f;

    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !isDead) {
            PauseResume();
        }
        if (audioSource.volume < 0.1f) {
            audioSource.volume = 0.1f;
        }
    }
    private void PauseResume() {
        if (!_isPaused) {
            _isPaused = true;
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            Cursor.visible = true;

        }
        else {
            _isPaused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            Cursor.visible = false;

        }
    }

    public void BossIncoming(bool status) {
        if (!isDead)
            bossText.SetActive(status);

    }
    public void UpdateHealth(float health) {

        if (health <= 0 && !isDead) {
            deathPanel.SetActive(true);
            bossText.SetActive(false);
            Cursor.visible = true;
            isDead = true;
            HighScoreManagement();
            SetGrenadeActive(false);
            killScore.text = "X " + kills.ToString();
        }
        playerHealth.value = health;
    }
    public bool IsDead() {
        return isDead;
    }

    private void HighScoreManagement() {
        if (kills > highestKills) {
            highScore.color = Color.green;
            PlayerPrefs.SetInt("kills", kills);
            highestKills = kills;
        }
        else {
            highScore.color = Color.white;
        }
        highScore.text = "HighScore : " + highestKills;

    }
    public void AddKill(int killsToAdd) {
        if (killsToAdd == 30) {
            SetGrenadeActive(true);
        }
        kills += killsToAdd;
        activeEnemyCount--;
        audioSource.volume = activeEnemyCount / 50f;

        killsText.text = kills.ToString();
        if (kills % 20 == 0) {
            SetGrenadeActive(true);
        }


    }

    public void Restart() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackToMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void AddActiveEnemyCount() {
        activeEnemyCount++;
        if (audioSource != null)
            audioSource.volume = activeEnemyCount / 50f;

    }
    public void SetGrenadeActive(bool active) {
        _grenadeActive = active;
        if (active) {
            grenadeImage.SetActive(true);
        }
        else {
            grenadeImage.SetActive(false);
        }
    }
    public bool IsGrenadeActive() {
        return _grenadeActive;
    }
    public bool IsPaused() {
        return _isPaused;
    }
}
