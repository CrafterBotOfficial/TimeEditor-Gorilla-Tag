using ComputerInterface.Interfaces;
using System;
using TimeEditor.UI.Views;
using Zenject;

namespace TimeEditor.UI
{
    public class Entry : IComputerModEntry
    {
        public string EntryName => ModInfo.ModName;
        public Type EntryViewType => typeof(MainView);
    }
    public class MainInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<IComputerModEntry>().To<Entry>().AsSingle();
        }
    }
}
