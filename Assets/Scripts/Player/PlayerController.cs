using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Rigidbody playerRB;
    Vector3 movementVelocity;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    //for physics stuff
    private void FixedUpdate()
    {
        playerRB.MovePosition(playerRB.position + (movementVelocity * Time.fixedDeltaTime));
    }

    public void SetMovementVelocity(Vector3 velocity)
    {
        movementVelocity = velocity;
    }

    public void LookAtPoint(Vector3 point)
    {
        //make it look at the same height as player
        Vector3 correctedLookAtPoint = new Vector3(point.x, transform.position.y, point.z);
        
        transform.LookAt(correctedLookAtPoint);

        Debug.DrawLine(transform.position, transform.position + (transform.forward * 2), Color.red);
    }
}
