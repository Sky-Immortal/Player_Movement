using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float speed = 2f;
    public float range = 4f;

    private Vector3 startPosition;
    private bool movingRight = true;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveObstacle();
    }

    private void MoveObstacle()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            if (transform.position.x >= startPosition.x + range)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position -= Vector3.right * speed * Time.deltaTime;

            if (transform.position.x <= startPosition.x - range)
            {
                movingRight = true;
            }
        }
    }
}
