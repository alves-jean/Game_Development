using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;
    public float minX = 0f;

    private float maxX;
    private float initialY;

    void Start()
    {
        initialY = transform.position.y;

        GameObject finish = GameObject.FindGameObjectWithTag("Finish");

        if (finish != null)
        {
            Camera cam = Camera.main;

            float halfWidth =
                cam.orthographicSize * cam.aspect;

            // Faz a borda direita da câmera parar na Flag
            maxX = finish.transform.position.x - halfWidth + 2f;
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        float targetX = Mathf.Clamp(
            player.position.x,
            minX,
            maxX
        );

        Vector3 targetPosition = new Vector3(
            targetX,
            initialY,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}