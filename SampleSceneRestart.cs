using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SampleSceneRestart : MonoBehaviour
{
    public PlayerController playerController;
    public Animator animator_sample_scene;
    public void LaunchSceneRestart()
    {
        if (playerController.isInTrain)
        {
            StartCoroutine(LaunchSceneRestartCoroutine());
        }
    }
    IEnumerator LaunchSceneRestartCoroutine()
    {
        animator_sample_scene.SetTrigger("SetBlackout");
        yield return new WaitForSecondsRealtime(3.5f);
        SceneManager.LoadScene("LoadingAfterMenu");
    }
}
