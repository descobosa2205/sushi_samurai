using UnityEngine;
using UnityEngine.UIElements;

public class Factory : MonoBehaviour
{
    [SerializeField] private GameObject[] sushiPrefabs;

    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private float bombProb = 0.1f;

    [SerializeField] private GameObject specialPrefab;
    [SerializeField] private float specialProb = 0.1f;

    private GameObject prefab;

    public GameObject CreateThrowable(Vector3 position, Quaternion rotation)
    {
        float randomNum = Random.Range(0f, 1f);
        if (randomNum < bombProb)
        {
            prefab = bombPrefab;
        }
        else if (randomNum < bombProb + specialProb)
        {
            prefab = specialPrefab;
        }
        else
        {
            prefab = sushiPrefabs[Random.Range(0, sushiPrefabs.Length)];
        }

        return Instantiate(prefab, position, rotation);
    }

}
