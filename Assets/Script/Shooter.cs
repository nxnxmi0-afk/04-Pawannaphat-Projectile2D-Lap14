using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem; 

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;    
    [SerializeField] private GameObject target;      
    [SerializeField] private GameObject bulletPrefab;

    void Start()
    {
       GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Vector2 screenPos = Mouse.current.position.ReadValue();

        if (Input.GetMouseButtonDown(0))
        {
           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red, 5f);

           RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

           if(hit.collider != null)
            {
                target.transform.position = new Vector2(hit.point.x, hit.point.y);
                Debug.Log(hit.collider.name);

                Vector2 projectileVelocity = CalculateProjectileVelocity(shootPoint.position , hit.point, 1f);

                Rigidbody2D shootBuller = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();

                shootBuller.linearVelocity = projectileVelocity;
                
            }
        }
    }
    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;

        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

        Vector2 projectileVelocity = new Vector2(velocityX, velocityY); 

        return projectileVelocity;
    }
}
