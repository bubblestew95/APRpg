using UnityEngine;

/// <summary>
/// 프로토타이핑용 무기 장착 스크립트 (IK 적용).
/// 무기를 지정된 부위에 장착하고, IK를 이용해 양손으로 무기를 잡도록 제어합니다.
/// </summary>
[RequireComponent(typeof(Animator))]
public class PT_WeaponEquipment : MonoBehaviour
{
    private Animator animator;

    [Header("무기 설정")]
    [Tooltip("장착할 무기의 프리팹")]
    [SerializeField] private GameObject weaponPrefab;

    [Tooltip("무기가 생성될 부위 (보통 캐릭터의 오른손)")]
    [SerializeField] private Transform weaponParent;

    [Header("IK 설정")]
    [Tooltip("IK를 활성화할지 여부")]
    [SerializeField] private bool useIK = true;

    // 무기에 설정된 IK 타겟들
    private Transform leftHandIKTarget;
    private Transform rightHandIKTarget;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (weaponPrefab != null && weaponParent != null)
        {
            EquipWeapon();
        }
    }

    /// <summary>
    /// 무기를 장착하고 IK 타겟을 찾습니다.
    /// </summary>
    private void EquipWeapon()
    {
        GameObject weapon = Instantiate(weaponPrefab, weaponParent);
        // 위치와 회전은 프리팹에 설정된 값을 그대로 사용하므로 오프셋은 더 이상 필요 없습니다.
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        // 무기 프리팹의 자식에서 IK 타겟을 찾습니다.
        // 이름으로 찾기 때문에, 프리팹에 설정된 이름과 일치해야 합니다.
        rightHandIKTarget = weapon.transform.Find("RightHandGrip");
        leftHandIKTarget = weapon.transform.Find("LeftHandGrip");

        if (rightHandIKTarget == null || leftHandIKTarget == null)
        {
            Debug.LogError("무기 프리팹에서 'RightHandGrip' 또는 'LeftHandGrip'을 찾을 수 없습니다.");
            useIK = false; // 타겟이 없으면 IK를 비활성화
        }
    }

    // 이 함수는 Animator에 의해 매 프레임 호출됩니다. (IK Pass가 활성화된 레이어에서)
    private void OnAnimatorIK(int layerIndex)
    {
        if (!useIK || animator == null || leftHandIKTarget == null || rightHandIKTarget == null)
        {
            return; // IK를 사용하지 않거나, 필요한 컴포넌트/타겟이 없으면 실행하지 않음
        }

        // 오른손 IK 설정
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandIKTarget.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandIKTarget.rotation);

        // 왼손 IK 설정
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandIKTarget.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandIKTarget.rotation);
    }
}
