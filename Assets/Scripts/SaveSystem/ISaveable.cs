using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Frog.SaveManagment 
{
    public interface ISaveable
    {
        object SaveState();

        void LoadState(object state);
    }
}
