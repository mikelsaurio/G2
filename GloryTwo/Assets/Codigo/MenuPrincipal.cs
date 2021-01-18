using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class MenuPrincipal : MonoBehaviour
{
    //public guardarValores gv;
    public TextMeshProUGUI pills;

    public void PlayButton()
    {
        Debug.Log("Start");

        StartCoroutine(StartFade(FindObjectOfType<AudioSource>(), 0.8f, 0));

        Invoke("CambioEscena", 1f);

    }

    public void SeePills()
    {
        pills.text = PlayerPrefs.GetInt("puntuacionPasillas").ToString();
    }

    public void CambioEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OptionsButton()
    {
        Debug.Log("Options");
    }

    public void CreditsButton()
    {
        Debug.Log("Credits");
    }
    public void QuitButton()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
        {
            float currentTime = 0;
            float start = audioSource.volume;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }
            yield break;
        }
}
