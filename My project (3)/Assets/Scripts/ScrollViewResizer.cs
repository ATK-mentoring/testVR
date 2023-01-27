using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScrollViewResizer : MonoBehaviour
{
    public ScrollRect scrollRect; // The ScrollRect component that will be resized
    public List<GameObject> items; // The list of items to be displayed in the ScrollRect
    public float itemPadding = 10f; // Padding between items in the ScrollRect

    void Start()
    {
        // Get the height of the first item in the list
        float itemHeight = items[0].GetComponent<RectTransform>().rect.height;

        // Calculate the total height of the ScrollRect based on the number of items, the item height, and the item padding
        float totalHeight = itemHeight * items.Count + itemPadding * (items.Count - 1);

        // Set the height of the ScrollRect and the viewport to the calculated total height
        scrollRect.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, totalHeight);
        scrollRect.viewport.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, totalHeight);
    }
}