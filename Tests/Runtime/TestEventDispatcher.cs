using EasyHelpers.Runtime.Tools.EventService;
using UnityEngine;
using UnityEngine.UI;

namespace EasyHelpers.Tests.Runtime
{
    public class TestEventDispatcher : MonoBehaviour
    {
        [SerializeField] private Button eventOneTriggerButton;
        [SerializeField] private Button eventTwoTriggerButton;

        private TestEvent1 testEvent1;
        private TestEvent2 testEvent2;

        private void Awake()
        {
            testEvent1 = new()
            {
                testIntValue = Random.Range(0, 10),
                testBoolValue = Random.Range(0, 10) % 2 == 0,
                testFloatValue = Random.Range(0, 10)
            };
            testEvent2 = new()
            {
                testStringValue = $"String:{UnityEngine.Random.Range(0, 10)}"
            };
            eventOneTriggerButton.onClick.AddListener(OnClickEventOneTriggerButton);
            eventTwoTriggerButton.onClick.AddListener(OnClickEventTwoTriggerButton);
        }

        private void OnClickEventOneTriggerButton()
        {
            EventManager.CallEvent<TestEvent1>(testEvent1);
        }

        private void OnClickEventTwoTriggerButton()
        {
            EventManager.CallEvent<TestEvent2>(testEvent2);
        }
    }
}
