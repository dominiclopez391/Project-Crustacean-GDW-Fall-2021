using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialHider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
