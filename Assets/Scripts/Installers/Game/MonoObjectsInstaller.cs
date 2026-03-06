using Game.Hole;
using Game.Level;
using Game.SquaresScroll;
using Game.Tower;
using Reflex.Core;
using Services.MessageService;
using UnityEngine;
using UnityEngine.InputSystem.UI;

namespace Installers.Game
{
    public class MonoObjectsInstaller : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private SquaresScrollView  squaresScrollView;
        [SerializeField] private DragIconView dragIconView;
        [SerializeField] private TowerView towerView;
        [SerializeField] private HoleView holeView;
        [SerializeField] private InputSystemUIInputModule uiModule;
        [SerializeField] private MessageView messageView;
        
        public void RegisterMonoObjects(ContainerBuilder builder)
        {
            // Register scene MainCamera
            builder.RegisterValue(mainCamera);
            
            builder.RegisterValue(squaresScrollView);
            builder.RegisterValue(dragIconView);
            
            builder.RegisterValue(towerView);
            builder.RegisterValue(holeView);
            
            // Register Input Action Map
            builder.RegisterValue(uiModule.actionsAsset);
            
            // Register Services
            builder.RegisterValue(messageView);
        }
    }
}