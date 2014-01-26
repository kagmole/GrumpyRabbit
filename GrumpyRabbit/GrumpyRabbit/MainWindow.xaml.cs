using GrumpyRabbit.Core;

using System.Windows;

namespace GrumpyRabbit
{
    public partial class MainWindow : Window
    {
        private GameEngine gameEngine;

        public MainWindow()
        {
            InitializeComponent();
            LaunchGameEngine();
        }

        public void LaunchGameEngine()
        {
            gameEngine = new GameEngine(worldCanvas);
            gameEngine.LaunchEngine();
        }
    }
}
