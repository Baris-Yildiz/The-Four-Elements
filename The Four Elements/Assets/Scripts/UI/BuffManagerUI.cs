using System;
using UnityEngine;
using UnityEngine.UI;

public class BuffManagerUI : MonoBehaviour
{

    private BuffManager _buffManager;
    [SerializeField] private GameObject buffPrefab;

    [SerializeField] private int poolAmount;
    private GameObject[] buffPrefabs;
    private int currIndex = 0;

    private void Awake()
    {
        _buffManager = GetComponentInParent<BuffManager>();
        if (_buffManager == null)
        {
            _buffManager = GameObject.FindGameObjectWithTag("Player").GetComponent<BuffManager>();
        }

        buffPrefabs = new GameObject[poolAmount];
        for (int i = 0; i < poolAmount; i++)
        {
            buffPrefabs[i] = Instantiate(buffPrefab, transform.position ,transform.rotation);
            buffPrefabs[i].transform.SetParent(transform);
            buffPrefabs[i].SetActive(false);
        }
    }

    private void OnEnable()
    {
        _buffManager.OnBuffAdded += AddBuff;
        _buffManager.OnBuffRemoved += RemoveBuff;
    }

    private void OnDisable()
    {
        _buffManager.OnBuffAdded -= AddBuff;
        _buffManager.OnBuffRemoved -= RemoveBuff;
        
    }

    void AddBuff(Buff buff)
    {
        int count = poolAmount;
        while (buffPrefabs[currIndex].activeSelf && count > 0)
        {
            currIndex = (currIndex + 1) % poolAmount;
            count--;
        }
        Sprite sprite = buff.effectIcon;
        buffPrefabs[currIndex].GetComponent<Image>().sprite = sprite;
        buffPrefabs[currIndex].SetActive(true);
    }

    void RemoveBuff(Buff buff)
    {
        for (int i = 0; i < poolAmount; i++)
        {
            if (buffPrefabs[i].activeSelf && buffPrefabs[i].GetComponent<Image>().sprite == buff.effectIcon)
            {
                buffPrefabs[i].SetActive(false);
            }
        }
    }
    
}
