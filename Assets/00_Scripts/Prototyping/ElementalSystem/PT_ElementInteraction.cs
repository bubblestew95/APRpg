using UnityEngine;

[System.Serializable]
public class PT_ElementInteraction
{
    [Tooltip("상호작용할 다른 속성")]
    public PT_ElementSO otherElement;

    [Tooltip("상호작용 결과로 생성될 이펙트 프리팹")]
    public GameObject interactionEffectPrefab;

    [Tooltip("상호작용 시 가해질 데미지 양")]
    public float damage;
}
