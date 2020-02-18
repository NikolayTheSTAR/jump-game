using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private JumpVariant[] variants = new JumpVariant[3];

    private Vector3 StartPos; // начальная позиция
    private float Step;

    public static Transform tran;
    public static Animator anim;
    public static bool isJump; // истинно, если осуществляется прыжок
    public static bool inJumpZone; // истинно, если игрок находится в зоне прыжка
    
    private static JumpVariant[] Variants = new JumpVariant[3];
    private static Vector3 JumpToPos; // позиция, в которую прыгает игрок
    private static float RatioXZ; // соотношение осей XZ
    private static float JumpSpeed; // скорость в прыжке

    void Start()
    {
        tran = transform;
        anim = GetComponent<Animator>();
        StartPos = transform.position;
        Variants = variants;
        UpdateRatio(JumpZone.Pos);
    }

    void Update()
    {
        // обычное перемещение
        if (!isJump)
        {
            // в двух осях
            if (RatioXZ != 0)
            {
                Step = Speed * Time.deltaTime;

                float sZ, sX;

                sZ = Mathf.Sqrt((Step * Step) / (RatioXZ * RatioXZ + 1));
                sX = sZ * RatioXZ;

                transform.Translate(sX, 0, sZ);
            }
            
            // в одной
            else
            {
                Step = Speed * Time.deltaTime;

                float sZ;

                sZ = Step;

                transform.Translate(0, 0, sZ);
            }
        }
        // перемещение в прыжке
        else
        {
            // в двух осях
            if (RatioXZ != 0)
            {
                Step = JumpSpeed * Time.deltaTime;

                float sZ, sX;

                sZ = Mathf.Sqrt((Step * Step) / (RatioXZ * RatioXZ + 1));
                sX = sZ * RatioXZ;

                transform.Translate(-sX, 0, -sZ);
            }

            // в одной
            else
            {
                Step = JumpSpeed * Time.deltaTime;

                float sZ;

                sZ = Step;

                transform.Translate(0, 0, -sZ);
            }
        }
    }

    // обновление соотношения XZ
    private static void UpdateRatio(Vector3 FinalPos)
    {
        Vector3 pos = tran.position;

        float deltaX = FinalPos.x - pos.x;
        float deltaZ = FinalPos.z - pos.z;

        // исключения
        if (deltaX == 0) RatioXZ = 0;
        else if (deltaZ == 0) RatioXZ = 1;

        // стандарт
        else RatioXZ = deltaX / deltaZ;

        //tran.GetChild(0).transform.Rotate(new Vector3(0, RatioXZ * 90, 0));
    }

    // обновление варианта
    public static void UpdateVariant()
    {
        if (anim.GetInteger("JumpIndex") == 1)
        {
           anim.SetInteger("JumpIndex", Variants[1].JumpAnimIndex);
        }
        else if (anim.GetInteger("JumpIndex") == 2)
        {
            anim.SetInteger("JumpIndex", Variants[2].JumpAnimIndex);
        }
        else
        {
            anim.SetInteger("JumpIndex", Variants[0].JumpAnimIndex);
        }
    }

    // обновление скорости движения в прыжке
    private static void UpdateJumpSpeed()
    {
        // разныца по осям

        Vector3 pos = tran.position;

        float deltaX = JumpToPos.x - pos.x;
        float deltaZ = JumpToPos.z - pos.z;

        // расстояние

        float Dim = Mathf.Sqrt(Mathf.Pow(deltaX, 2) + Mathf.Pow(deltaZ, 2));

        // скорость прыжка
        JumpSpeed = Dim;
    }

    // прыжок
    public static void Jump(Vector3 newJumpToPos)
    {
        if (!isJump & inJumpZone)
        {
            JumpToPos = newJumpToPos;

            UpdateRatio(JumpToPos);
            UpdateJumpSpeed();

            Score.AddScore();

            anim.SetTrigger("Jump");
            isJump = true;
        }
    }

    // конец прыжка
    private void EndJump()
    {
        isJump = false;
        UpdateRatio(JumpZone.Pos);
        UpdateVariant();
        JumpZone.UpdateVariant();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "EndZone")
        {
            Restart();
        }
        else if (col.tag == "JumpZone")
        {
            inJumpZone = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "JumpZone")
        {
            inJumpZone = false;
        }
    }

    // перезапуск
    private void Restart()
    {
        tran.position = StartPos;
        UpdateRatio(JumpZone.Pos);
    }
}