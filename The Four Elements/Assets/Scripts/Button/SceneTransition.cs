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
        Debug.Log("dsadasdsa");
        StartCoroutine(LoadYourAsyncScene());
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
            // Update progress bar
            if (progressBar != null)
            {
                // operation.progress goes from 0.0 to 0.9. When it reaches 0.9, it means the scene is loaded
                // but not yet activated.
                progressBar.value = Mathf.Clamp01(operation.progress / 0.9f);
            }

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