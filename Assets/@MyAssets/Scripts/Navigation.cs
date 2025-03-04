using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;

public class Navigation : MonoBehaviour
{

    [SerializeField]
    private NavMeshAgent zombie;
    public GameObject player;
    private bool atacking = false;
    [SerializeField]
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<XROrigin>().GameObject();
    }
    // Update is called once per frame
    void Update()
    {
        if (!atacking)
        {
            zombie.SetDestination(player.transform.position);
            if (Vector3.Distance(player.transform.position, transform.position) < 0.9)
            {
                zombie.SetDestination(this.transform.position);
                StartCoroutine(atack());
            }
        }
        

    }
    public IEnumerator atack()
    {
       
        atacking = true;
        animator.SetBool("Atack", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("Atack", false);
        atacking = false;
    }
}
