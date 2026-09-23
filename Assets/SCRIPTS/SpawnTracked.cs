using UnityEngine;
using Vuforia;

public class SpawnTracked : MonoBehaviour
{
    [SerializeField] ObserverBehaviour target;
    [SerializeField] GameObject[] prefabPokemon;
    [SerializeField] Transform anchor;

    private int random;

    GameObject instance;

    void Awake()
    {
        target.OnTargetStatusChanged += OnStatusChanged;
    }

    void OnDestroy()
    {
        target.OnTargetStatusChanged -= OnStatusChanged;
    }

    void OnStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        bool tracked =
            status.Status == Status.TRACKED ||
            status.Status == Status.EXTENDED_TRACKED;

        random = Random.Range(0, prefabPokemon.Length);

        if (tracked && instance == null)
            instance = Instantiate(prefabPokemon[random], anchor.position, anchor.rotation, anchor);

        if (!tracked && instance != null)
            Destroy(instance);
    }
}
