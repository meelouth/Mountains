using Michsky.UI.ModernUIPack;
using TMPro;
using UnityEngine;

namespace Client
{
    public class ProgressWindow : Window
    {
        [SerializeField] private ProgressBar progressBar;
        [SerializeField] private TextMeshProUGUI titleLabel;

        public void SetProgress(int progress)
        {
            progressBar.ChangeValue(progress);
        }

        public void SetTitle(string title)
        {
            titleLabel.text = title;
        }
    }
}