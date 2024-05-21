using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSelectTest : SelectTest
{
    [SerializeField]
    private string subCategory;
    [SerializeField]
    private bool itemOverlapping;
    [SerializeField]
    private int indexOverlapping;
    
    public string SubCateogy => subCategory;
    public bool ItemOverlapping => itemOverlapping;
    public int IndexOverlapping => indexOverlapping;

    public override SelectTest TestClone()
    { 
        return this;
    }
}
