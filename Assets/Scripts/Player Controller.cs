using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, walk, jump;
    public string currentState;
    public float speed;
    public float movement;
    private Rigidbody2D rigidbody;
    public string currentAnimation;
    private Vector3 characterScale; // Keep scale of character you setting before
    public float jumpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        characterScale = transform.localScale;
        rigidbody = GetComponent<Rigidbody2D>();
        currentState = "Idle";
        SetCharacterState(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    // Set Character animation
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentAnimation)) return;

        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }

    //check character state and sets the animation accordingly
    public void SetCharacterState(string state)
    {

        if (state.Equals("Walking"))
        {
            SetAnimation(walk, true, 2f);
        }
        else if (state.Equals("Jumping"))
        {
            SetAnimation(jump, false, 1f);
        }
        else
        {
            SetAnimation(idle, true, 1f);
        }
    }

    public void Move()
    {
        movement = Input.GetAxis("Horizontal");
        rigidbody.velocity = new Vector2(movement * speed, rigidbody.velocity.y);
        if (movement != 0)
        {
            SetCharacterState("Walking");
            if (movement > 0)
            {
                transform.localScale = new Vector2(characterScale.x, characterScale.y);
            }
            else
            {
                transform.localScale = new Vector2(-characterScale.x, characterScale.y);
            }
        }
        else
        {
            SetCharacterState("Idle");
        }
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
            SetCharacterState("Jumping");
        }
    }
}
