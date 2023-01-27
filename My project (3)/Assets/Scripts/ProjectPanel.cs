using UnityEngine;
using System.Collections;

public class ProjectPanel : MonoBehaviour
{
    public GameObject scrollView; // Reference to the scroll view game object
    public GameObject contentPanel; // Reference to the panel that contains all the list items
    public float padding = 10f; // Padding between the items in the scroll view

    void Start()
    {
        // Get the dimensions of all the items in the list
        float contentHeight = GetContentHeight();
        float contentWidth = GetContentWidth();

        // Calculate the total padding for the width and height
        int numItems = contentPanel.transform.childCount;
        float totalPaddingX = padding * (numItems - 1);
        float totalPaddingY = padding * (numItems - 1);

        // Set the size of the scroll view and the content panel to the size of all the items, plus padding
        scrollView.GetComponent<RectTransform>().sizeDelta = new Vector2(contentWidth + totalPaddingX, contentHeight + totalPaddingY);
        contentPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(contentWidth + totalPaddingX, contentHeight + totalPaddingY);
    }

    float GetContentHeight()
    {
        // Get the height of all the items in the list
        float height = 0f;
        foreach (Transform child in contentPanel.transform)
        {
            height += child.GetComponent<RectTransform>().sizeDelta.y;
        }
        return height;
    }

    float GetContentWidth()
    {
        // Get the width of all the items in the list
        float width = 0f;
        foreach (Transform child in contentPanel.transform)
        {
            width += child.GetComponent<RectTransform>().sizeDelta.x;
        }
        return width;
    }
}