using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Midori : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health = 100f;
    public float speed = 3f;

    public UI uiobj;

    public bool selected = false;

    public void setHealth(float health){
        if(selected) {
            this.health = health;
            uiobj.health = this.health;
            uiobj.maxHealth = this.maxHealth;
            uiobj.UpdateHealth(health);
        }
    }

    public void damage(float dmg){
        if(selected) setHealth(this.health - dmg);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
