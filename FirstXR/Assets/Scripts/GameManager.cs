using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CollectibleDetector : MonoBehaviour
{
    [SerializeField]
    private string collectibleTag = "moneta";

    [SerializeField]
    private float holdTimeToCollect = 2f;

    [SerializeField]
    private Collider detectionTrigger;

    [SerializeField]
    private int score = 0;

    private bool timerRunning = false;
    private float timer = 0f;
    private GameObject currentCollectedObject = null;
    private XRBaseInteractor currentInteractor = null;

    private void ResetTimer()
    {
        timerRunning = false;
        timer = 0f;
        currentCollectedObject = null;
        currentInteractor = null;
    }

    private void Update()
    {
        if (timerRunning && currentCollectedObject != null)
        {
            timer += Time.deltaTime;

            if (timer >= holdTimeToCollect)
            {
                CollectCurrentObject();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        XRBaseInteractor interactor = other.GetComponentInParent<XRBaseInteractor>();

        if (interactor != null)
        {
            if (interactor.firstInteractableSelected != null)
            {
                GameObject grabbedObject = interactor.firstInteractableSelected.transform.gameObject;

                if (grabbedObject.CompareTag(collectibleTag))
                {
                    if (currentCollectedObject != grabbedObject)
                    {
                        currentCollectedObject = grabbedObject;
                        currentInteractor = interactor;
                        timer = 0f;
                        timerRunning = true;
                    }
                    return;
                }
            }
        }
        ResetTimer();
    }

    private void CollectCurrentObject()
    {
        if (currentCollectedObject != null)
        {
            score++;

            currentCollectedObject.SetActive(false);

            ResetTimer();
        }
    }
}
