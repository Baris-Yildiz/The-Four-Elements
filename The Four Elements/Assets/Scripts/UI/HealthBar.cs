using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
     private EntityStats _entityStats;
    
    [SerializeField] private Image back;
    [SerializeField] private Image lerp;
    [SerializeField] private Image front;
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private float lerpSpeed = 1f;
    private bool startLerp = false;
    public float healthPercantage { get; set; } = 1f;


    private void Awake()
    {
        _entityStats = GetComponentInParent<EntityStats>();
        if (_entityStats == null)
        {
            _entityStats = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityStats>();
        }

        text.SetText(_entityStats.baseStats.MaxHealth + "/" + _entityStats.baseStats.MaxHealth );
        back.fillAmount = 1;
        lerp.fillAmount = 1;
        front.fillAmount = 1;
    }

    private void OnEnable()
    {
        _entityStats.OnHealthChanged += SetHealth;
    }
    private void OnDisable()
    {
        _entityStats.OnHealthChanged -= SetHealth;
    }

    private void Update()
    {
        Lerp();
    }

    private void SetHealth(float damage,Color color)
    {
        float maxHealth = _entityStats.baseStats.MaxHealth;
        healthPercantage = _entityStats.currentHealth / maxHealth;
        front.fillAmount = Mathf.Max(0 ,healthPercantage);
        text.SetText(Mathf.Max(0 , Mathf.Floor(_entityStats.currentHealth))+ "/" + maxHealth);
        startLerp = true;
    }

    private void Lerp()
    {
        if (startLerp)
        {
            lerp.fillAmount = Mathf.Lerp(lerp.fillAmount, front.fillAmount, lerpSpeed * Time.deltaTime);
            if (Mathf.Abs(lerp.fillAmount - front.fillAmount) < 0.005f)
            {
                lerp.fillAmount = front.fillAmount;
                startLerp = false;
            }
        }
        
    }
}
