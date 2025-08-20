using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputModule
{
    #region Public Fields
    #endregion

    #region Private Fields

    /// <summary>
    /// 플레이어를 총괄적으로 관리하는 플레이어 매니저.
    /// </summary>
    private PlayerManager playerMng = null;

    private PlayerInput playerInputComp = null;
    #endregion

    #region Properties
    #endregion

    #region Public Methods

    public void Initialize(PlayerManager playerManager)
    {
        playerMng = playerManager;

        if (playerMng == null)
        {
            Debug.LogError("playerManager component is null.");
            return;
        }

        playerInputComp = playerMng.GetComponent<PlayerInput>();
        if (playerInputComp == null)
        {
            Debug.LogError("PlayerInput component is missing on PlayerManager.");
            return;
        }
    }

    #endregion

    #region Private Methods

    

    #endregion
}
