using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    private HealthView _healthView;
    private int _health;

    private int HP
    {
        get => _health;
        set
        {
            _health = value; 

            if(_health <= 0)
            {

            }

            _healthView.UpdateView(_health);   
        }
    }


    public Health(HealthView view, int defaultHealth = 3)
    {
        _healthView = view;
        _health = defaultHealth;
        _healthView.UpdateView(_health);
    }

    public void ChangeHealth(int hp = 1) => HP += hp;

}
