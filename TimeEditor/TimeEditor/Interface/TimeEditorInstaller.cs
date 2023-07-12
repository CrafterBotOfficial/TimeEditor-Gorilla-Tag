using ComputerInterface.Interfaces;
using Zenject;
using static TimeEditor.Interface.Views.EditTimeView;

namespace TimeEditor.Interface
{
    internal class TimeEditorInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<IComputerModEntry>().To<Entry>().AsSingle();
        }
    }
}
