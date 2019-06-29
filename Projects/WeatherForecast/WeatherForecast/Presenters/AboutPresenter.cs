using WeatherForecast.UserControls.UserControlInterfaces;

namespace WeatherForecast.UserPresenters
{
    class AboutPresenter
    {
        private IAboutUserControl _aboutUserControl;
        private Model _model;

        public AboutPresenter(IAboutUserControl aboutUserControl, Model model)
        {
            _aboutUserControl = aboutUserControl;
            _model = model;

            _aboutUserControl.Load_ += Load;
        }

        private void Load()
        {
            _aboutUserControl.Path_ = _model.GetAboutSource();
        }
    }
}
