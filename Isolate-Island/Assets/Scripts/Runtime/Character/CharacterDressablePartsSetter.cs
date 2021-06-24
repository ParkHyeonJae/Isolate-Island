using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDressablePartsSetter : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteRenderer
        => _spriteRenderer = _spriteRenderer ?? GetComponent<SpriteRenderer>();


    

}
