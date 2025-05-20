using System.Collections;
using Cinemachine;
using StarterAssets;
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
            //Vector3 force = (playerCamera.Follow.position - playerCamera.transform.position).normalized;
            Vector3 force = Vector3.up * 0.1f;
            StartCoroutine(MoveChar(other.gameObject , force , 0.5f));
            print("hit player");
        }
    }

    IEnumerator MoveChar(GameObject obj,Vector3 dir , float sec)
    {
        obj.GetComponent<ThirdPersonController>().wind = 0f;
        while ((sec -= Time.unscaledDeltaTime) > 0)
        {
            obj.GetComponent<CharacterController>().Move(dir);
            yield return null;
        }
        obj.GetComponent<ThirdPersonController>().wind = 1;
    }
}
