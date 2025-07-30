using UnityEngine;
using System.Linq;

public class PT_InteractionManager : MonoBehaviour
{
    public static PT_InteractionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // self: 상호작용을 일으킨 주체
    // other: 상호작용의 대상
    public void HandleInteraction(PT_ElementSO element1, PT_ElementSO element2, Vector3 position, GameObject self, GameObject other)
    {
        if (element1 == null || element2 == null) return;

        var interaction = element1.interactions.FirstOrDefault(i => i.otherElement == element2);

        if (interaction != null && interaction.actions != null)
        {
            Debug.Log($"{element1.elementName} interacts with {element2.elementName}.");
            foreach (var action in interaction.actions)
            {
                if (action != null)
                {
                    action.Execute(position, self, other);
                }
            }
        }
    }
}