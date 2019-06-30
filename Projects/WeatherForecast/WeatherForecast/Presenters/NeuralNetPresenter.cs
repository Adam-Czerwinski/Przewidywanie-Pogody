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

            _neuralNetUserControl.Load_ += Load_;
        }

        private void Load_()
        {
            _neuralNetUserControl.Path_ = _model.GetDescNNSource();
        }
    }
}
