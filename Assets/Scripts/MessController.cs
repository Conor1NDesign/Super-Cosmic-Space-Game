using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessController : MonoBehaviour
{
    
    public GameObject mess;
    public GameObject messSpawnZone;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeAMess()
    {
        var messSpawnRange = new Vector3(Random.Range(-messSpawnZone.transform.localScale.x / 2, messSpawnZone.transform.localScale.x / 2), 0,
            Random.Range(-messSpawnZone.transform.localScale.z / 2, messSpawnZone.transform.localScale.z / 2));
        var messSpawnPoint = messSpawnZone.transform.position + messSpawnRange;
        Instantiate(mess, messSpawnPoint, Quaternion.identity);
    }
}
