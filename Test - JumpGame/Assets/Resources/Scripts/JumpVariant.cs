using UnityEngine;

[CreateAssetMenu(menuName = "JumpVariants", fileName = "JV")]
public class JumpVariant : ScriptableObject
{
    [SerializeField] public int JumpAnimIndex;
    [SerializeField] public float JumpZoneSize;
}