using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast
{
    class Presenter
    {
        private IView view;
        private Model model;

        public Presenter(IView view, Model model)
        {
            this.view = view;
            this.model = model;
        }

    }
}
