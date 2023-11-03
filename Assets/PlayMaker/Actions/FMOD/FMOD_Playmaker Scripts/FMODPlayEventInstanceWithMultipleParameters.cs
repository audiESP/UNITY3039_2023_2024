using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{

    [ActionCategory("FMOD")]
    [Tooltip("Play an FMOD event with 3D Parameter.")]
    public class FMODPlayEventInstanceWithMultipleParameters : FsmStateAction
    {
        public FsmOwnerDefault gameObject;
        [Tooltip("The position to play the event at.")]
        public FsmVector3 position;
        [RequiredField]
        [Tooltip("You need the full Event path. To know the full event path, you can add the FMOD Studio Event Emitter component to the Game Object, select the event using the magnifying glass icon, and then copy the string value to this box.")]
        public FsmString FMODEvent;
        //[Title("Parameters")]
        [ActionSection("Parameters")]
        public FsmString ParameterName;
        public FsmFloat ParameterValue;
        public FsmString ParameterName2;
        public FsmFloat ParameterValue2;
        public FsmString ParameterName3;
        public FsmFloat ParameterValue3;
        public FsmString ParameterName4;
        public FsmFloat ParameterValue4;

        public bool EveryFrame;

        private FMOD.Studio.EventInstance Instance;
        private FMOD.Studio.PARAMETER_ID paramID;
        private FMOD.Studio.PARAMETER_ID paramID2;
        private FMOD.Studio.PARAMETER_ID paramID3;
        private FMOD.Studio.PARAMETER_ID paramID4;

        public override void Reset()
            {
            gameObject = null;
            FMODEvent = null;
            ParameterName = null;
            ParameterValue = null;
            ParameterName2 = null;
            ParameterValue2 = null;
            ParameterName3 = null;
            ParameterValue3 = null;
            ParameterName4 = null;
            ParameterValue4 = null;
            position = null;
            EveryFrame = false;
            }

            // Code that runs on entering the state.
            public override void OnEnter()
            {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);

            if (FMODEvent.Value == null)
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
                eventInfo.getParameterDescriptionByName(ParameterName2.Value, out paramDescription);
                eventInfo.getParameterDescriptionByName(ParameterName3.Value, out paramDescription);
                eventInfo.getParameterDescriptionByName(ParameterName4.Value, out paramDescription);

                paramID = paramDescription.id;
                paramID2 = paramDescription.id;
                paramID3 = paramDescription.id;
                paramID4 = paramDescription.id;

            Instance.start();
                Instance.release();

                if (!EveryFrame)
                {
                    Instance.setParameterByID(paramID, ParameterValue.Value);
                    Instance.setParameterByID(paramID2, ParameterValue2.Value);
                    Instance.setParameterByID(paramID3, ParameterValue3.Value);
                    Instance.setParameterByID(paramID4, ParameterValue4.Value);
                Finish();
                }

            }

            public override void OnUpdate()

            {
            Instance.setParameterByID(paramID, ParameterValue.Value);
            Instance.setParameterByID(paramID2, ParameterValue2.Value);
            Instance.setParameterByID(paramID3, ParameterValue3.Value);
            Instance.setParameterByID(paramID4, ParameterValue4.Value);
            }


     }

}
