using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfo", menuName = "Scriptable Objects/Enemy")]
public class EnemyInfo : ScriptableObject
{
    // Valuable
    #region
    // Enemy Value
    #region
    [Serializable]
    public class EnemyValue
    {
        [Min(1)] public int maxHealth;

        public enum Type
        {
            Normal,
            Boss

        }
        public Type type = Type.Normal;

    }

    [SerializeField] private EnemyValue enemyValue;
    public EnemyValue GetEnemyValue() { return enemyValue; }

    #endregion

    // Detail
    #region
    [Serializable]
    public class Detail
    {
        public string enemyName;
        public string description;
    }

    public Detail enemyDetail;
    public Detail GetEnemyDetail() { return enemyDetail; }

    #endregion


    #endregion

}
