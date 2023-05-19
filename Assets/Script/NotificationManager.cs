using System;
using System.Collections;
using UnityEngine;
#if UNITY_IOS
using Unity.Notifications.iOS;
#endif
public class NotificationManager : MonoBehaviour
{

#if UNITY_IOS
    private void Start()
    {
        StartCoroutine(RequestAuthorization());
    }
    IEnumerator RequestAuthorization()
    {
        using (var req = new AuthorizationRequest(AuthorizationOption.Alert | AuthorizationOption.Badge, true))
        {
            Debug.Log(req.IsFinished);
            while (!req.IsFinished)
            {
                yield return null;
            };

            string res = "\n RequestAuthorization:";
            res += "\n finished: " + req.IsFinished;
            res += "\n granted :  " + req.Granted;
            res += "\n error:  " + req.Error;
            res += "\n deviceToken:  " + req.DeviceToken;

        PlayerPrefs.SetString("token", req.DeviceToken);

            Debug.Log(res);
        }
    }

    public void OnClick()
    {
        Debug.Log("Sending Notif");
        SendNotification();
    }

    private void SendNotification()
    {
        var timeTrigger = new iOSNotificationTimeIntervalTrigger()
        {
            TimeInterval = new TimeSpan(0, 0, 1),
            Repeats = false
        };

        var notification = new iOSNotification()
        {
            // You can specify a custom identifier which can be used to manage the notification later.
            // If you don't provide one, a unique string will be generated automatically.
            Identifier = "testNotification",
            Title = "Test Notification",
            Body = "Testing the notification capability",
            Subtitle = "What does the subtitle look like",
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "category_a",
            ThreadIdentifier = "thread1",
            Trigger = timeTrigger,
        };

        iOSNotificationCenter.ScheduleNotification(notification);
    }
#endif
}
