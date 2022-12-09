using System.Collections;
using UnityEngine;
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
        return false;
    }

    //twitch plays
    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"!{0} spin [Clicks the fidget spinner until the timer maxes out]";
    #pragma warning restore 414
    IEnumerator ProcessTwitchCommand(string command)
    {
        if (command.EqualsIgnoreCase("spin"))
        {
			if (!active)
			{
				yield return "sendtochaterror The fidget spinner cannot be clicked right now!";
				yield break;
			}
            yield return null;
            while (needy.GetNeedyTimeRemaining() < 40)
            {
                spinner.OnInteract();
                yield return new WaitForSeconds(.1f);
            }
        }
    }

    void TwitchHandleForcedSolve()
    {
        StartCoroutine(DealWithNeedy());
    }

    private IEnumerator DealWithNeedy()
    {
        while (!active) yield return null;
        while (active)
        {
            if (needy.GetNeedyTimeRemaining() < 40)
            {
                spinner.OnInteract();
                yield return new WaitForSeconds(.1f);
            }
            else
                yield return null;
        }
    }
}
