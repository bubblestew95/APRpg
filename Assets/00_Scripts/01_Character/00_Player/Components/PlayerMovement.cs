using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Public, Serialized Fields

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    
    #endregion

    #region Private Fields

    private CharacterMover characterMover = null;

    #endregion

    #region Properties
    #endregion

    #region Public Methods

    public void Move(Vector3 direction)
    {
        if (characterMover == null)
        {
            Debug.LogError("CharacterMover is not initialized.");
            return;
        }

        characterMover.Move(direction, moveSpeed);
    }

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

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        Init();
    }
    
    #endregion

}
