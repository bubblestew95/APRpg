using UnityEngine;

// 이 스크립트를 사용하려면 게임 오브젝트에 Collider와 Rigidbody 컴포넌트가 필요합니다.
[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class PT_Projectile : MonoBehaviour
{
    [Tooltip("이 투사체가 가진 속성")]
    public PT_ElementSO myElement;

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트에서 PT_Projectile 컴포넌트를 가져옵니다.
        var otherProjectile = collision.gameObject.GetComponent<PT_Projectile>();

        // 상대방도 투사체이고, 속성을 가지고 있다면 상호작용을 처리합니다.
        if (otherProjectile != null && otherProjectile.myElement != null)
        {
            PT_InteractionManager.Instance.HandleInteraction(
                myElement, 
                otherProjectile.myElement, 
                collision.contacts[0].point // 충돌 지점에서 이펙트가 발생하도록 위치 전달
            );

            // 테스트를 위해 상호작용 후 스스로를 파괴합니다.
            Destroy(gameObject);
        }
    }
}
