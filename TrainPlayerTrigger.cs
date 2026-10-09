    using UnityEngine;

    public class TrainPlayerTrigger : MonoBehaviour
    {
        public SoundsHandler soundsHandler;
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("В триггер вошел -> " + other.gameObject.name);

            PlayerController playerController =
                other.GetComponentInParent<PlayerController>();

            if (playerController != null)
            {
                Debug.Log("Именно player вошел в поезд");
                playerController.EnterTrain(transform.parent);
                soundsHandler.SetInsidePriority();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("Из триггера вышел ->: " + other.gameObject.name);

            PlayerController playerController =
                other.GetComponentInParent<PlayerController>();

            if (playerController != null)
            {
                Debug.Log("Именно player вышел из поезда");
                playerController.ExitTrain();
                soundsHandler.SetOutsidePriority();
            }
        }
    }