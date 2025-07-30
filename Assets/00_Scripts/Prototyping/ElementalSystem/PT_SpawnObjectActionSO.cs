using UnityEngine;

[CreateAssetMenu(fileName = "SpawnObjectAction", menuName = "Prototype/Elemental System/Actions/Spawn Object", order = 0)]
public class PT_SpawnObjectActionSO : PT_InteractionActionSO
{
    [Tooltip("생성할 오브젝트의 프리팹")]
    public GameObject prefabToSpawn;

    public override void Execute(Vector3 position, GameObject self, GameObject other)
    {
        if (prefabToSpawn != null)
        {
            Debug.Log($"Spawning '{prefabToSpawn.name}' at {position}");
            Instantiate(prefabToSpawn, position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Prefab to spawn is not set in the action asset.");
        }
    }
}
