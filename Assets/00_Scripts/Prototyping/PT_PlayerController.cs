using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PT_PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float moveSpeed = 5f;

    [Header("Detection Settings")]
    [SerializeField]
    private float fanAngle = 90f;
    [SerializeField]
    private float fanRadius = 5f;
    [SerializeField]
    private LayerMask detectionLayer;
    [SerializeField]
    private Transform bodyTr = null;

    private CharacterController characterController = null;
    private Animator animator = null;
    private Vector3 moveDirection = Vector3.zero;

    #region Input Actions

    private void OnMove(InputValue _value)
    {
        if (_value == null)
        {
            moveDirection = Vector3.zero;
            return;
        }

        moveDirection.x = _value.Get<Vector2>().x;
        moveDirection.z = _value.Get<Vector2>().y;
    }

    private void OnSkill_01(InputValue _value)
    {
        Debug.Log("Skill key pressed");

        List<Collider> detectedObjects = DetectObjectsInFan();

        foreach (Collider col in detectedObjects)
        {
            Debug.Log($"Detected object: {col.gameObject.name}");
            // Implement skill logic here, e.g., apply damage or effects to detected objects

            col.GetComponent<PT_ElementalProperty>();
        }
    }
    
    private void OnAttack(InputValue _value)
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

    private void OnAim(InputValue _value)
    {
        if (_value == null)
        {
            Debug.LogWarning("Aim input value is null.");
            return;
        }

        
    }

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        if (characterController == null)
        {
            Debug.LogError("CharacterController component is missing on the GameObject.");
        }

        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (characterController == null)
        {
            return;
        }

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        if (bodyTr != null && moveDirection != Vector3.zero)
        {
            bodyTr.rotation = Quaternion.LookRotation(moveDirection);
        }

        animator.SetFloat("Speed", characterController.velocity.magnitude);
    }

    public List<Collider> DetectObjectsInFan()
    {
        List<Collider> detectedColliders = new List<Collider>();
        Collider[] collidersInSphere = Physics.OverlapSphere(transform.position, fanRadius, detectionLayer);

        foreach (Collider col in collidersInSphere)
        {
            if (col.gameObject == this.gameObject)
            {
                continue;
            }

            Vector3 directionToTarget = (col.transform.position - transform.position).normalized;

            if (directionToTarget == Vector3.zero)
            {
                continue;
            }

            if (Vector3.Angle(transform.forward, directionToTarget) < fanAngle / 2)
            {
                detectedColliders.Add(col);
            }
        }

        return detectedColliders;
    }

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

#endregion
}
