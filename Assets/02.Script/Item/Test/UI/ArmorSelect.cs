using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSelect : SelectTest
{
    [SerializeField]
    private string subCategory;
    [SerializeField]
    private int indexOverlapping;

    public string SubCateogy => subCategory;
    public int IndexOverlapping => indexOverlapping;

    public override SelectTest TestClone()
    {
        return this;
    }
}
