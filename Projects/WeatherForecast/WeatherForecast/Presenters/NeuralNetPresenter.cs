using WeatherForecast.UserControls.UserControlInterfaces;

namespace WeatherForecast.UserPresenters
{
    class NeuralNetPresenter
    {
        private INeuralNetUserControl _neuralNetUserControl;
        private Model _model;

        public NeuralNetPresenter(INeuralNetUserControl neuralNetUserControl, Model model)
        {
            _neuralNetUserControl = neuralNetUserControl;
            _model = model;
        }
    }
}
