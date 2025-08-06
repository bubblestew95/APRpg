using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

/// <summary>
/// 프로토타입용 플레이어 캐릭터 컨트롤러.
/// Input System을 사용하여 이동, 조준, 공격 등의 입력을 처리합니다.
/// </summary>
public class PT_PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float moveSpeed = 5f; // 캐릭터의 이동 속도

    [Header("Detection Settings")]
    [SerializeField]
    private float fanAngle = 90f; // 부채꼴 감지 범위의 각도
    [SerializeField]
    private float fanRadius = 5f; // 부채꼴 감지 범위의 반지름
    [SerializeField]
    private LayerMask detectionLayer; // 감지할 오브젝트의 레이어
    [SerializeField]
    private Transform bodyTr = null; // 캐릭터의 시각적 모델(회전 처리를 위함)

    // --- 컴포넌트 및 내부 변수 ---
    private CharacterController characterController = null; // 물리 기반 이동을 위한 캐릭터 컨트롤러
    private Animator animator = null; // 애니메이션 제어를 위한 애니메이터
    private Camera mainCamera = null; // 마우스 위치를 월드 좌표로 변환하기 위한 메인 카메라

    private Vector3 moveDirection = Vector3.zero; // 캐릭터의 이동 방향
    private Vector2 mouseScreenPosition = Vector2.zero; // 마우스의 현재 화면 좌표
    private bool isAiming = false; // 현재 조준 상태인지 여부
    
    /// <summary>
    /// 입력 액션에 의해 호출되는 메소드 영역입니다.
    /// </summary>
    #region Input Actions

    /// <summary>
    /// 'Move' 입력 액션에 의해 호출됩니다.
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        moveDirection.x = input.x;
        moveDirection.z = input.y;
    }

    /// <summary>
    /// 'Look' 입력 액션에 의해 호출됩니다. (마우스 위치)
    /// </summary>
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseScreenPosition = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// 'Skill_01' 입력 액션에 의해 호출됩니다.
    /// </summary>
    public void OnSkill_01(InputAction.CallbackContext context)
    {
        Debug.Log("Skill key pressed");

        List<Collider> detectedObjects = DetectObjectsInFan();

        foreach (Collider col in detectedObjects)
        {
            Debug.Log($"Detected object: {col.gameObject.name}");
            // TODO: 스킬 로직 구현 (예: 감지된 오브젝트에 데미지 또는 효과 적용)
            col.GetComponent<PT_ElementalProperty>();
        }
    }
    
    /// <summary>
    /// 'Attack' 입력 액션에 의해 호출됩니다.
    /// </summary>
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            Debug.LogWarning("Animator component is not assigned.");
        }
    }

    /// <summary>
    /// 'Aim' 입력 액션에 의해 호출됩니다.
    /// </summary>
    public void OnAim(InputAction.CallbackContext context)
    {
        // ReadValue<float>()는 버튼이 눌리면 1, 떼면 0을 반환합니다.
        // 이를 bool 값으로 변환하여 isAiming 상태를 제어합니다.
        isAiming = context.ReadValue<float>() > 0.5f;

        Debug.Log($"OnAim called. Is aiming: {isAiming}");
    }

    /// <summary>
    /// 'Interact' 입력 액션에 의해 호출됩니다.
    /// </summary>
    public void OnInteract(InputAction.CallbackContext context)
    {
        // TODO: 상호작용 로직 구현
        Debug.Log("Interact key pressed");
    }

    #endregion

    /// <summary>
    /// 유니티에서 제공하는 콜백 메소드 영역입니다.
    /// </summary>
    #region Unity Callbacks

    private void Awake()
    {
        // 필수 컴포넌트들을 가져와 변수에 할당합니다.
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController component is missing on the GameObject.");
        }

        animator = GetComponentInChildren<Animator>();
        mainCamera = Camera.main; // "MainCamera" 태그가 있는 카메라를 찾습니다.
    }

    private void Update()
    {
        if (characterController == null)
        {
            return;
        }

        // 캐릭터 이동 처리
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // 조준 상태에 따라 캐릭터의 방향을 결정합니다.
        if (isAiming)
        {
            Aim(); // 조준 중일 때는 마우스 방향을 바라봅니다.
        }
        else
        {
            LookForward(); // 평상시에는 이동 방향을 바라봅니다.
        }

        // 현재 속도를 애니메이터의 "Speed" 파라미터에 전달하여 걷기/서기 애니메이션을 제어합니다.
        animator.SetFloat("Speed", characterController.velocity.magnitude);
    }

    #endregion

    /// <summary>
    /// 평상시(조준 중이 아닐 때) 캐릭터가 이동 방향을 바라보도록 합니다.
    /// </summary>
    private void LookForward()
    {
        if (bodyTr != null && moveDirection != Vector3.zero)
        {
            bodyTr.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    /// <summary>
    /// 조준 시 마우스 커서의 위치를 향해 캐릭터가 바라보도록 합니다.
    /// </summary>
    private void Aim()
    {
        if (mainCamera == null) return;

        // 1. 마우스 위치에서 카메라를 통해 레이(Ray)를 생성합니다.
        Ray ray = mainCamera.ScreenPointToRay(mouseScreenPosition);
        // 2. 캐릭터의 발밑을 기준으로 하는 가상의 XZ 평면을 생성합니다.
        Plane groundPlane = new Plane(Vector3.up, bodyTr.position);
        
        // 3. 레이가 평면과 충돌하는지 확인하고, 충돌했다면 그 위치를 가져옵니다.
        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 worldPoint = ray.GetPoint(distance);
            Vector3 lookDirection = worldPoint - bodyTr.position;
            lookDirection.y = 0f; // 캐릭터가 위아래로 기울지 않도록 Y축 회전은 제거합니다.

            // 4. 계산된 방향으로 캐릭터의 몸체를 회전시킵니다.
            if (lookDirection != Vector3.zero)
            {
                bodyTr.rotation = Quaternion.LookRotation(lookDirection);
            }
        }
    }

    /// <summary>
    /// 전방 부채꼴 범위 내의 오브젝트를 감지합니다.
    /// </summary>
    /// <returns>감지된 모든 Collider의 리스트</returns>
    public List<Collider> DetectObjectsInFan()
    {
        List<Collider> detectedColliders = new List<Collider>();
        Collider[] collidersInSphere = Physics.OverlapSphere(transform.position, fanRadius, detectionLayer);

        foreach (Collider col in collidersInSphere)
        {
            if (col.gameObject == this.gameObject) // 자기 자신은 제외
            {
                continue;
            }

            Vector3 directionToTarget = (col.transform.position - transform.position).normalized;

            if (directionToTarget == Vector3.zero)
            {
                continue;
            }

            // 타겟이 캐릭터의 전방 시야각 내에 있는지 확인합니다.
            if (Vector3.Angle(transform.forward, directionToTarget) < fanAngle / 2)
            {
                detectedColliders.Add(col);
            }
        }

        return detectedColliders;
    }

    /// <summary>
    /// 씬(Scene) 뷰에서 감지 범위를 시각적으로 표시하기 위한 기즈모(Gizmo)입니다.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector3 forward = transform.forward;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-fanAngle / 2, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(fanAngle / 2, Vector3.up);

        Vector3 leftRayDirection = leftRayRotation * forward;
        Vector3 rightRayDirection = rightRayRotation * forward;

        Gizmos.DrawRay(transform.position, leftRayDirection * fanRadius);
        Gizmos.DrawRay(transform.position, rightRayDirection * fanRadius);
    }
}
