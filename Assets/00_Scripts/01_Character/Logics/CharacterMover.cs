using UnityEngine;

/// <summary>
/// CharacterController를 사용하는 캐릭터를 이동시키는 클래스.
/// </summary>
public class CharacterMover
{
    private CharacterController characterController = null;

    public CharacterMover(CharacterController _characterController)
    {
        this.characterController = _characterController;
    }

    public void Move(Vector3 direction, float speed)
    {
        if (characterController == null)
        {
            Debug.LogError("CharacterController is not assigned.");
            return;
        }

        Vector3 moveDirection = direction.normalized * speed * Time.deltaTime;
        characterController.Move(moveDirection);
    }
}
