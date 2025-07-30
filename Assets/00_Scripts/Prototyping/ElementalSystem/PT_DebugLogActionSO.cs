using UnityEngine;

[CreateAssetMenu(fileName = "DebugLogAction", menuName = "Prototype/Elemental System/Actions/Debug Log", order = 1)]
public class PT_DebugLogActionSO : PT_InteractionActionSO
{
    [Tooltip("콘솔에 출력할 메시지")]
    [TextArea]
    public string message;

    public override void Execute(Vector3 position, GameObject self, GameObject other)
    {
        Debug.Log($"Action Log: {message}\nTriggered at: {position}");
    }
}
