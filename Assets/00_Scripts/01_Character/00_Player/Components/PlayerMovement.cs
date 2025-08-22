using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    #region Public, Serialized Fields

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    #endregion

    #region Private Fields

    private CharacterMover characterMover = null;

    private Vector3 moveDirection = Vector3.zero;

    #endregion

    #region Properties
    #endregion

    #region Public Methods

    #endregion

    #region Private Methods

    private void Init()
    {
        CharacterController characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController component is missing from the Player GameObject.");
            return;
        }

        characterMover = new CharacterMover(characterController);
    }

    private void HandleMove(GameEvents.PlayerMoveEvent _moveEvent)
    {
        moveDirection.x = _moveEvent.moveDirection.x;
        moveDirection.z = _moveEvent.moveDirection.y;
    }

    private void Move(Vector3 direction)
    {
        if (characterMover == null)
        {
            Debug.LogError("CharacterMover is not initialized.");
            return;
        }

        characterMover.Move(direction, moveSpeed);
    }

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        GameEventManager.Instance.Subscribe<GameEvents.PlayerMoveEvent>(HandleMove);
    }

    private void OnDisable()
    {
        GameEventManager.Instance.Unsubscribe<GameEvents.PlayerMoveEvent>(HandleMove);
    }

    private void Update()
    {
        if (moveDirection != Vector3.zero)
        {
            Move(moveDirection);
        }
    }

    #endregion

}
