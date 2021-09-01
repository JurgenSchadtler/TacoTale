using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasActionPoints : MonoBehaviour
{
    public int actions = 4;
    
    public Image[] actionBullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < actions)
                actionBullet[i].enabled = true;
            else
                actionBullet[i].enabled = false;
        }
    }
    public void setpoints(int h)
    {
        actions = h;
    }
}
