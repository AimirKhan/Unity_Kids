using Cysharp.Threading.Tasks;
using FSM;
using FSM.StateMachines;
using FSM.States;
using Game.GameSquare;
using Game.Hole;
using Game.SquaresScroll;
using Game.Tower;
using GameConfig;
using Reflex.Core;
using Reflex.Enums;
using Services;
using UnityEngine;
using Resolution = Reflex.Enums.Resolution;

namespace Installers.Game
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private MonoObjectsInstaller monoObjectsInstaller;
        [SerializeField] private GameConfigSO gameConfig;
        
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            monoObjectsInstaller.RegisterMonoObjects(containerBuilder);
            
            RegisterConfigs(containerBuilder);
            RegisterGameStates(containerBuilder);
            RegisterObjects(containerBuilder);
            RegisterFactories(containerBuilder);

            containerBuilder.RegisterValue(new DragService());
            containerBuilder.RegisterValue(this.GetCancellationTokenOnDestroy());
        }
        
        private void RegisterConfigs(ContainerBuilder builder)
        {
            builder.RegisterValue(gameConfig);
            builder.RegisterValue(gameConfig.GameSquaresSO);

        }
        
        private void RegisterGameStates(ContainerBuilder builder)
        {
            // Register state machine
            builder.RegisterType(typeof(GameplayStateMachine), Lifetime.Singleton, Resolution.Lazy);
            
            // Register states
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

        private void RegisterObjects(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(SquaresScrollModel), Lifetime.Singleton, Resolution.Lazy);
            builder.RegisterType(typeof(SquaresScrollPresenter), Lifetime.Singleton, Resolution.Lazy);
            
            builder.RegisterValue(typeof(HolePresenter));
            
            builder.RegisterValue(typeof(TowerModel));
            builder.RegisterValue(typeof(TowerPresenter));
        }

        private void RegisterFactories(ContainerBuilder builder)
        {
            
        }
    }
}
