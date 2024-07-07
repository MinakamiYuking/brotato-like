
using UnityEngine;

public class MobileJoystick : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private RectTransform joyStickOutLine;
    [SerializeField] private RectTransform joyStickKnob;

    [Header(" Settings ")]
    [SerializeField] private float moveFactor;
    private Vector3 clickedPosition;
    private Vector3 move ;
    private bool canControl;


    // Start is called before the first frame update
    void Start()
    {
        HideJoystick();
    }

    // Update is called once per frame
    void Update()
    {
        if (canControl)
        {
            ControlJoystick();
        }
    }
    public void CilckedOnJoystickZoneCallBack()
    {
        clickedPosition = Input.mousePosition;
        joyStickOutLine.transform.position = clickedPosition;
        ShowJoystick();
    }
    private void ControlJoystick()
    {
        Vector3 currentPosition = Input.mousePosition;
        Vector3 direction = currentPosition - clickedPosition ;
        float moveMangnitude = direction.magnitude* moveFactor / Screen.width;
        moveMangnitude = Mathf.Min(moveMangnitude, joyStickOutLine.rect.width / 2);
        move = direction.normalized * moveMangnitude;
        Vector3 targetPosition = clickedPosition + move;
        joyStickKnob.position = targetPosition;

        if (Input.GetMouseButtonUp(0))
        {
            HideJoystick();
            move = Vector3.zero;
        }

    }

    private void ShowJoystick()
    {
        joyStickOutLine.gameObject.SetActive(true);
        canControl = true;
    }

    private void HideJoystick()
    {
        joyStickOutLine.gameObject.SetActive(false);
        canControl = false ;
    }

    public Vector3 getMoveVector()
    {
        return move;
    }
}
