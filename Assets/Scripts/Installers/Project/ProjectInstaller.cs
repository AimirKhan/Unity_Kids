using Reflex.Core;
using Reflex.Enums;
using Services.Save;
using UnityEngine;
using Resolution = Reflex.Enums.Resolution;

namespace Installers.Project
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType(typeof(SaveService), Lifetime.Singleton, Resolution.Lazy);
        }
    }
}
