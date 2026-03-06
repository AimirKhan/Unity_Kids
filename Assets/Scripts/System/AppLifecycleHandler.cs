using Game.Tower;
using Reflex.Attributes;
using UnityEngine;

namespace System
{
    public class AppLifecycleHandler : MonoBehaviour
    {
        [Inject]
        private TowerModel towerModel;

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus) towerModel.ForceSave();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus) towerModel.ForceSave();
        }
        
        private void OnApplicationQuit() => towerModel.ForceSave();
    }
}