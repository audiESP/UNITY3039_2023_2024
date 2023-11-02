using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("FMOD")]
    [Tooltip("Play an FMOD event")]

    public class FMODPlayEventInstance : FsmStateAction

    {
        [Tooltip("The optional game object to attach the event instance to.")]
        public FsmOwnerDefault GameObject;

        [RequiredField]
        [Tooltip("You need the full Event path. To know the full event path, you can add the FMOD Studio Event Emitter component to the Game Object, select the event using the magnifying glass icon, and then copy the string value to this box.")]
        public FsmString FMODEvent;

        [Tooltip("The position to play the event at.")]
        public FsmVector3 position;

        [Tooltip("Store the event instance handle for further control.")]
        [UIHint(UIHint.Variable)]
        public FsmString storeEventInstance;

        private FMOD.Studio.EventInstance eventInstance;

        public override void Reset()
        {
            GameObject = null;
            FMODEvent = null;
            position = null;
            storeEventInstance = null;

        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(GameObject);

            if (FMODEvent.Value == null)
            {
                return;
            }

            // Create the event instance
            eventInstance = FMODUnity.RuntimeManager.CreateInstance(FMODEvent.Value);

            // Set the position of the event instance
            if (!position.IsNone)
            {
                FMOD.ATTRIBUTES_3D attributes = FMODUnity.RuntimeUtils.To3DAttributes(position.Value);
                eventInstance.set3DAttributes(attributes);
            }

            // Attach the event instance to a game object
            if (go != null)
            {
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(eventInstance, go.transform);
            }

            // Start playing the event instance
            eventInstance.start();
            eventInstance.release();

            // Store the event instance handle for further control
            if (!storeEventInstance.IsNone)
            {
                storeEventInstance.Value = eventInstance.handle.ToString();
            }

            Finish();
        }

    }
}
