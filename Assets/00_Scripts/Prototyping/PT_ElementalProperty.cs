using UnityEngine;

/// <summary>
/// 오브젝트의 속성을 정의하는 열거형.
/// </summary>
public enum EElementalType
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
    private EElementalType currentElement = EElementalType.None;

    [Tooltip("이 오브젝트에 적용될 수 있는 속성들"), SerializeField]
    private EElementalType[] validElements;

    public void SetElement(EElementalType newElement)
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

    public void TakeDamageWithElement(EElementalType attackingElement)
    {
        // TODO: 속성 상성 로직 구현
        Debug.Log($"{gameObject.name} (속성: {currentElement})이(가) {attackingElement} 속성의 공격을 받았습니다.");


    }
}
