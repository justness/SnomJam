using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttackScript : MonoBehaviour
{

    // Just for testing attack anim - to be deleted

    [SerializeField] Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(Attack());
        }



    }

    private IEnumerator Attack()
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        anim.ResetTrigger("Attack");
    }
}
