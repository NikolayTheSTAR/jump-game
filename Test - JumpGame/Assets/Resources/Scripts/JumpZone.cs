using UnityEngine;

public class JumpZone : MonoBehaviour
{
    [SerializeField] private JumpVariant[] variants = new JumpVariant[3];

    public static Vector3 Pos; // позиция зоны прыжка

    private static Transform tran;
    private static JumpVariant[] Variants;

    void Start()
    {
        Pos = transform.position;
        tran = transform;
        Variants = variants;
    }

    public static void UpdateVariant()
    {
        if (tran.localScale.x == Variants[0].JumpZoneSize)
        {
            tran.localScale = new Vector3(Variants[1].JumpZoneSize, tran.localScale.y, Variants[1].JumpZoneSize);
        }
        else if (tran.localScale.x == Variants[1].JumpZoneSize)
        {
            tran.localScale = new Vector3(Variants[2].JumpZoneSize, tran.localScale.y, Variants[2].JumpZoneSize);
        }
        else
        {
            tran.localScale = new Vector3(Variants[0].JumpZoneSize, tran.localScale.y, Variants[0].JumpZoneSize);
        }
    }
}