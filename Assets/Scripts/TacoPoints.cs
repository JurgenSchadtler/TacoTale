using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TacoPoints : MonoBehaviour
{
    private int tacos = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<UnityEngine.UI.Text>().text = "Ingredientes para el taco supremo: " + tacos + "/5";
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void setTacos(int n)
    {
        tacos = n;
        this.GetComponent<UnityEngine.UI.Text>().text = "Ingredientes para el taco supremo: " + tacos + "/5";
    }
}
