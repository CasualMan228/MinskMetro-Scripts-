using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class EscapeGroupHandler : MonoBehaviour
{
    public PlayerController playerController;
    public SampleSceneRestart sampleSceneRestart;
    public SampleSceneScript sampleSceneScript;
    public CanvasGroup canvas_group_escapeGroup;
    public bool isEscapeMenuOpened = false;
    Tween tween;

    public Slider sens_slider;
    public Slider overall_sound_slider;
    public Slider fov_slider;

    private void Update()
    {
        if (tween != null)
            return;

        if (Input.GetKeyDown(KeyCode.Escape) && sampleSceneScript.isAbleToShowEscapeMenu)
        {
            if (!isEscapeMenuOpened)
            {
                //Debug.Log("Открываю меню");
                OpenMenu();
            }
            else
            {
                //Debug.Log("Закрываю меню");
                CloseMenu();
            }
        }
    }
    void OpenMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        canvas_group_escapeGroup.interactable = true;
        canvas_group_escapeGroup.blocksRaycasts = true;

        tween = canvas_group_escapeGroup.DOFade(1f, 0.5f).OnComplete(() =>
        {
            tween = null;
            //Debug.Log("Меню открыл");
        });
        isEscapeMenuOpened = true;
    }
    void CloseMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canvas_group_escapeGroup.interactable = false;
        canvas_group_escapeGroup.blocksRaycasts = false;
        tween = canvas_group_escapeGroup.DOFade(0f, 0.5f).OnComplete(() =>
        {
            tween = null;
            //Debug.Log("Меню закрыл");
        });
        isEscapeMenuOpened = false;
    }
    public void CloseMenuEmergency()
    {
        sampleSceneScript.isAbleToShowEscapeMenu = false;
        if (tween != null)
        {
            tween.Kill();
            tween = null;
        }
        CloseMenu();
    }
    public void ContinueButtonClicked()
    {
        //Debug.Log("Закрываю меню по кнопке \"Продолжить\"");
        CloseMenu();
    }
    public void ToMenuButtonClicked()
    {
        CloseMenuEmergency();
        StartCoroutine(SetBlackoutAndLoadSceneOrLeaveCoroutine("LoadingAfterLogo"));
    }
    public void ExitButtonClicked()
    {
        CloseMenuEmergency();
        StartCoroutine(SetBlackoutAndLoadSceneOrLeaveCoroutine(true));
    }
    IEnumerator SetBlackoutAndLoadSceneOrLeaveCoroutine(string scene_name)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        sampleSceneRestart.animator_sample_scene.SetTrigger("SetBlackout");
        yield return new WaitForSecondsRealtime(3.5f);
        SceneManager.LoadScene(scene_name);
    }
    IEnumerator SetBlackoutAndLoadSceneOrLeaveCoroutine(bool areWeLeaving)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        sampleSceneRestart.animator_sample_scene.SetTrigger("SetBlackout");
        yield return new WaitForSecondsRealtime(3.5f);
        Application.Quit();
        Debug.Log("МЫ ВЫШЛИ ИЗ ИГРЫ");
    }

    public void SensChanged()
    {
        playerController.sensitivity = sens_slider.value;
    }
    public void OverallSoundChanged()
    {
        AudioListener.volume = overall_sound_slider.value;
    }
    public void FovChanged()
    {
        playerController.cam.GetComponent<Camera>().fieldOfView = fov_slider.value;
    }
}