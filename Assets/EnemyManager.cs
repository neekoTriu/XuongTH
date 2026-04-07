using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform pointA; 
    public Transform pointB; 
    
    //Hãy sửa lại dòng nãy hợp lý, mỗi thành viên sẽ thay phiên nhau dùng int hoặc float
    //Sau đó kiểm tra những phần còn thiếu khác để script có thể hoạt động bình thường

    public float speed;

    private Transform targetPoint;
    private bool isFacingRight = true;

    void Start()
    {
        targetPoint = pointB;  
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPoint.position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            if (targetPoint == pointA)
            {
                targetPoint = pointB;
                Flip(true); 
            }
            else
            {
                targetPoint = pointA;
                Flip(false); 
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.position.y > transform.position.y + 0.2f)
            {
                Destroy(gameObject);
            }
        }
    }

    void Flip(bool faceRight)
    {
        if (faceRight != isFacingRight)
        {
            isFacingRight = faceRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}