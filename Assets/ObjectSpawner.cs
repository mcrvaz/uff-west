using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {
    [Tooltip("Object to be instantiated.")]
    public GameObject prefab;
    [Tooltip("Minimum time in seconds before instantiating.")]
    public float minTime;
    [Tooltip("Maximum time in seconds before instantiating.")]
    public float maxTime;

    private Collider2D spawnArea;
    private Renderer prefabRenderer;
    private float currentTime, nextTime;

    void Awake() {
        prefabRenderer = prefab.GetComponentInChildren<Renderer>();
        spawnArea = GetComponent<Collider2D>();
    }

    void Start() {
        Invoke("InstantiateRepeating", GetRandomTime());
    }

    private float GetRandomTime() {
        return Random.Range(minTime, maxTime);
    }

    private Vector2 GetRandomPosition() {
        float randomX = Random.Range(
            -spawnArea.bounds.size.x + prefabRenderer.bounds.size.x,
            spawnArea.bounds.size.x - prefabRenderer.bounds.size.x
        );
        float randomY = Random.Range(
            -spawnArea.bounds.size.y + prefabRenderer.bounds.size.y,
            spawnArea.bounds.size.y - prefabRenderer.bounds.size.y
        );
        return new Vector2(randomX, randomY);
    }

    private GameObject InstantiatePrefab() {
        return (GameObject) Instantiate(prefab, GetRandomPosition(), Quaternion.identity);
    }

    private void InstantiateRepeating() {
        InstantiatePrefab();
        Invoke("InstantiateRepeating", GetRandomTime());
    }

}
