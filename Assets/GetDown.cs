using UnityEngine;

public class GetDown : MonoBehaviour
{
    bool active = false;

    bool keep_vel = true;

    Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        ServiceProvider.Instance.DevConsole.RegisterCommand("GetDown", GetDownActive);
        ServiceProvider.Instance.DevConsole.RegisterCommand("GetDownKeepVelocity", GetDownKeepVelocity);
        Random.InitState(System.DateTime.Now.Millisecond);
    }

    private void FixedUpdate()
    {
        if (!ServiceProvider.Instance.GameState.IsInLevel | ServiceProvider.Instance.GameState.IsInDesigner)
        {
            active = false;
            Debug.Log("... eternal love");
            return;
        }

        ServiceProvider.Instance.GameState.LevelRestarted += GameState_LevelRestarted;

        if (active)
        {
            ServiceProvider.Instance.PlayerAircraft.MainCockpitRotation = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

            if (keep_vel)
            {
                ServiceProvider.Instance.PlayerAircraft.Velocity = velocity;
            }
        }
    }

    private void GameState_LevelRestarted(object sender, System.EventArgs e)
    {
        active = false;
        Debug.Log("... eternal love");
    }

    private void GetDownActive()
    {
        active = !active;

        if (active)
        {
            Debug.Log("Get down...");

            if (keep_vel)
            {
                velocity = ServiceProvider.Instance.PlayerAircraft.Velocity;
            }
        }
        else
        {
            Debug.Log("...eternal love");
        }
    }

    private void GetDownKeepVelocity()
    {
        if (active)
        {
            Debug.LogError("Cannot change constant velocity setting now!");
        }

        keep_vel = !keep_vel;
        Debug.Log("Constant velocity set to " + keep_vel.ToString());
    }
}
