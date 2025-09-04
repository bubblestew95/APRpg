using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatusSO", menuName = "Scriptable Objects/CharacterStatusSO")]
public class CharacterStatusSO : ScriptableObject
{
    #region Public, Serialized Fields
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected float attackPoint = 10f;
    [SerializeField] protected float defensePoint = 1f;

    #endregion

    #region Private Fields
    #endregion

    #region Properties

    public int MaxHealth
    {
        get { return maxHealth; }
    }

    public float AttackPoint
    {
        get { return attackPoint; }
    }

    public float DefensePoint
    {
        get { return defensePoint; }
    }

    #endregion
}
