using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Required for IEnumerator
using UnityEngine.UI;

public class SceneTranstion : MonoBehaviour
{
    public string sceneToLoadName = "MyNextScene";
    public GameObject loadingScreen; // Assign a UI panel for the loading screen
    public Slider progressBar; // Assign a UI Slider for progress (optional)

    public void LoadSceneAsync()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadYourAsyncScene());
        Cursor.visible = false;
    }

    IEnumerator LoadYourAsyncScene()
    {
        // Show loading screen
        if (loadingScreen != null)
            loadingScreen.SetActive(true);

        // Start loading the scene asynchronously
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoadName);

        // Don't activate the new scene until it's fully loaded and you allow it
        operation.allowSceneActivation = false;

        // Loop while the scene is not yet loaded
        while (!operation.isDone)
        {
       

            // Check if the scene is almost loaded (progress is near 0.9)
            if (operation.progress >= 0.9f)
            {
                // You can add a "Press any key to continue" or other logic here
                // For now, let's just activate it when it's ready.
                operation.allowSceneActivation = true;
            }

            yield return null; // Wait for the next frame
        }

        // Hide loading screen after scene is loaded and activated
        if (loadingScreen != null)
            loadingScreen.SetActive(false);
    }
}