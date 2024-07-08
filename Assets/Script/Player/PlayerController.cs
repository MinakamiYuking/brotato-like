using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private MobileJoystick joystick;

    [Header(" Settings ")]
    [SerializeField] private float moveSpeed ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        rigid.velocity = joystick.getMoveVector() * moveSpeed * Time.deltaTime;
    }
}
