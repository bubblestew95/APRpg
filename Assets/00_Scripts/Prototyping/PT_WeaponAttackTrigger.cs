using UnityEngine;

namespace Prototyping
{
    [RequireComponent(typeof(Collider))]
    public class PT_WeaponAttackTrigger : MonoBehaviour
    {
        [SerializeField]
        private string targetTag = "Enemy";

        private Collider weaponCollider;

        private void Awake()
        {
            weaponCollider = GetComponent<Collider>();
            if (weaponCollider != null)
            {
                weaponCollider.isTrigger = true;
                weaponCollider.enabled = false;
            }
            else
            {
                Debug.LogError("PT_WeaponAttackTrigger requires a Collider component.", this);
            }
        }

        public void EnableAttackTrigger()
        {
            if (weaponCollider != null)
            {
                weaponCollider.enabled = true;
            }
        }

        public void DisableAttackTrigger()
        {
            if (weaponCollider != null)
            {
                weaponCollider.enabled = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(targetTag))
            {
                Debug.Log($"Weapon hit: {other.name}!");
            }
        }
    }
}
