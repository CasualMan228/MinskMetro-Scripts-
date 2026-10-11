using System.Collections;
using UnityEngine;

public class NavHandler : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject navStation;
    public GameObject navStation_train2way;
    public GameObject navStation_train1way;
    public GameObject navTrain;

    private void Awake()
    {
        Debug.Log("Сетка -> чисто станция");
        navStation.SetActive(true);
        navStation_train1way.SetActive(false);
        navStation_train2way.SetActive(false);
        navTrain.SetActive(false);
    }
    public void Start1Way()
    {
        Debug.Log("Сетка будет -> станция + 1 путь");
        StartCoroutine(ChangeNavMesh(navStation_train1way));
    }
    public void Start2Way()
    {
        Debug.Log("Сетка будет -> станция + 2 путь");
        StartCoroutine(ChangeNavMesh(navStation_train2way));
    }
    IEnumerator ChangeNavMesh(GameObject navmesh)
    {
        yield return new WaitForSecondsRealtime(102.5f);
        navmesh.SetActive(true);
        navStation.SetActive(false);

        yield return new WaitForSecondsRealtime(21.5f);
        SetNavStationOrTrain();
    }
    void SetNavStationOrTrain()
    {
        if (playerController.isInTrain)
        {
            navTrain.SetActive(true);
        }
        else
        {
            navStation.SetActive(true);
        }
        navStation_train1way.SetActive(false);
        navStation_train2way.SetActive(false);
    }
}