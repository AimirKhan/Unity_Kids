using FSM;
using FSM.StateMachines;
using FSM.States;
using Reflex.Core;
using Reflex.Enums;
using UnityEngine;
using Resolution = Reflex.Enums.Resolution;

namespace Installers.Game
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private Camera mainCamera;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            RegisterGameStates(containerBuilder);
            RegisterMono(containerBuilder);
        }
        
        private void RegisterGameStates(ContainerBuilder builder)
        {
            // Register state machine
            builder.RegisterType(typeof(GameplayStateMachine), Lifetime.Singleton, Resolution.Lazy);
            
            RegisterStateByType<BootstrapState>();
            RegisterStateByType<StartState>();
            RegisterStateByType<PlayState>();
            return;

            void RegisterStateByType<T>() where T : BaseState
            {
                builder.RegisterType(typeof(T), new[] { typeof(BaseState) },
                    Lifetime.Singleton, Resolution.Lazy);
            }
        }

        private void RegisterMono(ContainerBuilder builder)
        {
            builder.RegisterValue(mainCamera);
        }
    }
}
