namespace Client
{
    public class ModelAnalyzeProgress
    {
        public int Percent { get; set; }
        public ModelAnalyzeState State { get; set; }

        public ModelAnalyzeProgress(int percent, ModelAnalyzeState state)
        {
            Percent = percent;
            State = state;
        }
    }
}