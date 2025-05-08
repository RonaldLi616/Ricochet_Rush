using UnityEngine;

[CreateAssetMenu(fileName = "PlayerRecord", menuName = "Scriptable Objects/PlayerRecord")]
public class PlayerRecord : ScriptableObject
{
    [Min(0)]public int ballRemains = 0;
}
