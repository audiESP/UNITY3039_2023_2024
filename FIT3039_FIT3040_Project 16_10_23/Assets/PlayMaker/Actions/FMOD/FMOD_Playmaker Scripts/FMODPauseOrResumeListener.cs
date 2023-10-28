using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("FMOD")]
	[Tooltip("Pause or resume all FMOD events.")]
	public class FMODPauseOrResumeListener : FsmStateAction
	{
		public FsmBool Pause;

        public override void Reset()
        {
			Pause = false;
        }

        public override void OnEnter()
        {

			FMODUnity.RuntimeManager.PauseAllEvents(Pause.Value);

		}
	}

}
