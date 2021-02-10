using SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Controls;
using System.Windows.Controls;

namespace SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Presenters
{
    public class MainPresenter
    {
        private ContentControl persistentContainer;
        private ContentControl transitionContainer;

        public MainPresenter(ContentControl persistentContainer, ContentControl transitionContainer)
        {
            this.persistentContainer = persistentContainer;
            this.transitionContainer = transitionContainer;
        }

        public void InitPresentation()
        {
            var startingControl = new StartingBillboardControl(true);

            Present(startingControl);
        }

        private void Present<T>(T control)
            where T : UserControl, Control.IPresentableControl
        {
            control.PresentationShowInEventHandler = OnPresentationShowIn;
            control.PresentationEndedEventHandler = OnPresentationEnded;

            transitionContainer.Content = control;
        }

        private void OnPresentationShowIn(object sender, System.EventArgs e)
        {
            persistentContainer.Content = transitionContainer.Content;
            transitionContainer.Content = null;
        }

        private void OnPresentationEnded(object sender, System.EventArgs e)
        {
            if (persistentContainer.Content is StartingBillboardControl)
            {
                Present(new BillboardSlideshowControl());
            }
            else
            {
                Present(new StartingBillboardControl(false));
            }
        }
    }
}
