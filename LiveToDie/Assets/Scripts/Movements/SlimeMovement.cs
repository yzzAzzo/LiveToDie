using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float Min;
    public float Max;

    Animator animator;
    Rigidbody2D rb;
    private float movementDuration = 2.0f;
    private float waitBeforeMoving = 2.0f;
    private bool hasArrived = false;

    void Update()
    {
        if (!hasArrived)
        {
            hasArrived = true;
            float randX = Random.Range(-0.5f, 0.5f);
            float randZ = Random.Range(-0.5f, 0.5f);
            StartCoroutine(MoveToPoint(new Vector2(transform.position.x + randX, transform.position.y + randZ)));
        }
    }

    //Lerping ---> https://chicounity3d.wordpress.com/2014/05/23/how-to-lerp-like-a-pro/
    // coroutine
    private IEnumerator MoveToPoint(Vector2 targetPos)
    {
        float timer = 0.0f;
        Vector2 startPos = transform.position;

        while (timer < movementDuration)
        {
            timer += Time.deltaTime;
            float t = timer / movementDuration;    
            transform.position = Vector2.Lerp(startPos, targetPos, t);

            yield return null;
        }

        yield return new WaitForSeconds(waitBeforeMoving);
        hasArrived = false;
    }
}
