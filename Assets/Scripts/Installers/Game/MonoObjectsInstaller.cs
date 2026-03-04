using Game.SquaresScroll;
using Reflex.Core;
using UnityEngine;

namespace Installers.Game
{
    public class MonoObjectsInstaller : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private SquaresScrollView  squaresScrollView;
        
        public void RegisterMonoObjects(ContainerBuilder builder)
        {
            // Register scene MainCamera
            builder.RegisterValue(mainCamera);
            builder.RegisterValue(squaresScrollView);
        }
    }
}