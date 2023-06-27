using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    public Board board;
    public Score score;
    private bool istimerrunning = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (istimerrunning)
        {
            if (score.timer > 0)
            {
                score.timer -= Time.deltaTime;
            }
            else
            {
                score.timer = 0;
                istimerrunning = false;
            }
        }
    }
}
