using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeViewButton : MonoBehaviour
{
    [SerializeField] private CanvasGroup currPanel;
    [SerializeField] private CanvasGroup targetPanel;
    [SerializeField] private Vector2 cameraTarget;
    [SerializeField] private float cameraZoom = 5f;
    [SerializeField] private float delayTime = 0.2f;
    [SerializeField] private float fadeTime = 0.2f;

    public void ChangeView()
    {
        EnablePanel(currPanel, false);
        EnablePanel(targetPanel, true);
        CameraController.SetTarget(cameraTarget, cameraZoom);
    }

    private void EnablePanel(CanvasGroup panel, bool enabled)
    {
        if (enabled)
        {
            StartCoroutine(PanelFade(panel, enabled));
        }
        else
        {
            panel.alpha = enabled ? 1 : 0;
            panel.blocksRaycasts = enabled;
            panel.interactable = enabled;
        }
    }

    private IEnumerator PanelFade(CanvasGroup panel, bool enabled)
    {
        yield return new WaitForSeconds(delayTime);

        panel.blocksRaycasts = enabled;
        panel.interactable = enabled;

        float currTime = 0;
        float startAlpha = panel.alpha;
        float targetAlpha = enabled ? 1 : 0;
        while (currTime <= fadeTime)
        {
            panel.alpha = Mathf.Lerp(startAlpha, targetAlpha, currTime / fadeTime);
            currTime += Time.deltaTime;
            yield return null;
        }
    }
}
