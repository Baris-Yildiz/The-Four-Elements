using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEndGame : MonoBehaviour
{
    public void EndTheGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
