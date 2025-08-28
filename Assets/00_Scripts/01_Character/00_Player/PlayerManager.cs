using UnityEngine;

/// <summary>
/// 플레이어를 총괄적으로 관리하는 매니저 클래스.
/// </summary>
public class PlayerManager : MonoBehaviour
{
    #region Public, Serialized Fields
    #endregion

    #region Private Fields

    #region  Components

    private PlayerInputController playerInputCtrl = null;

    #endregion

    #endregion

    #region Properties
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods

    private void Initialize()
    {
        playerInputCtrl = GetComponent<PlayerInputController>();
        if (playerInputCtrl == null)
        {
            Debug.LogError("PlayerInputController component is missing on PlayerManager.");
            return;
        }

        playerInputCtrl.Initialize(this);
    }

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        Initialize();
    }

    #endregion
}
