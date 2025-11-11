using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [SerializeField] private Image gameOverImage;
    [SerializeField] private float fadeDuration = 2f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if(gameOverImage != null)
        {
            Color c = gameOverImage.color;
            c.a = 0f;
            gameOverImage.color = c;
        }
    }

    public void ShowGameOver()
    {
        if (gameOverImage != null)
        {
            StartCoroutine(FadeInGameOver());
        }
    }

    private IEnumerator FadeInGameOver()
    {
        float elapsedTime = 0f;
        Color c = gameOverImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            gameOverImage.color = c;
            yield return null;
        }

        c.a = 1f;
        gameOverImage.color = c;
    }


}
