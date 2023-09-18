using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Supporting
{
    public class Utils
    {
        public static bool IsInLayer(int layer, LayerMask layerMask)
        {
            return layerMask == (layerMask | (1 << layer));
        }
    }
}


