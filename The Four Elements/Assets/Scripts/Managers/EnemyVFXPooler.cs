using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVFXPooler : MonoBehaviour
{
    public static EnemyVFXPooler Instance;

    [SerializeField] private GameObject redPrefab;
    [SerializeField] private GameObject greenPrefab;
    [SerializeField] private GameObject bluePrefab;

    [SerializeField] private int poolSize = 10;

    private Queue<ParticleSystem> redPool = new Queue<ParticleSystem>();
    private Queue<ParticleSystem> greenPool = new Queue<ParticleSystem>();
    private Queue<ParticleSystem> bluePool = new Queue<ParticleSystem>();

    void Awake()
    {
        Instance = this;

        InitPool(redPrefab, redPool);
        InitPool(greenPrefab, greenPool);
        InitPool(bluePrefab, bluePool);
    }

    private void InitPool(GameObject prefab, Queue<ParticleSystem> queue)
    {
        for (int i = 0; i < poolSize; i++)
        {
            var obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            var ps = obj.GetComponent<ParticleSystem>();
            queue.Enqueue(ps);
        }
    }

    public void PlayParticle(Vector3 position, float attackSpeed, Color color)
    {
        Queue<ParticleSystem> selectedPool = GetPoolByColor(color);

        if (selectedPool == null || selectedPool.Count == 0) return;

        var ps = selectedPool.Dequeue();
        ps.transform.position = position;

        // Set color module
        var main = ps.main;
        main.startColor = color;
        main.duration = attackSpeed;
        ps.gameObject.SetActive(true);
        
        ps.Play();

        StartCoroutine(ReturnToPoolAfter(ps, main.duration, selectedPool));
    }

    private IEnumerator ReturnToPoolAfter(ParticleSystem ps, float delay, Queue<ParticleSystem> returnPool)
    {
        yield return new WaitForSeconds(delay);
        ps.Stop();
        ps.gameObject.SetActive(false);
        returnPool.Enqueue(ps);
    }

    private Queue<ParticleSystem> GetPoolByColor(Color color)
    {
        if (color == Color.red) return redPool;
        if (color == Color.green) return greenPool;
        if (color == Color.blue) return bluePool;
        return null;
    }
}
