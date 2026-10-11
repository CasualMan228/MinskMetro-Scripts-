using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Video;
public class TrainHandler : MonoBehaviour
{
    public PlayerController playerController;
    public EscapeGroupHandler escapeGroupHandler;
    public SampleSceneRestart sampleSceneRestart;
    public TrainGroupHandler trainGroupHandler;
    public MusicHandler musicHandler;
    public StationHandler stationHandler;
    public NavHandler navHandler;
    public GameObject ar_out;
    public GameObject dep_out;
    public GameObject dep_in;
    public GameObject door_close;
    public GameObject next_station_kov_sl;
    public GameObject next_station_nem_sa;
    public GameObject indicator_alarm_train;

    public Animator[] door_animators;
    Animator animator_train = null;

    public Material gabarit_yellow;
    public Material gabarit_white;

    public MeshRenderer[] train_indicators;
    public Material indicator_white;
    public Material indicator_red;
    public Material indicator_red_blink;
    public Material indicator_green;

    public VideoPlayer tablo_videoplayer;
    public VideoClip tablo_next;
    public VideoClip tablo_default;

    public DecalProjector route_front;
    public DecalProjector route_back;
    public DecalProjector routeNum_front;
    public DecalProjector routeNum_back;
    public Material matUBIPLO;
    public Material matSLYHAS;
    public Material[] nums;
    int counterNums = 0;
    private void Awake()
    {
        StartCoroutine(FixGabaritCoroutine());
    }
    private void Start()
    {
        StartCoroutine(SpawnTrainCoroutine());
    }
    
    IEnumerator FixGabaritCoroutine()
    {
        gabarit_white.EnableKeyword("_EMISSION");
        gabarit_yellow.EnableKeyword("_EMISSION");
        yield return new WaitForSecondsRealtime(0.5f);
        gabarit_white.DisableKeyword("_EMISSION");
        gabarit_yellow.DisableKeyword("_EMISSION");
    }
    IEnumerator SpawnTrainCoroutine()
    {
        while (true)
        {
            /*yield return StartCoroutine(SpawnTrainEventsCoroutine(2));
            yield return StartCoroutine(SpawnTrainEventsCoroutine(1));*/
            StartCoroutine(SpawnTrainEventsCoroutine(2));
            SetRouteAndNum();
            yield return new WaitForSecondsRealtime(280f);
            StartCoroutine(SpawnTrainEventsCoroutine(1));
            SetRouteAndNum();
            yield return new WaitForSecondsRealtime(280f);
        }
    }
    void SetRouteAndNum()
    {
        if (counterNums % 2 == 0)
        {
            route_front.material = matUBIPLO;
            route_back.material = matSLYHAS;
        }
        else
        {
            route_front.material = matSLYHAS;
            route_back.material = matUBIPLO;
        }
        routeNum_front.material = nums[counterNums];
        routeNum_back.material = nums[counterNums];
        counterNums++;
        if (counterNums >= nums.Length)
            counterNums = 0;
    }
    IEnumerator SpawnTrainEventsCoroutine(int way)
    {
        if (way == 1)
        {
            stationHandler.LaunchWay(1);
            navHandler.Start1Way();
            trainGroupHandler.LaunchTrainGroupAnimation(1);
        }
        else
        {
            stationHandler.LaunchWay(2);
            navHandler.Start2Way();
            trainGroupHandler.LaunchTrainGroupAnimation(2);
        }
        musicHandler.LaunchMusicHandler();
        animator_train = GameObject.Find("train").GetComponent<Animator>();
        animator_train.SetTrigger("trigger_arrive_train");

        yield return new WaitForSecondsRealtime(71.15f);
        PlayArrivingOutSound();

        yield return new WaitForSecondsRealtime(25f);
        AllIndicatorsRed();

        yield return new WaitForSecondsRealtime(1f);
        EnableEmissionGabaritYellow();

        yield return new WaitForSecondsRealtime(1.85f);
        RedBlinkCorrectIndicators();

        yield return new WaitForSecondsRealtime(1f);
        OpenTrainDoors();
        EnableEmissionGabaritWhite();

        yield return new WaitForSecondsRealtime(3f);
        GreenCorrectIndicators();

        yield return new WaitForSecondsRealtime(7f);
        if (way == 2)
        {
            PlayNextStation_kov_sl();
        }
        else
        {
            PlayNextStation_nem_sa();
        }

        yield return new WaitForSecondsRealtime(12.45f);
        SetVideoTabloNext();

        yield return new WaitForSecondsRealtime(0.55f);
        PlayIndicatorTrainSound();
        RedBlinkCorrectIndicators();

        yield return new WaitForSecondsRealtime(0.4f);
        PlayIndicatorTrainSound();

        yield return new WaitForSecondsRealtime(0.4f);
        PlayIndicatorTrainSound();

        yield return new WaitForSecondsRealtime(0.1f);
        PlayCloseTrainDoorsSound();

        yield return new WaitForSecondsRealtime(0.1f);
        CloseTrainDoors();

        yield return new WaitForSecondsRealtime(0.2f);
        PlayIndicatorTrainSound();

        yield return new WaitForSecondsRealtime(0.4f);
        PlayIndicatorTrainSound();

        yield return new WaitForSecondsRealtime(0.4f);
        PlayIndicatorTrainSound();

        yield return new WaitForSecondsRealtime(0.4f);
        PlayIndicatorTrainSound();

        yield return new WaitForSecondsRealtime(0.4f);
        PlayIndicatorTrainSound();

        yield return new WaitForSecondsRealtime(0.4f);
        PlayIndicatorTrainSound();

        yield return new WaitForSecondsRealtime(0.3f);
        DisableEmissionGabaritWhite();

        yield return new WaitForSecondsRealtime(0.1f);
        PlayIndicatorTrainSound();

        yield return new WaitForSecondsRealtime(0.4f);
        AllIndicatorsRed();

        yield return new WaitForSecondsRealtime(3f);
        StartDeparture();

        yield return new WaitForSecondsRealtime(0.45f);
        SetVideoTabloDefault();

        yield return new WaitForSecondsRealtime(0.55f);
        DisableEmissionGabaritYellow();

        yield return new WaitForSecondsRealtime(1f);
        PlayDepInSound();

        yield return new WaitForSecondsRealtime(0.5f);
        RecoverIndicators();

        yield return new WaitForSecondsRealtime(10f);
        if (playerController.isInTrain)
        {
            escapeGroupHandler.CloseMenuEmergency();
        }

        yield return new WaitForSecondsRealtime(10f);
        sampleSceneRestart.LaunchSceneRestart();
    }

