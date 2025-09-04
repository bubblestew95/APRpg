using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatusSO", menuName = "Scriptable Objects/PlayerStatusSO")]
public class PlayerStatusSO : CharacterStatusSO
{
    #region Public, Serialized Fields

    [SerializeField] private int level = 1;
    [SerializeField] private int elementalStack = 2;
    [SerializeField] private int requireExp = 100;
    
    #endregion
}
