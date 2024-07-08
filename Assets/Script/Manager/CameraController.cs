using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    [Header(" Element ")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector2 minMaxXY;
    

    void LateUpdate()
    {

        if (playerTransform == null)
        {
            Debug.LogWarning("No transform Has been specified.... ");
        }
        Vector3 playerPosition = playerTransform.position;
        playerPosition.z = -10;

        playerPosition.x = Mathf.Clamp(playerPosition.x, -minMaxXY.x, minMaxXY.x);
        playerPosition.y = Mathf.Clamp(playerPosition.y, -minMaxXY.y, minMaxXY.y);


        transform.position = playerPosition;
    }
}
