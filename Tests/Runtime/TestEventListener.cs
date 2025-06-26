using EasyHelpers.Runtime.Common;
using EasyHelpers.Runtime.Tools.EventService;
using TMPro;
using UnityEngine;

namespace EasyHelpers.Tests.Runtime
{
    public class TestEventListener : MonoBehaviour, IEventListener<TestEvent1>, IEventListener<TestEvent2>
    {
        [SerializeField] private TMP_Text outputText;

        private void OnEnable()
        {
            EventManager.AddListener<TestEvent1>(this);
            EventManager.AddListener<TestEvent2>(this);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<TestEvent1>(this);
            EventManager.RemoveListener<TestEvent2>(this);
        }

        public void OnTrigger(TestEvent1 eventData)
        {
            outputText.SetText(eventData.ToJson());
        }

        public void OnTrigger(TestEvent2 eventData)
        {
            outputText.SetText(eventData.ToJson());
        }
    }

    public class TestEvent1 : EventBase
    {
        public int testIntValue;
        public float testFloatValue;
        public bool testBoolValue;
    }
    public class TestEvent2 : EventBase
    {
        public string testStringValue;
    }
}
