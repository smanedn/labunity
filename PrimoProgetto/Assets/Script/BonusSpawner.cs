using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    public GameObject bonusPrefab;
    [SerializeField] int numberOfBonuses;

    public GameObject pavimento;
    public GameObject MuroEsterno;
    public GameObject MuroInterno;

    void Start()
    {
        SpawnBonuses();
    }

    void SpawnBonuses()
    {
        Collider pavimentoCollider = pavimento.GetComponent<Collider>();
        Collider muroEsternoCollider = MuroEsterno.GetComponent<Collider>();
        Collider muroInternoCollider = MuroInterno.GetComponent<Collider>();
        Bounds pavimentoBounds = pavimentoCollider.bounds;

        for (int i = 0; i < numberOfBonuses; i++)
        {
            Vector3 spawnPosition;
            int attempts = 0;
            const int maxAttempts = 100;

            do
            {
                float x = Random.Range(pavimentoBounds.min.x, pavimentoBounds.max.x);
                float z = Random.Range(pavimentoBounds.min.z, pavimentoBounds.max.z);
                float y = pavimentoBounds.max.y + 0.5f;
                spawnPosition = new Vector3(x, y, z);

                attempts++;
                if (attempts > maxAttempts)
                {
                    break;
                }

            } while (IsInsideCollider(spawnPosition, muroEsternoCollider) || IsInsideCollider(spawnPosition, muroInternoCollider));

            if (attempts <= maxAttempts)
            {
                Instantiate(bonusPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
    bool IsInsideCollider(Vector3 point, Collider collider)
    {

        return Physics.CheckSphere(point, 0.1f, 1 << collider.gameObject.layer);
    }
}
