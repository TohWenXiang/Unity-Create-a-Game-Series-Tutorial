using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController), typeof(GunController))]
public class Player : Character
{
    public float moveSpeed = 5;

    PlayerController playerController;
    GunController gunController;
    Camera playerView;

    protected override void Awake()
    {
        base.Awake();
        playerController = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        playerView = Camera.main;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        MovementInput();

        LookInput();

        WeaponInput();
    }

    void MovementInput()
    {
        Vector3 inputDir;
        Vector3 moveVel;

        //-----------------Movement Input------------------
        //gather input here
        inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //normalize it into a direction vector with a magnitude of one
        inputDir.Normalize();

        //calculate the velocity by multiplying input direction with the moving speed(magnitude)
        moveVel = inputDir * moveSpeed;

        //pass movement velocity to controller so that it can move the playerS
        playerController.SetMovementVelocity(moveVel);
    }

    void LookInput()
    {
        //-----------------Look Input------------------
        //create a ray from the camera that goes through the mouse position to infinity
        Ray ray = playerView.ScreenPointToRay(Input.mousePosition);
        //create a plane for plane to intersect with
        Plane ground = new Plane(Vector3.up, Vector3.zero);
        //this is how far the ray will travel before it intersect
        float rayDist;

        //check if ray intersects with ground
        if (ground.Raycast(ray, out rayDist))
        {
            //get point of intersection using ray distance
            Vector3 intersectPoint = ray.GetPoint(rayDist);
            //Debug.DrawLine(ray.origin, intersectPoint, Color.red);
            playerController.LookAtPoint(intersectPoint);
        }
    }

    void WeaponInput()
    {
        //-----------------Weapon Input------------------
        if (Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
    }
}
