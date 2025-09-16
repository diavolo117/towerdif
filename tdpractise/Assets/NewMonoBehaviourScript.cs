using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private int resource = 50;
    [SerializeField] public TMP_Text moneytext;


    private void Start()
    {
        Updatemoneyui();
    }

    private void Updatemoneyui()
    {
        moneytext.text = "Money: " + resource;
    }
    public void moneytaken()
    {
        
    }
    public bool Tryspendmoney(int cost)
    {
        if (resource > cost)
        {
            resource -= cost;
            return true;
        }
        return false;
    }
    public static NewMonoBehaviourScript Instance {  get; private set; }
    public event Action Ontick;
    [SerializeField] private float tickrate = 1;
    private Coroutine tickcoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this) {

            Destroy(gameObject); return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        Starttick();
        NewMonoBehaviourScript.Instance.Ontick += Handletick;
    }

    private void Handletick()
    {
        Updatemoneyui();
    }

    private void OnDisable()
    {
        Stoptick();
        if (NewMonoBehaviourScript.Instance != null) 
        {
            NewMonoBehaviourScript.Instance.Ontick -= Handletick;
        }
    }

    private void Stoptick()
    {
        if (tickcoroutine != null) 
        {
            StopCoroutine(tickcoroutine);
            tickcoroutine = null;
        }
    }

    private void Starttick()
    {
        if (tickcoroutine != null) return;
        tickcoroutine = StartCoroutine(tickgoing());
    }
    private IEnumerator tickgoing()
    {
        while (true)
        {

            Ontick?.Invoke();
            yield return new WaitForSeconds(tickrate);


        }
    }
}
