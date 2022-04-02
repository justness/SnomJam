using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    Transform t;
    Rigidbody rb;
    float moveSpeed = 12f;
    float dashSpeed = 100f;

    public TextMeshProUGUI dashCountUI;
    bool canDash = true;
    public float dashTimer = 2f;
    public int dashCount = 0;
    int maxDashes = 10;
    Vector3 direction;

    float distToGround;

    void Start()
    {
        t = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        direction = t.forward;
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    void Update()
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
        }
        else direction = t.forward;

        // Dash
        if (canDash && dashTimer <= 0 && dashCount > 0){
            canDash = false;
            StartCoroutine(CoolDown());
        }
        if (canDash && Input.GetMouseButtonDown(0)){
            StartCoroutine(Dash());
        }
        // Air time
        if (IsGrounded() && canDash && Input.GetKeyDown(KeyCode.Space)){
            StartCoroutine(Jump());
            dashCount++;
            dashTimer = 2f;
            UpdateDashCount();
        }
        if (!IsGrounded()){
            t.position += new Vector3(0,-.05f,0);
        }
    }

    IEnumerator Dash()
    {
        dashCount++;
        dashTimer = 2f;

        UpdateDashCount();

        // TODO: Adjust these values for feel.
        // TODO: Change dash direction to current velocity direction rather than forward?
        Vector3 forwardMove = direction * Time.deltaTime * (dashSpeed * dashCount) * (1f/maxDashes);
        for (int i = 0; i < maxDashes+1; i++){
            t.position += forwardMove;
            if (t.position.y < 1) t.position = new Vector3(t.position.x, 1, t.position.z);
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator CoolDown(){
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
        for (int i = 0; i < 5; i++){
            t.position += new Vector3(0,1,0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    void UpdateDashCount(){
        dashCountUI.text = dashCount.ToString();
        Color textColor = Color.white;
        textColor.a = (1f/maxDashes) * dashCount;
        if (textColor.a > 1) textColor.a = 1;
        dashCountUI.color = textColor;
    }
}
