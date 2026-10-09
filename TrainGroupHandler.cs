using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TrainGroupHandler : MonoBehaviour
{
    public Animator trainGroup_animator;
    public Text direction;
    public Text timer;
    public void LaunchTrainGroupAnimation(int way)
    {
        StartCoroutine(LaunchTrainGroupAnimationCoroutine(way));
    }
    IEnumerator LaunchTrainGroupAnimationCoroutine(int way)
    {
        yield return new WaitForSecondsRealtime(39f);
        trainGroup_animator.SetTrigger("LaunchGUITrain");
        if (way == 1)
        {
            direction.text = "Немаршанскі Сад << АЭРАДРОМНАЯ << Кавальская Слабада";
        }
        else
        {
            direction.text = "Кавальская Слабада << АЭРАДРОМНАЯ << Немаршанскі Сад";
        }
        for (int seconds = 35; seconds >= 27; seconds--)
        {
            timer.text = $"00:{seconds}";
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}
