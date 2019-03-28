using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
public class fidgetSpinnerScript : MonoBehaviour
{
    public KMNeedyModule needy;

    private bool active;
    public KMSelectable spinner;
    // Use this for initialization
    void Start()
    {
        needy.OnNeedyActivation += activation;
        needy.OnNeedyDeactivation += OnNeedyDeactivation;
        needy.OnTimerExpired += OnTimerExpired;
        spinner.OnInteract += spin;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            transform.Rotate(Vector3.up * needy.GetNeedyTimeRemaining());
        }

    }
    void activation()
    {
        active = true;
    }
    protected void OnNeedyDeactivation()
    {
        active = false;
    }
    protected void OnTimerExpired()
    {
        needy.OnStrike();
        active = false;
    }

    protected bool spin()
    {
        if (needy.GetNeedyTimeRemaining() > 40)
        needy.SetNeedyTimeRemaining(needy.GetNeedyTimeRemaining() + 0);

        if ((needy.GetNeedyTimeRemaining() < 40))
            needy.SetNeedyTimeRemaining(needy.GetNeedyTimeRemaining() + 2);
        Debug.Log(needy.CountdownTime);
        return false;
    }
}
