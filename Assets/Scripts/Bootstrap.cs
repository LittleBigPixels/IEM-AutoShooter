using System;
using System.Collections.Generic;
using UnityEngine;

public class GameApp
{
    [RuntimeInitializeOnLoadMethod]
    static void Bootstrap()
    {
        var gameAppObject = new GameObject("Game");
        gameAppObject.AddComponent<GameAppComponent>();
    }
}