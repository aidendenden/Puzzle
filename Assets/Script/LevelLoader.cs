using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public int sceneNum;

    private void Update()
    {
       
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1;
        //StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        StartCoroutine(LoadLevel(sceneNum));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

}
