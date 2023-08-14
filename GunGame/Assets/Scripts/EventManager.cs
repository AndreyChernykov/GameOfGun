using UnityEngine.Events;

public class EventManager
{
    public static UnityEvent<int> events = new UnityEvent<int>();
    public static UnityEvent eventsGame = new UnityEvent();

    public static void SendEvent(int n)
    {
        events.Invoke(n);
    }

    public static void SendEvent()
    {
        eventsGame.Invoke();
    }

}
