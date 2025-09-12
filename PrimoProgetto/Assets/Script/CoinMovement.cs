using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    [SerializeField] Vector3 coinRotation; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().Rotate(coinRotation);
    }
}
