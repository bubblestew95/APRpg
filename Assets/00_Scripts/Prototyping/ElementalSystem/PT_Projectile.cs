using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class PT_Projectile : MonoBehaviour
{
    [Tooltip("이 투사체가 가진 속성")]
    public PT_ElementSO myElement;

    private void OnCollisionEnter(Collision collision)
    {
        var otherProjectile = collision.gameObject.GetComponent<PT_Projectile>();

        if (otherProjectile != null && otherProjectile.myElement != null)
        {
            // InteractionManager에 자신(self)과 상대방(other) 게임 오브젝트를 전달합니다.
            PT_InteractionManager.Instance.HandleInteraction(
                myElement, 
                otherProjectile.myElement, 
                collision.contacts[0].point, 
                this.gameObject,             // 나의 게임 오브젝트
                collision.gameObject       // 상대방의 게임 오브젝트
            );

            // 테스트를 위해 상호작용 후 스스로를 파괴합니다.
            Destroy(gameObject);
        }
    }
}