using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInputManager : MonoBehaviour, ICharacterInput
{
    int horizontal = 0;
    int vertical = 0;

    public bool IsMoving { get; private set; }
    public Vector3 MovementVector { get; private set; }

    private void Update()
    {
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        if(horizontal != 0 || vertical != 0)
        {
            IsMoving = true;            
        }

        MovementVector = IsMoving ? new Vector3(horizontal, 0, vertical).normalized : Vector3.zero;
    }
}
