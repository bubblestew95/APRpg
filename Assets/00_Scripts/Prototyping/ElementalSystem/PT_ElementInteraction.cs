using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PT_ElementInteraction
{
    [Tooltip("상호작용할 다른 속성")]
    public PT_ElementSO otherElement;

    [Tooltip("상호작용 시 순차적으로 실행될 액션들의 리스트")]
    public List<PT_InteractionActionSO> actions;
}