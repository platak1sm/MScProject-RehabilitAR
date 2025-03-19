using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StartSceneController : MonoBehaviour
{
    public Image fadePanel;
    public float fadeDuration = 1f;
    private bool isFading = false;

    void Start()
    {
        if (!fadePanel) Debug.LogError("Fade Panel missing!");
        fadePanel.color = new Color(0, 0, 0, 0); // Force transparent
        Debug.Log("FadePanel initialized, alpha: " + fadePanel.color.a);
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch) && !isFading)
        {
            Debug.Log("A pressed—starting fade");
            StartCoroutine(FadeAndLoad());
        }
    }

    IEnumerator FadeAndLoad()
    {
        isFading = true;
        float elapsedTime = 0f;
        Color startColor = fadePanel.color;
        Color endColor = new Color(0, 0, 0, 1);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadePanel.color = new Color(0, 0, 0, alpha);
            Debug.Log("Fading, alpha: " + alpha);
            yield return null;
        }

        fadePanel.color = endColor; // Ensure fully black
        Debug.Log("Fade complete, loading ExerciseScene");
        SceneManager.LoadScene("RehabilitAR");
    }
}