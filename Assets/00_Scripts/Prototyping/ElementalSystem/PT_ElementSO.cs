using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PT_Element", menuName = "Prototype/Elemental System/Element", order = 0)]
public class PT_ElementSO : ScriptableObject
{
    [Tooltip("속성의 이름 (예: 불, 물)")]
    public string elementName;

    [Tooltip("이 속성이 다른 속성과 상호작용하는 규칙 리스트")]
    public List<PT_ElementInteraction> interactions;
}
