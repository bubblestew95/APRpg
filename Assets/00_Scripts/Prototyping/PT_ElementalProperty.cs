using UnityEngine;

/// <summary>
/// 오브젝트의 속성을 정의하는 열거형.
/// </summary>
public enum ElementalType
{
    None,
    Fire,
    Water,
    Wind
}

/// <summary>
/// 오브젝트에 속성을 부여하는 프로토타이핑용 컴포넌트.
/// </summary>
public class PT_ElementalProperty : MonoBehaviour
{
    [Tooltip("현재 오브젝트의 속성"), SerializeField]
    private ElementalType currentElement = ElementalType.None;

    [Tooltip("이 오브젝트에 적용될 수 있는 속성들"), SerializeField]
    private ElementalType[] validElements;

    public void SetElement(ElementalType newElement)
    {
        if (System.Array.Exists(validElements, element => element == newElement))
        {
            currentElement = newElement;
            Debug.Log($"Element set to: {currentElement}");
        }
        else
        {
            Debug.Log($"Element {newElement} is not valid for this object.");
        }
    }
}
