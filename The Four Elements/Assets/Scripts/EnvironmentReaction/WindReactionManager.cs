using UnityEngine;

public class WindReactionManager : EnvironmentReactionManager
{
    public override void ReactToEffect()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.right * 100);
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
