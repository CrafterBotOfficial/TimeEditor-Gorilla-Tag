using ComputerInterface.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace TimeEditorGorillaTag.Computer
{
    internal class MainInstaller : Installer
    {
        public override void InstallBindings()
        {
            // Bind your mod entry like this
            Container.Bind<IComputerModEntry>().To<ModEntry>().AsSingle();
        }
    }
    internal class ModEntry : IComputerModEntry
    {
        public string EntryName => ModInfo.ModName;
        public Type EntryViewType => typeof(Views.MainView);
    }
}
