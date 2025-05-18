using System;

using TMPro;

using UnityEngine;

using Random = UnityEngine.Random;



public class FloatingText : MonoBehaviour

{

    private RectTransform rectTransform;

    private TextMeshProUGUI text;

    private Vector3 offSet;

    [SerializeField] private float alphaSpeed;

    [SerializeField] private Vector3 movementSpeed;

    private bool initialized = false;



    private void Awake()

    {

        rectTransform = GetComponent<RectTransform>();

        text = GetComponent<TextMeshProUGUI>();

        offSet = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.1f, 0f), 0);

        text.color = Color.red;

        transform.position += offSet;

    }

    void Update()

    {

        if (!initialized)

        {

            return;

        }



        Color c = text.color;

        c.a -= alphaSpeed * Time.deltaTime;

        text.color = c;

//text.color = Color.Lerp(text.color , Color.blue, alphaSpeed*Time.deltaTime);

        if (c.a <= 0f)

        {

            ResetText();

        }




    }

    public void InitializeText(String _text,Color color)

    {


        gameObject.SetActive(true);

        text.text = _text;

        text.color = color;

        offSet = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0.0f, 0.25f), 0);

        rectTransform.anchoredPosition += (Vector2)offSet;

        initialized = true;



    }

    void ResetText()

    {

        gameObject.SetActive(false);

        text.color = Color.red;

        rectTransform.anchoredPosition -= (Vector2)offSet;

        text.alpha = 1;

        initialized = false;

    }

}