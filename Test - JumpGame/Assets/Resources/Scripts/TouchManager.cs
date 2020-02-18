using UnityEngine;

public class TouchManager : MonoBehaviour
{
    Vector3 ClickPos; // позиция касания

    void OnMouseDown()
    {
        ClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (ClickPos.y > 1)
        {
            float d = ClickPos.y - 1;
            ClickPos.y = 1;
            ClickPos.z += d;
        }

        if (ClickPos.z < Player.tran.position.z)
        {
            Player.Jump(ClickPos);
        }
    }
}