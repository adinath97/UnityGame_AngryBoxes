using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] GameObject startFade;
    [SerializeField] GameObject endFade;
    [SerializeField] GameObject UIBackgroundImage;
    [SerializeField] GameObject InstructionsText;
    [SerializeField] GameObject playBtn;
    [SerializeField] GameObject playerLivesBox;
    [SerializeField] GameObject angryBoxCountBox;
    [SerializeField] AudioClip buttonSoundEffect;
    [SerializeField] List<GameObject> healthImages = new List<GameObject>();
    [SerializeField] List<GameObject> angryBoxImages = new List<GameObject>();

    public static bool beginGame, gameOver, activateEndFade, activateFiniteEndFade, allowPlayBtnPress;
    private int angryBoxCounter, healthImageCounter;

    private AudioSource myAudioSource;

    void Awake()
    {
        StartCoroutine(StartingRoutine());
        myAudioSource = this.GetComponent<AudioSource>();
        angryBoxCounter = 0;
        healthImageCounter = 0;
        gameOver = false;
        beginGame = false;
        activateEndFade = false;
        activateFiniteEndFade = false;
        playerLivesBox.SetActive(false);
        angryBoxCountBox.SetActive(false);
        UIBackgroundImage.SetActive(true);
        InstructionsText.SetActive(true);
        playBtn.SetActive(true);
        foreach(GameObject x in healthImages) {
            x.SetActive(false);
        }
        foreach(GameObject x in angryBoxImages) {
            x.GetComponent<Image>().color = new Color(1f,1f,1f,.25f);
            x.SetActive(false);
        }
    }

    private IEnumerator StartingRoutine() {
        endFade.SetActive(false);
        startFade.SetActive(true);
        yield return new WaitForSeconds(.5f);
        startFade.SetActive(false);
        allowPlayBtnPress = true;
    }

    void Update()
    {

        if(gameOver && GameManagerScript.angryBoxCount == 5) {
            gameOver = false;
            StartCoroutine(LoadNextLevel());
        }
        if(activateEndFade) {
            activateEndFade = false;
            StartCoroutine(EndingRoutine());
        }
        if(activateFiniteEndFade) {
            activateFiniteEndFade = false;
            StartCoroutine(FiniteEndingRoutine());
        }
        playerLivesBox.GetComponent<Text>().text = "LIVES: " + GameManagerScript.playerLives.ToString();
        angryBoxCountBox.GetComponent<Text>().text = "ANGRY BOXES ACTIVATED: " + GameManagerScript.angryBoxCount.ToString() + " / 5";
    }

    private IEnumerator LoadNextLevel() {
        endFade.SetActive(true);
        yield return new WaitForSeconds(.5f);
        if(SceneManager.GetActiveScene().buildIndex == 1 ||
            SceneManager.GetActiveScene().buildIndex == 2) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if(SceneManager.GetActiveScene().buildIndex == 3) {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    private IEnumerator FiniteEndingRoutine() {
        endFade.SetActive(true);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator EndingRoutine() {
        endFade.SetActive(true);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("GameOverScene");
    }

    public void DecrementHealth(int playerLives) {
        if(playerLives == 2) {
            healthImages[playerLives].GetComponent<Image>().color = new Color(1f,1f,1f,.25f);
        }
        if(playerLives == 1) {
            healthImages[playerLives].GetComponent<Image>().color = new Color(1f,1f,1f,.25f);
            healthImages[playerLives + 1].GetComponent<Image>().color = new Color(1f,1f,1f,.25f);
        }
        
    }

    public void IncrementAngryBox() {
        angryBoxImages[angryBoxCounter].GetComponent<Image>().color = new Color(1f,1f,1f,1f);
        angryBoxCounter++;
    }

    public void StartLevel() {
        if(allowPlayBtnPress) {
            allowPlayBtnPress = false;
            myAudioSource.PlayOneShot(buttonSoundEffect);
            StartCoroutine(RestartLevelRoutine());
        }
    }

    private IEnumerator RestartLevelRoutine() {
        endFade.SetActive(true);
        yield return new WaitForSeconds(.5f);
        beginGame = true;
        Player.isAlive = true;
        EnemyOneMovement.beginGame = true;
        foreach(GameObject x in angryBoxImages) {
            x.GetComponent<Image>().color = new Color(1f,1f,1f,.25f);
            x.SetActive(true);
        }
        if(GameManagerScript.playerLives == 2) {
            healthImages[GameManagerScript.playerLives].GetComponent<Image>().color = new Color(1f,1f,1f,.25f);
            healthImages[GameManagerScript.playerLives - 1].GetComponent<Image>().color = new Color(1f,1f,1f,1f);
            healthImages[GameManagerScript.playerLives - 2].GetComponent<Image>().color = new Color(1f,1f,1f,1f);
        }
        if(GameManagerScript.playerLives == 1) {
            healthImages[GameManagerScript.playerLives].GetComponent<Image>().color = new Color(1f,1f,1f,.25f);
            healthImages[GameManagerScript.playerLives + 1].GetComponent<Image>().color = new Color(1f,1f,1f,.25f);
            healthImages[GameManagerScript.playerLives - 1].GetComponent<Image>().color = new Color(1f,1f,1f,1f);
        }
        foreach(GameObject x in healthImages) {
            x.SetActive(true);
        }
        UIBackgroundImage.SetActive(false);
        InstructionsText.SetActive(false);
        playBtn.SetActive(false);
        endFade.SetActive(false);
        StartCoroutine(ShowLevelOneRoutine());
    }

    private IEnumerator ShowLevelOneRoutine() {
        startFade.SetActive(true);
        yield return new WaitForSeconds(.5f);
        startFade.SetActive(false);
    }
}
