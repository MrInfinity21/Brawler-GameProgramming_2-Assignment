using UnityEngine;

[CreateAssetMenu(fileName = "Movement Configuration", menuName = "Game Configs/Movement Configuration")]
public class MovementConfig : ScriptableObject
{
    public float targetMoveSpeed = 5f;
    public float baseJumpForce = 8f;
    public float gravityMultiplier = 5f;
}
