using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterInput
{
    bool IsMoving { get; }

    Vector3 MovementVector { get; }
}
