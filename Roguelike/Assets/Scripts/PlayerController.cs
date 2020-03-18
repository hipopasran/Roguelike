using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask blockingLayer;

    private ICharacterInput playerInput;
    private Collider playerCollider;
    private bool step;
    private bool canMove;

    private void Start()
    {
        playerInput = InputManager.Instance.PlayerInput;
        playerCollider = GetComponentInChildren<Collider>();
    }

    private void Update()
    {
        if(playerInput.IsMoving)
        {
            Move();
        }
    }

    private void Move()
    {
        if (!step)
        {
            if (playerInput.IsMoving && playerInput.MovementVector != Vector3.zero)
            {
                canMove = AttemptMove(playerInput.MovementVector);
                playerCollider.enabled = true;
            }

            if (canMove)
            {
                step = true;
                this.transform.position += playerInput.MovementVector;
                StartCoroutine(Wait());
            }
        }
    }

    private bool AttemptMove(Vector3 movementVector)
    {
        Vector3 start = transform.position;
        Vector3 end = movementVector;

        playerCollider.enabled = false;
        if(Physics.Raycast(start, end, out RaycastHit hit, movementVector.magnitude, blockingLayer))
        {
            Debug.DrawRay(start, transform.TransformDirection(end) * (int)movementVector.magnitude, Color.blue);
            if (hit.collider.tag == "Box")
            {
                hit.collider.enabled = false;
                if(Physics.Raycast(start, end, out RaycastHit secondHit, (int)movementVector.magnitude * 2, blockingLayer))
                {
                    Debug.DrawRay(start, transform.TransformDirection(end) * (int)movementVector.magnitude * 2, Color.red);
                    if(secondHit.collider.tag == "Wall" || secondHit.collider.tag == "Box")
                    {
                        hit.collider.enabled = true;
                        return false;
                    }
                    else
                    {
                        hit.collider.enabled = true;
                        hit.collider.transform.position += movementVector;
                        return true;
                    }
                }
                else
                {
                    hit.collider.enabled = true;
                    hit.collider.transform.position += movementVector;
                    return true;
                }
            }
            else if(hit.collider.tag == "Wall")
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        return true;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        step = false;
    }
}
