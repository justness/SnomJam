using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 12f;
    [SerializeField] float dashSpeed = 10;
    [SerializeField] float jumpSpeed = 0.1f;
    public TextMeshProUGUI dashCountUI;
    public bool dashing = false;
    public float dashTimer = 2f;
    public int dashCount = 0;
    
    Transform t;
    Rigidbody rb;

    bool canDash = true;
    int maxDashes = 20;
    Vector3 direction;

    float distToGround;
    
    Animator anim;

    void Start()
    {
        t = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        direction = t.forward;

        anim = GetComponentInChildren<Animator>();
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    void FixedUpdate()
    {
        Vector3 forwardMove = t.forward * Time.deltaTime * moveSpeed;
        Vector3 rightMove = t.right * Time.deltaTime * moveSpeed;
        dashTimer -= Time.deltaTime;
        
        // Movement
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)){
            Vector3 originalPos = t.position;
            
            if (Input.GetKey(KeyCode.W)) {
                t.position = new Vector3(t.position.x + forwardMove.x, t.position.y, t.position.z + forwardMove.z);
            }
            if (Input.GetKey(KeyCode.S)) {
                t.position = new Vector3(t.position.x - forwardMove.x, t.position.y, t.position.z - forwardMove.z);
            }
            if (Input.GetKey(KeyCode.D)) {
                t.position = new Vector3(t.position.x + rightMove.x, t.position.y, t.position.z + rightMove.z);
            }
            if (Input.GetKey(KeyCode.A)) {
                t.position = new Vector3(t.position.x - rightMove.x, t.position.y, t.position.z - rightMove.z);
            }
            
            direction = Vector3.Normalize(t.position - originalPos);
            
            anim.SetBool("Running", true);
        }
        else {
            direction = t.forward;
            if (!dashing) rb.velocity = new Vector3(0, rb.velocity.y, 0);
            
            anim.SetBool("Running", false);
        }
    }

    void Update()
    {
        // Dash
        if (canDash && dashTimer <= 0 && dashCount > 0 || !canDash && dashTimer <= 0 && dashCount == maxDashes){
            canDash = false;
            StartCoroutine(CoolDown());
        }
        if (canDash && Input.GetMouseButtonDown(0)){
            StartCoroutine(Dash());
        }
        
        // Air time
        if (IsGrounded() && canDash && Input.GetKeyDown(KeyCode.Space)){
            //Debug.Log("jumped");
            StartCoroutine(Jump());
            dashCount++;
            dashTimer = 2f;
            UpdateDashCount();
        }
        
        if (!IsGrounded()) {
            rb.velocity += new Vector3(0,-.75f,0);
        }

        if (rb.transform.position.y < 0)
        {
            rb.transform.position = new Vector3(rb.transform.position.x, 0, rb.transform.position.z);
        }
    }

    IEnumerator Dash()
    {
        anim.SetTrigger("Attack");
        
        dashing = true;
        dashCount++;
        dashTimer = 2f;

        UpdateDashCount();

        // TODO: Adjust these values for feel.
        Vector3 forwardMove = direction * (dashSpeed * dashCount) * (1f/maxDashes);
        for (int i = 0; i < maxDashes+1; i++){
            rb.velocity += forwardMove;
            yield return new WaitForSeconds(0.01f);
        }

        dashing = false;
        rb.velocity = Vector3.zero;
        if (dashCount == maxDashes) canDash = false;
        
        anim.ResetTrigger("Attack");
    }
    
    IEnumerator CoolDown() {
        int dashed = dashCount;
        for (int i = 0; i < dashed; i++){
            dashCount--;
            UpdateDashCount();
            yield return new WaitForSeconds(1.2f);
        }
        canDash = true;
        dashCount = 0;
    }
    
    IEnumerator Jump(){
        for (int i = 0; i < 5; i++) {
            rb.velocity += new Vector3(0, 15-i, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    void UpdateDashCount() {
        dashCountUI.text = dashCount.ToString();
        Color textColor = Color.white;
        textColor.a = (1f/maxDashes) * dashCount;
        if (textColor.a > 1) textColor.a = 1;
        dashCountUI.color = textColor;
    }
}
