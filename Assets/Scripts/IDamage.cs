using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IDamage 
{
    int Health { get; set; }

    void Damage();
}
