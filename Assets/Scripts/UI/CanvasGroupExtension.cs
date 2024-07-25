using UnityEngine;

public static class CanvasGroupExtension
{
    public static void SetActive(this CanvasGroup canvasGroup, bool isActive)
    {
        if (isActive)
        canvasGroup.alpha = 1;

        else
        canvasGroup.alpha = 0;

        canvasGroup.interactable = isActive;
        canvasGroup.blocksRaycasts = isActive;
    }
}
