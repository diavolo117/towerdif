using UnityEngine;

public class lookatb : MonoBehaviour
{
    private Camera maincam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maincam = Camera.main;
    }
    private void LateUpdate()
    {
        transform.forward = maincam.transform.forward;  
    }

}
