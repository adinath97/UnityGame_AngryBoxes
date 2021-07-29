using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] Text gameOverText;
    public static bool playerWon, playerLost;
    
    void Awake()
    {
        if(!playerLost) {
            gameOverText.text = "YAY! YOU WON :-)";
        }
        else if(playerLost) {
            playerLost = false;
            gameOverText.text = "YOU LOST :-(";
        }
    }
}
