using Reflex.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace System
{
    public class Loader : MonoBehaviour
    {
        private void Start()
        {
            ContainerScope.OnSceneContainerBuilding += InstallExtra;

            SceneManager.LoadSceneAsync("Game")!.completed += operation =>
            {
                ContainerScope.OnSceneContainerBuilding -= InstallExtra;
            };
            return;

            void InstallExtra(Scene scene, ContainerBuilder builder)
            {
            }
        }
    }
}
