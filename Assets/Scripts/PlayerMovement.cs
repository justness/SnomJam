using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 100f;
    public TextMeshProUGUI dashCountUI;
    public bool dashing = false;
    public float dashTimer = 3f;
    public int dashCount = 0;
    public float dashCharge = 1;
    
    Transform t;
    Rigidbody rb;

    bool canDash = true;
    int maxDashes = 10;

    float distToGround;
    
    Animator anim;

    void Start()
    {
        t = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;

        anim = GetComponentInChildren<Animator>();
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.5f);
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
                rb.velocity += new Vector3(forwardMove.x, 0, forwardMove.z);
            }
            if (Input.GetKey(KeyCode.S)) {
                rb.velocity += new Vector3(-forwardMove.x, 0, -forwardMove.z);
            }
            if (Input.GetKey(KeyCode.D)) {
                rb.velocity += new Vector3(rightMove.x, 0, rightMove.z);
            }
            if (Input.GetKey(KeyCode.A)) {
                rb.velocity += new Vector3(-rightMove.x, 0, -rightMove.z);
            }
            anim.SetBool("Running", true);
        }
        else {
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
        if (canDash && Input.GetMouseButton(0)){
            if (dashCharge < 5){
                dashCharge += Time.deltaTime;
            }
        }
        if (canDash && Input.GetMouseButtonUp(0)){
            StartCoroutine(Dash());
        }
        
        // Air time
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)){
            StartCoroutine(Jump());
        }
        
        if (!IsGrounded()) {
            rb.velocity += new Vector3(0,-.3f,0);
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
        StartCoroutine(FovChange());

        float boost = dashCharge+(dashCount*.2f);
        rb.velocity = t.forward*moveSpeed*boost;
        yield return new WaitForSeconds(.1f*dashCount);

        dashing = false;
        rb.velocity = Vector3.zero;
        if (dashCount == maxDashes) canDash = false;
        
        anim.ResetTrigger("Attack");
    }

    IEnumerator FovChange(){
        for (int i = 0; i < 4; i++){
            Camera.main.fieldOfView += i;
            yield return new WaitForSeconds(.02f*dashCount);
        }
        for (int i = 0; i < 4; i++){
            Camera.main.fieldOfView -= i;
            yield return new WaitForSeconds(.02f*dashCount);
        }
    }
    
    IEnumerator CoolDown() {
        int dashed = dashCount;
        for (int i = 0; i < dashed; i++){
            dashCount--;
            UpdateDashCount();
            yield return new WaitForSeconds(.25f);
        }
        canDash = true;
        dashCount = 0;
    }
    
    IEnumerator Jump(){
        for (int i = 0; i < 5; i++) {
            rb.velocity += new Vector3(0, 10-i, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    void UpdateDashCount() {
        dashCountUI.text = "<shake a="+(dashCount*.05).ToString()+">"+dashCount.ToString();
        Color textColor = Color.black;
        textColor.a = (1f/maxDashes) * dashCount;
        if (textColor.a > 1) textColor.a = 1;
        dashCountUI.color = textColor;
    }
}