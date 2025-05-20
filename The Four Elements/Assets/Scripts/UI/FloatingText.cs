using System.Collections;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private RectTransform rectTransform;
    private TextMeshProUGUI text;

    [SerializeField] private float alphaSpeed = 1f;
    [SerializeField] private Vector3 movementSpeed = new Vector3(0, 0f, 0);
    [SerializeField] private Vector2 offSetX = new Vector2(-0.2f, 0.2f);
    [SerializeField] private Vector2 offSetY = new Vector2(0.1f, 0.3f);
    [SerializeField] private Vector3 startingOffSet = Vector3.zero;
    private bool initialized = false;
    private float startingFontSize;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponent<TextMeshProUGUI>();
        startingFontSize = text.fontSize;
        text.fontWeight = FontWeight.Bold;
        rectTransform.localPosition = startingOffSet;
        // Optional: outline for visibility
        text.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, 0.2f);
        text.fontMaterial.SetColor(ShaderUtilities.ID_OutlineColor, Color.black);
    }

    void Update()
    {
        if (!initialized) return;

        // Fade out
        Color currentColor = text.color;
        currentColor.a -= alphaSpeed * Time.deltaTime;
        text.color = currentColor;
        text.fontSize += alphaSpeed * Time.deltaTime / 4;
        // Float upward
        rectTransform.localPosition += movementSpeed * Time.deltaTime;
        

        if (currentColor.a <= 0f)
        {
            ResetText();
        }
    }

    public void InitializeText(string _text, Color baseColor)
    {
        gameObject.SetActive(true);
        text.text = _text;
        text.color = baseColor;
        text.fontSize  = startingFontSize;
       rectTransform.localPosition = startingOffSet;
        Vector3 offset = new Vector3(
            Random.Range(offSetX.x, offSetX.y),
            Random.Range(offSetY.x, offSetY.y),
            0
        );
        rectTransform.localPosition += offset;

        initialized = true;
    }

    void ResetText()
    {
        text.fontSize  = startingFontSize;
        initialized = false;
        gameObject.SetActive(false);
        rectTransform.localPosition = startingOffSet;
    }
}