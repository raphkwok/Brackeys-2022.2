using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public int storyNum;

    public bool loading;
    // Start is called before the first frame update
    void Start()
    {
        if (gm.gameObject == null)
        {
            gm = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Load(string scene)
    {
        if (loading) return;
        StartCoroutine(LoadScene(scene));
    }

    public void LoadWithDelay(string scene, float time)
    {
        if (loading) return;
    }

    IEnumerator Delay(string scene, float time)
    {
        yield return new WaitForSeconds(time);
        LoadScene(scene);
    }
    IEnumerator LoadScene(string scene)
    {
        yield return null;
        SceneManager.LoadScene(scene);
    }
}
