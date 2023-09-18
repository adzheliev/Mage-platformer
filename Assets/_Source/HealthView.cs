using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthView : MonoBehaviour
{
    [SerializeField] private GameObject _hp1, _hp2, _hp3;

    public void UpdateView(int hp)
    {
        if(hp > 2)
        {
            _hp1.SetActive(true);
            _hp2.SetActive(true);
            _hp3.SetActive(true);
        }
        else if(hp > 1)
        {
            _hp1.SetActive(true);
            _hp2.SetActive(true);
            _hp3.SetActive(false);
        }
        else if(hp > 0)
        {
            _hp1.SetActive(true);
            _hp2.SetActive(false);
            _hp3.SetActive(false);
        }
        else
        {
            _hp1.SetActive(false);
            _hp2.SetActive(false);
            _hp3.SetActive(false);
        }
    }
}
