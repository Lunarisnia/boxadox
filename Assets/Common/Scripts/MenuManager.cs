using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Canvas mainMenuCanvas;
    public void Quit()
    {
        Application.Quit();
    }

    public void Play(int sceneIndex)
    {
        StartCoroutine(loadLevelAsync(sceneIndex));
    }

    IEnumerator loadLevelAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        operation.allowSceneActivation = false;
        mainMenuCanvas.enabled = false;
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (progress == 1)
            {
                yield return new WaitForSeconds(2f);
                operation.allowSceneActivation = true;
                break;
            }
            yield return null;
        }
    }
}
