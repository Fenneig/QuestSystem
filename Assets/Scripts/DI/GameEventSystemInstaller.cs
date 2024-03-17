using Zenject;

namespace QuestSystem.DI
{
    public class GameEventSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameEventSystem>().FromNew().AsSingle();
        }
    }
}