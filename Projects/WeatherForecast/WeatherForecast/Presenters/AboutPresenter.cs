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
        }
    }
}
