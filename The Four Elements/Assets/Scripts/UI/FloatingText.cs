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

    [SerializeField] private Vector2 offSetX;
    [SerializeField] private Vector2 offSetY;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponent<TextMeshProUGUI>();

        offSet = new Vector3(Random.Range(offSetX.x, offSetX.y), Random.Range(offSetY.x, offSetY.y), 0);
        transform.position += offSet;

        // Default color
        text.color = Color.red;

        // Set font weight thicker
        text.fontWeight = FontWeight.Bold;

        // Optional: increase face dilation or outline for bolder effect
        text.enableVertexGradient = true;
        text.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 0.5f);  // Make text thicker
        text.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, 0.2f);
        text.fontMaterial.SetColor(ShaderUtilities.ID_OutlineColor, Color.black);
    }

    void Update()
    {
        if (!initialized) return;

        Color c = text.color;
        c.a -= alphaSpeed * Time.deltaTime;
        text.color = c;

        if (c.a <= 0f)
        {
            ResetText();
        }
    }

    public void InitializeText(string _text, Color baseColor)
    {
        gameObject.SetActive(true);
        text.text = _text;

        // Create a simple vertical gradient from the base color to a darker shade
        text.enableVertexGradient = true;

        VertexGradient gradient = new VertexGradient(
            baseColor,                                    // top left
            baseColor,                                    // top right
            baseColor * 0.6f,                             // bottom left
            baseColor * 0.6f                              // bottom right
        );
        text.colorGradient = gradient;

        // Make it bolder visually
        text.fontWeight = FontWeight.Bold;
        text.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 0.5f);
        text.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, 0.2f);
        text.fontMaterial.SetColor(ShaderUtilities.ID_OutlineColor, Color.black);

        offSet = new Vector3(Random.Range(offSetX.x, offSetX.y), Random.Range(offSetY.x, offSetY.y), 0);
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
