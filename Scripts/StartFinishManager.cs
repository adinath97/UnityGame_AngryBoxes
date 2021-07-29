using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class StartFinishManager : MonoBehaviour
{
    [SerializeField] GameObject startFade;
    [SerializeField] GameObject endFade;
    [SerializeField] AudioClip buttonSoundEffect;
    private bool loadNewScene;
    private AudioSource myAudioSource;

    void Awake()
    {
        myAudioSource = this.GetComponent<AudioSource>();
        StartCoroutine(StartingRoutine());
        if(SceneManager.GetActiveScene().buildIndex == 4) {
            GameManagerScript.playerLives = 3;
        }
    }

    private IEnumerator StartingRoutine() {
        startFade.SetActive(true);
        endFade.SetActive(false);
        yield return new WaitForSeconds(.5f);
        startFade.SetActive(false);
        loadNewScene = true;
    }
    
    public void LoadStartMenu()
    {
        if(loadNewScene) {
            loadNewScene = false;
            myAudioSource.PlayOneShot(buttonSoundEffect);
            StartCoroutine(LoadStartMenuCoroutine());
        }
    }

    private IEnumerator LoadStartMenuCoroutine() {
        endFade.SetActive(true);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("MenuScene");
    }

    public void LoadLevelOne() {
        if(loadNewScene) {
            loadNewScene = false;
            myAudioSource.PlayOneShot(buttonSoundEffect);
            StartCoroutine(LoadLevelOneCoroutine());
        }
    }

    private IEnumerator LoadLevelOneCoroutine() {
        endFade.SetActive(true);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Game1Scene");
    }

    public void LoadInstructions() {
        if(loadNewScene) {
            loadNewScene = false;
            myAudioSource.PlayOneShot(buttonSoundEffect);
            StartCoroutine(LoadInstructionsCoroutine());
        }
    }

    private IEnumerator LoadInstructionsCoroutine() {
        endFade.SetActive(true);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("InstructionsScene");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
