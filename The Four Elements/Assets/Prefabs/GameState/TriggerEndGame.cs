using UnityEngine;

public class TriggerEndGame : MonoBehaviour
{
    public void EndTheGame()
    {
        GameObject.FindWithTag("Canvas").GetComponentInChildren<Animator>().SetTrigger("EndGame");
        print("ended game");
    }
}
