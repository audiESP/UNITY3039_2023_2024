using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{

    [ActionCategory("FMOD")]
    [Tooltip("Play an FMOD event with 3D Parameter.")]
    public class FMODPlayEventInstanceWithParameters : FsmStateAction
    {
            public FsmOwnerDefault gameObject;

        [RequiredField]
        [Tooltip("You need the full Event path. To know the full event path, you can add the FMOD Studio Event Emitter component to the Game Object, select the event using the magnifying glass icon, and then copy the string value to this box.")]
        public FsmString FMODEvent;
        public FsmString ParameterName;
        public FsmFloat ParameterValue;
        [Tooltip("The position to play the event at.")]
        public FsmVector3 position;

        public bool EveryFrame;

        private FMOD.Studio.EventInstance Instance;
        private FMOD.Studio.PARAMETER_ID paramID;

        public override void Reset()
            {
            gameObject = null;
            FMODEvent = null;
            ParameterName = null;
            ParameterValue = null;
            position = null;
            EveryFrame = false;
            }

            // Code that runs on entering the state.
            public override void OnEnter()
            {
                var go = Fsm.GetOwnerDefaultTarget(gameObject);

            if (FMODEvent.Value == null || ParameterName.Value == null)
            {
                return;
            }

            Instance = FMODUnity.RuntimeManager.CreateInstance(FMODEvent.Value);
                Instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(go.transform.position));

                if (!position.IsNone)
                {
                    FMOD.ATTRIBUTES_3D attributes = FMODUnity.RuntimeUtils.To3DAttributes(position.Value);
                    Instance.set3DAttributes(attributes);
                }

                FMOD.Studio.EventDescription eventInfo;

                Instance.getDescription(out eventInfo);

                FMOD.Studio.PARAMETER_DESCRIPTION paramDescription;

                eventInfo.getParameterDescriptionByName(ParameterName.Value, out paramDescription);

                paramID = paramDescription.id;

                Instance.start();
                Instance.release();

                if (!EveryFrame)
                {
                    Instance.setParameterByID(paramID, ParameterValue.Value);
                    Finish();
                }

            }

            public override void OnUpdate()

            {
                Instance.setParameterByID(paramID, ParameterValue.Value);
            }


     }

}
