using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Client
{
    public class AutomaticPainter : MonoBehaviour, IAutomaticPainter
    {
        private IConfiguration configuration;

        public event Action OnPaint;

        [Inject]
        public void Construct (IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Paint(ModelView model, Action<ModelAnalyzeProgress> onProgress, Action onComplete)
        {
            UniTask.Void(() => PaintAsync(model, onProgress, onComplete));
        }

        private async UniTaskVoid PaintAsync(ModelView model, Action<ModelAnalyzeProgress> onProgress, Action onComplete)
        {
            if (model == null)
                await UniTask.CompletedTask;

            var mesh = model.Mesh;
            
            var (min, max) = await FindMinMaxYAsync(mesh, onProgress);

            UniTask.Void(() => PaintModel(model, min, max, onProgress, onComplete));
        }

        private async UniTaskVoid PaintModel(ModelView model, float min, float max,
            Action<ModelAnalyzeProgress> onProgress, Action onComplete)
        {
            var mesh = model.Mesh;
            
            model.Vertices = new Dictionary<int, Vertex>();
            model.Colors = new Color[mesh.vertices.Length];
            
            model.HighestPoint = max;
            model.LowestPoint = min;
                
            var progress = new ModelAnalyzeProgress(0, ModelAnalyzeState.Paint);

            for (var index = 0; index < mesh.vertices.Length; index++)
            {
                var vert = mesh.vertices[index];
                var height = Mathf.InverseLerp(min,max,vert.z);
                var color = configuration.Gradient.Evaluate(height);
                
                model.Colors[index] = color;
                model.Vertices.Add(index, new Vertex(vert.z));

                if (index % configuration.ProgressThreshold == 0)
                {
                    progress.Percent = (int)((index / (float)mesh.vertices.Length) * 100);
                    onProgress?.Invoke(progress);
                    await UniTask.Yield();
                }
            }
            
            mesh.SetColors(model.Colors);
            
            onComplete?.Invoke();
            OnPaint?.Invoke();

            await UniTask.Yield();
        }

        private async UniTask<Tuple<float, float>> FindMinMaxYAsync(Mesh mesh, Action<ModelAnalyzeProgress> onProgress)
        {
            var min = float.MaxValue;
            var max = float.MinValue;

            var progress = new ModelAnalyzeProgress(0, ModelAnalyzeState.Analyze);
            
            for (var index = 0; index < mesh.vertices.Length; index++)
            {
                var vert = mesh.vertices[index];

                if (vert.z > max)
                {
                    max = vert.z;
                }

                if (vert.z < min)
                {
                    min = vert.z;
                }
                
                if (index % configuration.ProgressThreshold == 0)
                {
                    progress.Percent = (int)((index / (float)mesh.vertices.Length) * 100);
                    onProgress?.Invoke(progress);
                    await UniTask.Yield();
                }
            }

            return new Tuple<float, float>(min, max);
        }
    }
}