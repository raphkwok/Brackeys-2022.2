using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm; // Static reference so other objects can access this script
    public int storyNum; // Record which loop
    public GameObject[][] storyObjects; // first column is the story number, second column is the array of objects to be activated
    public bool loading;
    public Animator transition = null;
    public float sceneTransitionTime;
    // Start is called before the first frame update
    void Start()
    {
        // this might not be right idk I'm trying something new
        // Idea is to delete itself if there is already another gamemanager in the scene
        if (gm == null || gm.gameObject == null)
        {
            gm = this;
        }
        else
        {
            Destroy(gameObject);
        }

        gm = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Called by menu to trigger game start
    public void Load(string scene)
    {
        if (loading) return;
        loading = true;
        StartCoroutine(LoadScene(scene));
    }

    // probably use this for menu later for animations but idk might be useful
    public void LoadWithDelay(string scene, float time)
    {
        if (loading) return;

        loading = true;
        StartCoroutine(Delay(scene, time));
    }
    // Delay coroutine for load with delay
    IEnumerator Delay(string scene, float time)
    {
        yield return new WaitForSeconds(time);
        LoadScene(scene);
    }

    // Actually loads scene
    public IEnumerator LoadScene(string sceneName)
    {
        if (transition != null) transition.SetTrigger("Transition"); // Start transitioning scene out


        yield return new WaitForSeconds(sceneTransitionTime); // Wait for transition

        // Start loading scene
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);
        load.allowSceneActivation = false;
        while (!load.isDone) // Could make a loading bar here :shrug:
        {
            if (load.progress >= 0.9f)
            {
                load.allowSceneActivation = true;
            }

            yield return null;
        }
        load.allowSceneActivation = true;

        yield return null;

        // if (sceneName != "Main Menu") OnLevelLoad();

        //  Set volume
        // AudioListener.volume = volume;


        gm = this; // Reset self as GameManager instance

        if (transition != null) transition.SetTrigger("Transition"); // Start transitioning scene back


        yield return new WaitForSeconds(sceneTransitionTime); // Wait for transition
        loading = false;
    }

    // Call when loop is finished
    public void Loop()
    {
        Load("Main Menu");
        // if (SceneManager.GetActiveScene().name == "Level")
        // {
        //     storyNum++;
        //     Load("Intro");

        // }
        // else
        // {
        //     Load("Level");
        // }
        // Might be a good time to check if the story is over
    }

    // Call when car runs into death wall
    public void Death()
    {
        // Just reset current scene
        Load(SceneManager.GetActiveScene().name);
    }

    public void OnLevelLoad()
    {
        // Activate objects in story
        for (int i = 0; i <= storyNum; i++)
        {
            // Loop through last loops objects and also current loop
            for (int j = 0; j <= storyObjects[i].Length; j++)
            {
                if (storyObjects[i][j] != null) storyObjects[i][j].SetActive(true);
            }
        }
    }
}
