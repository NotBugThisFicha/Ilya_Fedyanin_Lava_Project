using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotMover : XPMover
{
    // Start is called before the first frame update
    protected override void Start()
    {
        Carrot.CarrotFarmedEvent += Add;
    }

    protected override void OnDestroy()
    {
        Carrot.CarrotFarmedEvent -= Add;
    }
}
