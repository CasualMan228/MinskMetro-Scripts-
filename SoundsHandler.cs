using UnityEngine;

public class SoundsHandler : MonoBehaviour
{
    public GameObject[] outside_sounds;
    public GameObject[] inside_sounds;
    public void SetOutsidePriority()
    {
        foreach (var current_sound in outside_sounds)
        {
            current_sound.GetComponent<AudioLowPassFilter>().enabled = false;
        }
        foreach (var current_sound in inside_sounds)
        {
            current_sound.GetComponent <AudioLowPassFilter>().enabled = true;
        }
    }
    public void SetInsidePriority()
    {
        foreach (var current_sound in outside_sounds)
        {
            current_sound.GetComponent<AudioLowPassFilter>().enabled = true;
        }
        foreach (var current_sound in inside_sounds)
        {
            current_sound.GetComponent<AudioLowPassFilter>().enabled = false;
        }
    }
}