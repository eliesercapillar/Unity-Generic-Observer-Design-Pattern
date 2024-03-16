using System;
using UnityEditor;
using UnityEngine;

namespace Tools
{
    // Be sure to specify the type this is an editor for.
    // Specified types should of course have an Observer<> property to be manipulated.

    //[CustomEditor(typeof(PUT_TYPE_HERE))]
    public class ObserverEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // When adding persistent events to the observer:
            //   - The referencing type must be UnityEngine.Object.
            //   - The method needs to be public.
            //   - No anonymous/lambdas.
            //   - An event's parameter type has the either match the event type or be serializable into the callback.
            
            // TYPE varName = target;

            // Add buttons as necessary for your use case.
            if (GUILayout.Button("Increment Value"))
            {
                // Define logic for the object that contains the observer here.
                throw new NotImplementedException();
            }

            if (GUILayout.Button("Decrement Value"))
            {
                // Define logic for the object that contains the observer here.
                throw new NotImplementedException();
            }

            if (GUILayout.Button("Add Debugger"))
            {
                //varName.Observer.AddListener(Debugger.Instance.DebugValue);
                throw new NotImplementedException();
            }

            if (GUILayout.Button("Remove Debugger"))
            {
                //varName.Observer.RemoveListener(Debugger.Instance.DebugValue);
                throw new NotImplementedException();
            }
        }
    }
}

