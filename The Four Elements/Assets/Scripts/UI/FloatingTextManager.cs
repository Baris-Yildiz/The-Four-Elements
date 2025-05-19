using System;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    private EntityStats entityStats;
    [SerializeField] private GameObject floatingText;

    [SerializeField] private int textAmount;

    private GameObject[] floatingTexts;
    private int currIndex;
    private void Awake()
    {
        floatingTexts = new GameObject[textAmount];
        entityStats = GetComponentInParent<EntityStats>();
        for (int i = 0; i < textAmount; i++)
        {
            GameObject t = Instantiate(floatingText, transform.position, transform.rotation);
            t.transform.SetParent(transform);
            t.SetActive(false);
            floatingTexts[i] = t;
        }
    }

    void CreateFloatingText(float damage, Color color)
    {
        int count = textAmount;
        while (floatingTexts[currIndex].activeSelf && count > 0)
        {
            currIndex = (currIndex + 1) % textAmount;
            count--;
        }

        GameObject f_text = floatingTexts[currIndex];
//        Debug.Log(Mathf.Floor(damage).ToString());
        f_text.GetComponent<FloatingText>().InitializeText(Mathf.Floor(damage).ToString() , color);
    }

    private void OnEnable()
    {
        entityStats.OnHealthChanged += CreateFloatingText;
    }

    private void OnDisable()
    {
        entityStats.OnHealthChanged -= CreateFloatingText;
    }

}
