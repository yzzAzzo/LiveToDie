using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{

    public Animator animator;
    public LayerMask water;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        var isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(1f, 1f), 0f, water);

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        animator.SetFloat("Horizontal",movement.x);
        animator.SetFloat("Vertical",movement.y);
        animator.SetFloat("Magnitude",movement.magnitude);

        transform.position = transform.position + (movement * Time.deltaTime);

    }

    private void OnCollisionEnter()
    {
        Debug.Log("Hit deteceted");
    }

}
    