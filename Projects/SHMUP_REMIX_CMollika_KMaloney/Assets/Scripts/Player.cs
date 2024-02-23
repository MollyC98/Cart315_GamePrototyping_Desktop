using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Vector2 rawInput;

    //[SerializeField] float paddingLeft;
    //[SerializeField] float paddingRight;
    //[SerializeField] float paddingTop;
    //[SerializeField] float paddingBottom;

    Vector2 minBounds;
    Vector2 maxBounds;

    float minX;
    float maxX;

     GameObject river;

    void Start()
    {
        river =  GameObject.Find("riverfield_water");
        Debug.Log(river);
        InitBounds();
     

    }


     Shooter shooter;
    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    

    void Update()
    {
        Move();
    }

    void InitBounds()
    {
        //Camera mainCamera = Camera.main;
       // minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
        //maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));

        Camera mainCamera = Camera.main;
        //view port bounds
        // minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0)); 
        // maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));


        //get rthe bounds of the sprite (river bed)
         Debug.Log(river.GetComponent<SpriteRenderer>().bounds.extents);
         minBounds  = new Vector2(-river.GetComponent<SpriteRenderer>().bounds.extents.x,-river.GetComponent<SpriteRenderer>().bounds.extents.y);
         maxBounds  = new Vector2(river.GetComponent<SpriteRenderer>().bounds.extents.x,river.GetComponent<SpriteRenderer>().bounds.extents.y);
    
    }

    void Move()
    {
        //Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        //Vector2 newPos = new Vector2();
        //newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        //newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        //transform.position = newPos;

         Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
       // newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
       // newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);

        newPos.x = Mathf.Clamp(transform.position.x + delta.x,  minBounds.x, maxBounds.x);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y, maxBounds.y);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        //Debug.Log(rawInput);
    }

    void OnFire(InputValue value)
    {
        if(shooter != null)
        {
            shooter.isFiring = value.isPressed;

        }
    }
}
