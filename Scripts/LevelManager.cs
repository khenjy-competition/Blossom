using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject BG;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(BG);
    }

    private void ReStart()
    {

    }
}