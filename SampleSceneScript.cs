using System.Collections;
using UnityEngine;

public class SampleSceneScript : MonoBehaviour
{
    public GameObject camBegin;
    public GameObject camPlayer;

    public bool isAbleToMove = false;
    public bool isAbleToShowEscapeMenu = false;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        camPlayer.SetActive(false);
        camBegin.SetActive(true);
    }
    private void Start()
    {
        StartCoroutine(LaunchSceneCorrectlyCoroutine());
    }
    IEnumerator LaunchSceneCorrectlyCoroutine()
    {
        isAbleToMove = false;
        isAbleToShowEscapeMenu = false;
        yield return new WaitForSecondsRealtime(7f);
        camBegin.GetComponent<Animator>().SetTrigger("LaunchAnimation");
        yield return new WaitForSecondsRealtime(5f);
        camPlayer.SetActive(true);
        camBegin.SetActive(false);
        isAbleToMove = true;
        isAbleToShowEscapeMenu = true;
    }
}
