using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        animator.SetBool("movingLeft", moveX < 0);
        animator.SetBool("movingRight", moveX > 0);
        animator.SetBool("movingUp", moveY > 0);
        animator.SetBool("movingDown", moveY < 0);
    }
}
