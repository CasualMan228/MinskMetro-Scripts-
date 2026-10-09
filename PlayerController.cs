using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public EscapeGroupHandler escapeGroupHandler;
    public SampleSceneScript sampleSceneScript;
    public NavMeshAgent agent;
    //public GameObject player;
    public Transform cam;
    public float playerSpeed = 5f;
    public float sensitivity = 200f;
    private float xRotation = 0f; //контроль вертикального угла

    public bool isInTrain = false;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        if (sampleSceneScript.isAbleToMove)
        {
            if (!escapeGroupHandler.isEscapeMenuOpened)
                RotateCam();

            if (isInTrain)
            {
                //синхронизация Agent с фактическим положением игрока после движения поезда
                agent.nextPosition = transform.position;

                MovePlayer();

                //теперь переносим игрока туда, куда его переместил NavMeshAgent
                transform.position = agent.nextPosition;
            }
            else
            {
                MovePlayer();
            }
        }
    }
    private void MovePlayer()
    {
        Vector3 direction = Vector3.zero;

        //сбор ввода по WASD относительно направления камеры
        if (Input.GetKey(KeyCode.W)) direction += cam.forward;
        if (Input.GetKey(KeyCode.S)) direction -= cam.forward;
        if (Input.GetKey(KeyCode.A)) direction -= cam.right;
        if (Input.GetKey(KeyCode.D)) direction += cam.right;

        //ОБНУЛЯЕМ Y ДО НОРМАЛИЗАЦИИ: исключаем влияние наклона камеры на скорость
        direction.y = 0;

        //НОРМАЛИЗУЕМ ОДИН РАЗ В КОНЦЕ: выравниваем скорость по диагоналям до честной единицы
        direction = direction.normalized;

        //двигаем капсулу по NavMesh сетке
        agent.Move(direction * playerSpeed * Time.deltaTime);
    }
    public void EnterTrain(Transform train)
    {
        isInTrain = true;
        transform.SetParent(train, true);

        //Agent больше не должен самостоятельно менять Transform игрока!!!
        agent.updatePosition = false;
        agent.updateRotation = false;

        //синхронизируем Agent с игроком
        agent.nextPosition = transform.position;
    }

    public void ExitTrain()
    {
        Vector3 exitPosition = transform.position;
        transform.SetParent(null, true);
        isInTrain = false;

        agent.updatePosition = true;
        agent.updateRotation = true;

        agent.Warp(exitPosition);
    }
    private void RotateCam()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        //вращаем саму капсулу влево-вправо
        transform.Rotate(Vector3.up * mouseX);

        //расчет и ограничение угла вверх-вниз для камеры
        xRotation = xRotation - mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        //вращаем камеру
        cam.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
