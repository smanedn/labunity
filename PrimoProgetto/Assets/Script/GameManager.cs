using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject enemyPrefab;

    [SerializeField] Vector3 initPosEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(enemyPrefab, initPosEnemy, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
