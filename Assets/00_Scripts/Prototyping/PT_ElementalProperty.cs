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
    [Tooltip("현재 오브젝트의 속성")]
    public ElementalType currentElement = ElementalType.None;

    private void OnTriggerEnter(Collider other)
    {
        PT_ElementalProperty otherElement = other.GetComponent<PT_ElementalProperty>();
        if (otherElement != null)
        {
            OnElementalCollision(otherElement.currentElement);
        }
    }

    /// <summary>
    /// 다른 속성과 충돌했을 때 호출되는 메서드.
    /// </summary>
    /// <param name="collidedElement">충돌한 오브젝트의 속성</param>
    private void OnElementalCollision(ElementalType collidedElement)
    {
        // 여기에 상호작용 로직을 구현하세요.
        Debug.Log($"Collided with {collidedElement}");

        if (currentElement == collidedElement)
        {
            Debug.Log("Same elemental type, no interaction.");
        }
        else
        {
            // 다른 속성과의 상호작용 로직을 구현합니다.
            switch (currentElement)
            {
                case ElementalType.Fire:
                    if (collidedElement == ElementalType.Water)
                    {
                        Debug.Log("Fire extinguished by Water!");
                    }
                    break;
                case ElementalType.Water:
                    if (collidedElement == ElementalType.Fire)
                    {
                        Debug.Log("Water evaporated by Fire!");
                    }
                    break;
                case ElementalType.Wind:
                    Debug.Log("Wind interacts with " + collidedElement);
                    break;
                default:
                    Debug.Log("No specific interaction defined.");
                    break;
            }
        }
    }
}
