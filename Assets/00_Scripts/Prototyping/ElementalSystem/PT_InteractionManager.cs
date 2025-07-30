using UnityEngine;
using System.Collections.Generic;
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

    public void HandleInteraction(PT_ElementSO element1, PT_ElementSO element2, Vector3 position)
    {
        if (element1 == null || element2 == null) return;

        // element1의 상호작용 목록에서 element2를 찾습니다.
        var interaction = element1.interactions.FirstOrDefault(i => i.otherElement == element2);

        if (interaction != null)
        {
            // 상호작용 결과 처리
            Debug.Log($"{element1.elementName}이(가) {element2.elementName}와(과) 상호작용하여 {interaction.damage}의 데미지를 입혔습니다!");

            if (interaction.interactionEffectPrefab != null)
            {
                Instantiate(interaction.interactionEffectPrefab, position, Quaternion.identity);
            }
        }
    }
}
