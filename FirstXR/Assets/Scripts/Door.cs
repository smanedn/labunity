using UnityEngine;

public class Door : MonoBehaviour
{
    public float slideDistance = 3f;
    public float slideSpeed = 1f;

    private bool shouldSlide = false;
    private Vector3 targetPosition;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + transform.right * slideDistance;
    }

    void Update()
    {
        if (shouldSlide)
        {

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, slideSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                shouldSlide = false;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("chiave"))
        {
            shouldSlide = true;
        }
    }
}
