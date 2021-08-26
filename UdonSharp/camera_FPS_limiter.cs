/*
*===========================================================*
*       _      _   ____              _          _           *
*      | | ___| |_|  _ \  ___   __ _| |    __ _| |__  ___   *
*   _  | |/ _ \ __| | | |/ _ \ / _` | |   / _` | '_ \/ __|  *
*  | |_| |  __/ |_| |_| | (_) | (_| | |__| (_| | |_) \__ \  *
*   \___/ \___|\__|____/ \___/ \__, |_____\__,_|_.__/|___/  *
*                              |___/                        *
*===========================================================*
*                                                           *
*                  Auther: Jetdog8808                       *
*                                                           *
*===========================================================*
*/

using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace JetDog.Prefabs
{
    public class camera_FPS_limiter : UdonSharpBehaviour
    {
        [Range(1,90)]
        public int fps = 24;
        private Camera cam;

        void Start()
        {
            cam = GetComponent<Camera>();
        }

        public void RenderWait()
        {
            cam.enabled = false;
            /* calculate how many frames to wait to meet camera fps requirements.
             * 
             * target time => (1.0f / fps)
             * how many seconds between target frames.
             * 
             * delay frames => target time / Time.deltaTime
             * divide target time by how fast came is currently rendering frames.
             * giving back how many frames to wait. 
             * 
             * will enable camera after frame delays.
             */
            SendCustomEventDelayedFrames(nameof(RenderSetup), (int)Mathf.Ceil((1.0f / fps) / Time.deltaTime), VRC.Udon.Common.Enums.EventTiming.Update);
        }

        public void RenderSetup()
        {
            cam.enabled = true;
        }
        
        public void OnPostRender()
        {
            //cant dissable camera on same frame being rendered will render nothing. so delays till next frame to dissable. 
            SendCustomEventDelayedFrames(nameof(RenderWait), 0, VRC.Udon.Common.Enums.EventTiming.Update);
        }
    }
}
