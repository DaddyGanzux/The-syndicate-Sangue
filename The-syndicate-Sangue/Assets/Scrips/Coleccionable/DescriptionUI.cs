using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DescriptionUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI header;
    [SerializeField] TextMeshProUGUI content;
    public int characterWrapLimit;

    [SerializeField] LayoutElement LayoutElement;
    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void Show(ItemUI item)
    {
        header.text = item.itemData.name;
        content.text = item.itemData.description;

        int headerLength = header.text.Length;
        int contentLength = content.text.Length;

        LayoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit);
    }

    private void Update()
    {
        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        float finalPivotX;
        float finalPivotY;

        if (pivotX < 0.5)
            finalPivotX = -0.1f;
        else
            finalPivotX = 1.01f;

        if (pivotY < 0.5)
            finalPivotY = 0;
        else
            finalPivotY = 1;

        rect.pivot = new Vector2(finalPivotX, finalPivotY);

        transform.position = position;
    }

}
