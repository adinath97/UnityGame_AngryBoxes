using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public static int angryBoxCount, playerLives = 3;
    public static bool resetLives;
    
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameManagerScript>().Length;
        if(numGameSessions > 1) {
            Destroy(this.gameObject);
        }
        else {
            DontDestroyOnLoad(this.gameObject);
        }
        angryBoxCount = 0;
        if(resetLives) {
            resetLives = false;
            playerLives = 3;
        }
    }

    public void ProcessPlayerDeath() {
        if(playerLives > 1) {
            TakeLife();
        }
        else {
            ResetGameSession();
        }
    }

    private void TakeLife() {
        playerLives--;
        SceneManagerScript.activateFiniteEndFade = true;
        FindObjectOfType<SceneManagerScript>().DecrementHealth(playerLives);
    }

    private void ResetGameSession() {
        playerLives--;
        resetLives = true;
        GameOverManager.playerLost = true;
        SceneManagerScript.activateEndFade = true;
        FindObjectOfType<SceneManagerScript>().DecrementHealth(playerLives);
        Destroy(this.gameObject);
    }
}
