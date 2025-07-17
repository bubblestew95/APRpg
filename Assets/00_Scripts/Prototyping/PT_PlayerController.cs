using UnityEngine;
using UnityEngine.InputSystem;

public class PT_PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    private CharacterController characterController = null;
    private Vector3 moveDirection = Vector3.zero;

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

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController component is missing on the GameObject.");
        }
    }

    private void Update()
    {
        if (characterController == null)
        {
            return;
        }
        
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
