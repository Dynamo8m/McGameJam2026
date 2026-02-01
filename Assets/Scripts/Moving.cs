using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float distance = 3f;
    public float speed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float xOffset = Mathf.PingPong(Time.time * speed, distance * 2) - distance;
        transform.position = startPos + new Vector3(xOffset, 0f, 0f);
    }
}
