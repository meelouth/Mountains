using System;

namespace Client
{
    public interface IAutomaticPainter
    {
        event Action OnPaint;
        void Paint(ModelView model, Action<ModelAnalyzeProgress> onProgress, Action onComplete);
    }
}