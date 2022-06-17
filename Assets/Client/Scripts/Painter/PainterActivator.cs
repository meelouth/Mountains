namespace Client
{
    public class PainterActivator : IPainterActivator
    {
        private readonly IPainter painter;
        private readonly IAutomaticPainter automaticPainter;

        public PainterActivator(IPainter painter, IAutomaticPainter automaticPainter)
        {
            this.painter = painter;
            this.automaticPainter = automaticPainter;
        }

        public void Initialize()
        {
            automaticPainter.OnPaint += OnAutomaticPaint;
        }

        private void OnAutomaticPaint()
        {
            painter.Enable();
        }
    }
}