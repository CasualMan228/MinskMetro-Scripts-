using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMENUHandler : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Start()
    {
        StartCoroutine(LaunchNextScene());
    }
    IEnumerator LaunchNextScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync("SampleScene");
    }
}
