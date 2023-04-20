using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScroller : MonoBehaviour
{
    [SerializeField] private GameObject surface1;
    [SerializeField] private GameObject surface2;
    [SerializeField] private int offset;
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSurface2X(surface1,surface2);
        ChangeSurface2X(surface2, surface1);
    }

    private void ChangeSurface2X(GameObject surface1,GameObject surface2)
    {
        if (player.transform.position.x > surface1.transform.position.x)
        {
            surface2.transform.position = new Vector3(surface1.transform.position.x + offset, surface1.transform.position.y,
                surface1.transform.position.z);
        }
        else
        {
            surface2.transform.position = new Vector3(surface1.transform.position.x - offset, surface1.transform.position.y,
                surface1.transform.position.z);
        }
    }
}
