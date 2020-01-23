using UnityEngine;
using System.Collections;

public class GameAssets : MonoBehaviour {

    public static GameAssets i;

    private void Awake()
    {
        i = this;
    }
    public Sprite snakeHeadSprite;

}
