using UnityEngine;

public class SeguirPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 distancia;
    void FixedUpdate()
    {
        transform.position = player.position + distancia;
    }
}
