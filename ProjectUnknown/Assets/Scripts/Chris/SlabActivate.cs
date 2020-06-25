using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlabActivate : MonoBehaviour
{
    [SerializeField]
    private Sprite activeSprite;

    private SpriteRenderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        this.myRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            this.ActivateSlab();
        }
    }

    public void ActivateSlab()
    {
        this.myRenderer.sprite = this.activeSprite;
    }
}