    void SetVideoTabloNext()
    {
        tablo_videoplayer.clip = tablo_next;
        tablo_videoplayer.Play();
    }
    void SetVideoTabloDefault()
    {
        tablo_videoplayer.clip = tablo_default;
        tablo_videoplayer.Play();
    }
    void RecoverIndicators()
    {
        foreach (var current_indicator in train_indicators)
        {
            if (!current_indicator.gameObject.name.Contains("train_indicator"))
            {
                indicator_red.DisableKeyword("_EMISSION");
                current_indicator.material = indicator_red;
            }
            else
            {
                indicator_white.EnableKeyword("_EMISSION");
                current_indicator.material = indicator_white;
            }
        }
    }
    void AllIndicatorsRed()
    {
        foreach (var indicator in train_indicators)
        {
            indicator.material = indicator_red;
            indicator_red.EnableKeyword("_EMISSION");
        }
    }
    void RedBlinkCorrectIndicators()
    {
        foreach (var current_indicator in train_indicators)
        {
            if (current_indicator.CompareTag("Correct_row"))
            {
                current_indicator.material = indicator_red_blink;
            }
        }
        for (int step = 0; step <= 19; step++)
        {
            float i = step * 0.2f;
            if (step % 2 != 0)
            {
                Invoke("EnableRedCorrectIndicator", i);
            }
            else
            {
                Invoke("DisableRedCorrectIndicator", i);
            }
        }
    }
    void EnableRedCorrectIndicator()
    {
        indicator_red_blink.EnableKeyword("_EMISSION");
    }
    void DisableRedCorrectIndicator()
    {
        indicator_red_blink.DisableKeyword("_EMISSION");
    }
    void GreenCorrectIndicators()
    {
        foreach (var current_indicator in train_indicators)
        {
            if (current_indicator.CompareTag("Correct_row"))
            {
                current_indicator.material = indicator_green;
                indicator_green.EnableKeyword("_EMISSION");
            }
        }
    }
    void EnableEmissionGabaritYellow()
    {
        gabarit_yellow.EnableKeyword("_EMISSION");
    }
    void DisableEmissionGabaritYellow()
    {
        gabarit_yellow.DisableKeyword("_EMISSION");
    }
    void EnableEmissionGabaritWhite()
    {
        gabarit_white.EnableKeyword("_EMISSION");
    }
    void DisableEmissionGabaritWhite()
    {
        gabarit_white.DisableKeyword("_EMISSION");
    }
    void PlayCloseTrainDoorsSound()
    {
        AudioSource door_close_sound = door_close.GetComponent<AudioSource>();
        door_close_sound.Play();
    }
    void StartDeparture()
    {
        animator_train.SetTrigger("trigger_departure_train");
        AudioSource dep_out_sound = dep_out.GetComponent<AudioSource>();
        dep_out_sound.Play();
    }
    void PlayDepInSound()
    {
        AudioSource dep_in_sound = dep_in.GetComponent<AudioSource>();
        dep_in_sound.Play();
    }
    void PlayIndicatorTrainSound()
    {
        AudioSource indicator_train_sound = indicator_alarm_train.GetComponent<AudioSource>();
        indicator_train_sound.Play();
    }
    void PlayArrivingOutSound()
    {
        AudioSource ar_out_sound = ar_out.GetComponent<AudioSource>();
        ar_out_sound.Play();
    }
    void PlayNextStation_kov_sl()
    {
        AudioSource kov_sl_sound = next_station_kov_sl.GetComponent<AudioSource>();
        kov_sl_sound.Play();
    }
    void PlayNextStation_nem_sa()
    {
        AudioSource nem_sa_sound = next_station_nem_sa.GetComponent<AudioSource>();
        nem_sa_sound.Play();
    }
    void OpenTrainDoors()
    {
        foreach (var current_animator in door_animators)
        {
            current_animator.SetTrigger("trigger_open_train");
        }
    }
    void CloseTrainDoors()
    {
        foreach (var current_animator in door_animators)
        {
            current_animator.SetTrigger("trigger_close_train");
        }
    }
}