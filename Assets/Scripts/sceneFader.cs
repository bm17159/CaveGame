using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class sceneFader : MonoBehaviour
{
    // Reference to the Image component used for fading
    public Image fadeImage;
    // Duration of the fade effect
    public float fadeDuration = 1f;

    // Called when the script instance is being loaded
    private void Start()
    {
        // Ensure the fade image is active
        fadeImage.gameObject.SetActive(true);
        // Start the fade-out effect
        StartCoroutine(FadeOut());
    }

    // Method to load a new scene with a fade-in effect
    public void LoadScene(string sceneName)
    {
        // Start the fade-in effect and load the new scene
        StartCoroutine(FadeIn(sceneName));
    }

    // Coroutine to handle the fade-out effect
    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        // Gradually increase the alpha value of the image color to create a fade-out effect
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }

    // Coroutine to handle the fade-in effect and load the new scene
    private IEnumerator FadeIn(string sceneName)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        // Gradually decrease the alpha value of the image color to create a fade-in effect
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = 1f - Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // Load the new scene after the fade-in effect is complete
        SceneManager.LoadScene(sceneName);
    }
}
