using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    bool isFollowing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReactToPlayer()
    {
        if (!isFollowing)
        {
            // if enough treats, follow, otherwise hiss at player
            Messenger.Broadcast(GameEvent.CAT_COLLECTED);
        }
    }
}
