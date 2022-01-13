using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCharacterCollision : MonoBehaviour
{
    //reference code from https://www.youtube.com/watch?v=-yjKyI8NfKA
    public CapsuleCollider characterCollider;
    public CapsuleCollider characterBlockerCollider;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(characterCollider, characterBlockerCollider, true);
    }

}
