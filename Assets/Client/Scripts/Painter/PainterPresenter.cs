using VContainer.Unity;

namespace Client
{
    public class PainterPresenter : IStartable
    {
        private readonly IPainterActivator painterActivator;

        public PainterPresenter(IPainterActivator painterActivator)
        {
            this.painterActivator = painterActivator;
        }

        public void Start()
        {
            painterActivator.Initialize();
        }
    }
}