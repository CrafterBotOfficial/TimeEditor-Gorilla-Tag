using ComputerInterface.Interfaces;
using TimeEditor.Interface.Views;
using Zenject;

namespace TimeEditor.Interface
{
    internal class MainInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<IComputerModEntry>().To<MainView.Entry>().AsSingle();
        }
    }
}