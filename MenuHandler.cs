using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public Animator animator;
    bool isClicked = false;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void ImageClicked(string num)
    {
        if (!isClicked)
        {
            isClicked = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (!PlayerPrefs.HasKey("Quality"))
            {
                PlayerPrefs.SetInt("Quality", 0);
            }
            switch (num)
            {
                case "first":
                    {
                        PlayerPrefs.SetInt("Quality", 1);
                        Debug.Log("Графика -> 1");
                        break;
                    }
                case "second":
                    {
                        PlayerPrefs.SetInt("Quality", 2);
                        Debug.Log("Графика -> 2");
                        break;
                    }
                case "third":
                    {
                        PlayerPrefs.SetInt("Quality", 3);
                        Debug.Log("Графика -> 3");
                        break;
                    }
                default:
                    {
                        Debug.Log("Некорректный аргумент!");
                        break;
                    }
            }
            PlayerPrefs.Save();
            StartCoroutine(LaunchMenuCoroutine());
        }
    }
    IEnumerator LaunchMenuCoroutine()
    {
        animator.SetTrigger("ToPCTrigger");
        yield return new WaitForSeconds(9f);
        SceneManager.LoadScene("LoadingAfterMenu");
    }
}
