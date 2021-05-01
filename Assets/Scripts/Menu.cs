using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    [SerializeField] GameObject helpPanel;
    bool helpActive = false;

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void HowToPlay() {
        helpActive = !helpActive;
        helpPanel.SetActive(helpActive);
    }
    public void Quit() {
        Application.Quit();
    }
}
