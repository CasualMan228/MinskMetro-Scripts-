using System.Collections;
using UnityEngine;

public class StationHandler : MonoBehaviour
{
    public TrainHandler trainHandler;
    public GameObject alarm_safe_door;
    public Animator[] station_door_animators;

    public GameObject[] Lights1Way;
    public GameObject[] Lights2Way;
    public Material light_on;
    public Material light_off;
    public Material light_yellow;

    bool isOpened1Way = false;
    bool isOpened2Way = false;
    public void LaunchWay(int way)
    {
        StartCoroutine(LaunchWayCoroutine(way));
    }
    IEnumerator LaunchWayCoroutine(int way)
    {
        yield return new WaitForSecondsRealtime(62.5f);
        StartCoroutine(PlayLightningCoroutine(way));

        yield return new WaitForSecondsRealtime(38.15f);
        StartCoroutine(PlayIndicatorDoorsCoroutine(way));
        PlayIndicatorSafeDoorSound();

        yield return new WaitForSecondsRealtime(0.35f);
        OpenStationDoors(way);
        PlayIndicatorSafeDoorSound();
        
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSecondsRealtime(0.35f);
            PlayIndicatorSafeDoorSound();
        }

        yield return new WaitForSecondsRealtime(22.05f);
        StartCoroutine(PlayIndicatorDoorsCoroutine(way));
        PlayIndicatorSafeDoorSound();

        yield return new WaitForSecondsRealtime(0.35f);
        CloseStationDoors(way);
        PlayIndicatorSafeDoorSound();

        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSecondsRealtime(0.35f);
            PlayIndicatorSafeDoorSound();
        }

        yield return new WaitForSecondsRealtime(0.5f);
        RestoreLights(way);

        yield return new WaitForSecondsRealtime(1f);
        RestoreLights(way);

        yield return new WaitForSecondsRealtime(1.5f);
        RestoreLights(way);
    }
    IEnumerator PlayLightningCoroutine(int way)
    {
        if (way == 1)
        {
            foreach (var current_indicator in Lights1Way)
            {
                current_indicator.GetComponent<MeshRenderer>().material = light_off;
            }
        }
        else
        {
            foreach (var current_indicator in Lights2Way)
            {
                current_indicator.GetComponent<MeshRenderer>().material = light_off;
            }
        }

        yield return new WaitForSecondsRealtime(0.5f);

        if (way == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int step = 0; step <= 38; step += 2)
                {
                    Lights1Way[step].GetComponent<MeshRenderer>().material = light_on;
                    Lights1Way[step + 1].GetComponent<MeshRenderer>().material = light_on;
                    yield return new WaitForSecondsRealtime(0.5f);
                }
                yield return new WaitForSecondsRealtime(0.5f);
                foreach (var current_indicator in Lights1Way)
                {
                    current_indicator.GetComponent<MeshRenderer>().material = light_off;
                }
                yield return new WaitForSecondsRealtime(0.5f);
            }
            foreach (var current_indicator in Lights1Way)
            {
                current_indicator.GetComponent<MeshRenderer>().material = light_on;
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                for (int step = 0; step <= 38; step += 2)
                {
                    Lights2Way[step].GetComponent<MeshRenderer>().material = light_on;
                    Lights2Way[step + 1].GetComponent<MeshRenderer>().material = light_on;
                    yield return new WaitForSecondsRealtime(0.5f);
                }
                yield return new WaitForSecondsRealtime(0.5f);
                foreach (var current_indicator in Lights2Way)
                {
                    current_indicator.GetComponent<MeshRenderer>().material = light_off;
                }
                yield return new WaitForSecondsRealtime(0.5f);
            }
            foreach (var current_indicator in Lights2Way)
            {
                current_indicator.GetComponent<MeshRenderer>().material = light_on;
            }
        }
    }
    void PlayIndicatorSafeDoorSound()
    {
        AudioSource indicator_safe_door_sound = alarm_safe_door.GetComponent<AudioSource>();
        indicator_safe_door_sound.Play();
    }
    void OpenStationDoors(int way)
    {
        if (way == 1)
        {
            foreach (var current_animator in station_door_animators)
            {
                if (current_animator.gameObject.transform.CompareTag("First_way"))
                {
                    current_animator.SetTrigger("trigger_open_station");
                }
            }
        }
        else
        {
            foreach (var current_animator in station_door_animators)
            {
                if (current_animator.gameObject.transform.CompareTag("Second_way"))
                {
                    current_animator.SetTrigger("trigger_open_station");
                }
            }
        }
    }
    void RestoreLights(int way)
    {
        if (way == 1)
        {
            foreach (var current_indicator in Lights1Way)
            {
                current_indicator.GetComponent<MeshRenderer>().material = light_on;
            }
        }
        else
        {
            foreach (var current_indicator in Lights2Way)
            {
                current_indicator.GetComponent<MeshRenderer>().material = light_on;
            }
        }
    }
    IEnumerator PlayIndicatorDoorsCoroutine(int way)
    {
        if (way == 1)
        {
            foreach (var current_indicator in Lights1Way)
            {
                current_indicator.GetComponent<MeshRenderer>().material = light_off;
            }
            isOpened1Way = !isOpened1Way;
        }
        else
        {
            foreach (var current_indicator in Lights2Way)
            {
                current_indicator.GetComponent<MeshRenderer>().material = light_off;
            }
            isOpened2Way = !isOpened2Way;
        }

        if (way == 1)
        {
            for (int step = 0; step <= 7; step++)
            {
                foreach (var current_indicator in Lights1Way)
                {
                    current_indicator.GetComponent<MeshRenderer>().material = light_yellow;
                }
                yield return new WaitForSecondsRealtime(0.175f);
                foreach (var current_indicator in Lights1Way)
                {
                    current_indicator.GetComponent<MeshRenderer>().material = light_off;
                }
                yield return new WaitForSecondsRealtime(0.175f);
            }
        }
        else
        {
            for (int step = 0; step <= 7; step++)
            {
                foreach (var current_indicator in Lights2Way)
                {
                    current_indicator.GetComponent<MeshRenderer>().material = light_yellow;
                }
                yield return new WaitForSecondsRealtime(0.175f);
                foreach (var current_indicator in Lights2Way)
                {
                    current_indicator.GetComponent<MeshRenderer>().material = light_off;
                }
                yield return new WaitForSecondsRealtime(0.175f);
            }
        }
        if (isOpened1Way)
        {
            foreach (var current_indicator in Lights1Way)
            {
                current_indicator.GetComponent<MeshRenderer>().material = light_yellow;
            }
        }
        if (isOpened2Way)
        {
            foreach (var current_indicator in Lights2Way)
            {
                current_indicator.GetComponent<MeshRenderer>().material = light_yellow;
            }
        }
    }
    void CloseStationDoors(int way)
    {
        if (way == 1)
        {
            foreach (var current_animator in station_door_animators)
            {
                if (current_animator.gameObject.transform.CompareTag("First_way"))
                {
                    current_animator.SetTrigger("trigger_close_station");
                }
            }
        }
        else
        {
            foreach (var current_animator in station_door_animators)
            {
                if (current_animator.gameObject.transform.CompareTag("Second_way"))
                {
                    current_animator.SetTrigger("trigger_close_station");
                }
            }
        }
    }
}