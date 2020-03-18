using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private ICharacterInput playerInput;

    public ICharacterInput PlayerInput
    {
        get { return playerInput; }
    }

    protected override void Awake()
    {
        base.Awake();

        playerInput = GetComponent<ICharacterInput>();
        if(playerInput == null)
        {
            Debug.LogError("You need to add some Input");
        }
    }
}
