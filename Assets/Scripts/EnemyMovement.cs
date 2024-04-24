using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [SerializeField] private float speed;

    [SerializeField] private float distance;

    [SerializeField] private LayerMask floor;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.velocity = new Vector2(speed * transform.right.x, rb2D.velocity.y);

        RaycastHit2D floorInfo = Physics2D.Raycast(transform.position, transform.right, distance, floor);

        if (floorInfo){
            Flip();
        }
    }

    private void Flip(){
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * distance);
    }
}
