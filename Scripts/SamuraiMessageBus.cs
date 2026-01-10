using System;

public static class SamuraiMessageBus
{
    public static event Action<string> OnMessage;

    public static void Publish(string message)
    {
        OnMessage?.Invoke(message);
    }
}
