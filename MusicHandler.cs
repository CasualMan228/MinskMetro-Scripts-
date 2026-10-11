using System.Collections;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public PlayerController playerController;

    public AudioSource music;
    public AudioSource music2;

    public void LaunchMusicHandler()
    {
        StartCoroutine(StartMusicHandlerCoroutine());
    }
    IEnumerator StartMusicHandlerCoroutine()
    {
        yield return new WaitForSecondsRealtime(30f);
        Debug.Log("Музыка 2 го");
        StartCoroutine(FadeOut(music, 3f));
        StartCoroutine(FadeIn(music2, 3f));

        yield return new WaitForSecondsRealtime(115f);
        if (!playerController.isInTrain)
        {
            Debug.Log("Музыка 1 го");
            StartCoroutine(FadeOutAndStop(music2, 3f));
            StartCoroutine(FadeIn(music, 3f));
        }

        IEnumerator FadeIn(AudioSource source, float duration)
        {
            source.volume = 0f;
            source.Play();

            float currentTime = 0f;
            while (currentTime < duration)
            {
                currentTime += Time.unscaledDeltaTime;
                source.volume = Mathf.Lerp(0f, 1f, currentTime / duration);
                yield return null;
            }
            source.volume = 1f;
        }

        IEnumerator FadeOut(AudioSource source, float duration)
        {
            float startVolume = source.volume;
            float currentTime = 0f;

            while (currentTime < duration)
            {
                currentTime += Time.unscaledDeltaTime;
                source.volume = Mathf.Lerp(startVolume, 0f, currentTime / duration);
                yield return null; //кадр -> итерация
            }
            source.volume = 0f;
        }

        IEnumerator FadeOutAndStop(AudioSource source, float duration)
        {
            float startVolume = source.volume;
            float currentTime = 0f;

            while (currentTime < duration)
            {
                currentTime += Time.unscaledDeltaTime;
                source.volume = Mathf.Lerp(startVolume, 0f, currentTime / duration);
                yield return null;
            }
            source.volume = 0f;
            source.Stop();
        }
    }
}
