using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
     

     
    private void Awake()
    {
        Instance = this;
    }

    public Sprite bluegemSprite;
    public Sprite yellowgemSprite;
    public Sprite pinkgemSprite;
    public Sprite keySprite;
    public Sprite cat_smallSprite;
    public Sprite panel_smallSprite;
    public Sprite nitrogenSprite;
}
