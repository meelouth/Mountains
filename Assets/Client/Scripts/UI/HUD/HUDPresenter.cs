using VContainer.Unity;

namespace Client
{
    public class HUDPresenter : IStartable
    {
        private readonly IHUDController hudController;

        public HUDPresenter(IHUDController hudController)
        {
            this.hudController = hudController;
        }
        
        public void Start()
        {
            hudController.Initialize();
        }
    }
}