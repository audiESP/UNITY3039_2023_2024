using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("FMOD")]
    [Tooltip("Play an FMOD event and then stop immediately upon exiting the State in which the action is in or when the FSM is disabled.")]

    public class FMODPlayEventInstanceStopOnExit : FsmStateAction

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
            eventInstance = FMODUnity.RuntimeManager.CreateInstance(FMODEvent.Value);

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


            if (!storeEventInstance.IsNone)
            {
                storeEventInstance.Value = eventInstance.handle.ToString();
            }

            Finish();
        }
        public override void OnUpdate()
        {
            // Stop and release the event instance when the FSM is disabled or the state is exited
            if (!Fsm.Active)

            {
                eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }

        public override void OnExit()
        {
            // Stop the event instance if it is still playing
            if (eventInstance.isValid())
            {
                eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }
    }
}
