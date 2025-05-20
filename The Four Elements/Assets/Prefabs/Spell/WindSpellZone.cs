using Cinemachine;
using UnityEngine;

public class WindSpellZone : MonoBehaviour
{
    public bool HitPlayer = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<WindReactionManager>(out WindReactionManager script))
        {
            script.ReactToEffect();
        } else if (other.CompareTag("Player") && !HitPlayer)
        {
            HitPlayer = true;
            CinemachineVirtualCamera playerCamera = GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>();
            Vector3 force = (playerCamera.Follow.position - playerCamera.transform.position).normalized;
            
            other.gameObject.GetComponent<CharacterController>().Move(force);
            print("hit player");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
