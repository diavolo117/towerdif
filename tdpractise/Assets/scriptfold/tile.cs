using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] public int cost1 = 30;
    [SerializeField] public int cost2 = 50;
    [SerializeField] public int cost3 = 80;
    [SerializeField] public GameObject buttontile;
    [SerializeField] public GameObject button1;
    [SerializeField] public GameObject botton2;
    [SerializeField] public GameObject botton3;
    private int index;
    public void indextower()
    {
        if (NewMonoBehaviourScript.Instance.Tryspendmoney(cost1))
        {
            Instantiate(towerprefab1, transform.position, Quaternion.identity);
            hastower = true;
            menuUI.SetActive(false);
            
            
        }
    }

    public void twndextower()
    {
        if (NewMonoBehaviourScript.Instance.Tryspendmoney(cost2))
        {
            Instantiate(towerprefab2, transform.position, Quaternion.identity);
            hastower = true;
            menuUI.SetActive(false);
            
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) 
            {
                if (hit.collider.gameObject == buttontile)
                {
                    ontileclick();

                }
                if (hit.collider.gameObject == button1)
                {
                    indextower();

                }
                if (hit.collider.gameObject == botton2)
                {
                    twndextower();

                }
                if (hit.collider.gameObject == botton3)
                {
                    trndextower();

                }
            }
        
        
        }
    }
    public void trndextower()
    {
        if (NewMonoBehaviourScript.Instance.Tryspendmoney(cost3))
        {
            Instantiate(towerprefab3, transform.position, Quaternion.identity);
            hastower = true;
            menuUI.SetActive(false);
            
        }
    
    }
    public GameObject towerprefab1;
    public GameObject towerprefab2;
    public GameObject towerprefab3;
    public GameObject menuUI;

    private bool hastower = false;

    public void ontileclick()
    {
        if (!hastower)
        {
            menuUI.SetActive(true);
        }

    }
   
}