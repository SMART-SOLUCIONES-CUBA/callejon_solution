using SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Presenters.Control;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SmartSolucionesCuba.SAPRESSC.WpfApp.CPCC.Billboard.FixScreenClient.Presenters
{
    public class ContentSlideshowPresenter
    {
        private ContentControl persistentSlideContainer;
        private ContentControl transitionSlideContainer;
        private TextBlock advisesTextBlock;

        private System.EventHandler endSelfPresentationHandler;


        public event System.EventHandler FullScreenRequired;

        public event System.EventHandler FullScreenUnrequired;

        public ContentSlideshowPresenter(ContentControl persistentSlideContainer, ContentControl transitionSlideContainer, TextBlock advisesTextBlock, System.EventHandler endSelfPresentationHandler)
        {
            this.persistentSlideContainer = persistentSlideContainer;
            this.transitionSlideContainer = transitionSlideContainer;
            this.advisesTextBlock = advisesTextBlock;
            this.endSelfPresentationHandler = endSelfPresentationHandler;
        }

        public void InitPresentation()
        {
            advisesTextBlock.Text = GetAdvisesTextBlockValue();

            if (Providers.PlaylistProvider.GetSingleInstance().IsEmpty)
            {
                endSelfPresentationHandler(this, new System.EventArgs { });
            }
            else
            {
                PresentSlide(Providers.PlaylistProvider.GetSingleInstance().DequeueForPresentation());
            }
        }

        private string GetAdvisesTextBlockValue()
        {
            var advisesSeparator = " || ";

            var result = string.Empty;

            if (Providers.CachedDataProvider.GetSingleInstance().Advises.Count <= 0)
            {
                result = Providers.CachedDataProvider.GetSingleInstance().Configuration.AdvisesChainPlaceholder;
            }
            else
            {
                result = string.Join(advisesSeparator, Providers.CachedDataProvider.GetSingleInstance().Advises.Select(item => item.Message));
            }

            if (Providers.CachedDataProvider.GetSingleInstance().Configuration.AdvisesChainWellcome != null && Providers.CachedDataProvider.GetSingleInstance().Configuration.AdvisesChainWellcome.Length > 0)
            {
                result = Providers.CachedDataProvider.GetSingleInstance().Configuration.AdvisesChainWellcome + advisesSeparator + result;
            }

            result = result + advisesSeparator + "SMART SOLUCIONES CUBA & Centro Provincial de Cine de Camaguey, 2018. Todos los derechos reservados.";

            return result;
        }

        private void PresentSlide(UserControl control)
        {
            (control as IPresentableControl).PresentationShowInEventHandler = OnPresentationShowIn;
            (control as IPresentableControl).PresentationEndedEventHandler = OnPresentationEnded;

            transitionSlideContainer.Content = control;

            if ((control as IPresentableControl).RequireFullScreen)
            {
                FullScreenRequired?.Invoke(this, System.EventArgs.Empty);
            }
            else
            {
                FullScreenUnrequired?.Invoke(this, System.EventArgs.Empty);
            }
        }

        private void OnPresentationShowIn(object sender, System.EventArgs e)
        {
            persistentSlideContainer.Content = transitionSlideContainer.Content;
            transitionSlideContainer.Content = null;

            if (sender is IPresentableControl)
            {
                var requeridExternalDelayTime = (sender as IPresentableControl).RequiredExternalDelayTime;

                if (requeridExternalDelayTime == System.TimeSpan.Zero) return;

                var timer = new DispatcherTimer
                {
                    Interval = requeridExternalDelayTime
                };

                timer.Tick += new System.EventHandler(OnBillboardDelayTimerTick);

                timer.Start();
            }
        }

        private void OnBillboardDelayTimerTick(object sender, System.EventArgs e)
        {
            var timer = sender as DispatcherTimer;

            OnPresentationEnded(this, e);

            timer.Stop();
        }

        private void OnPresentationEnded(object sender, System.EventArgs e)
        {
            if (Providers.PlaylistProvider.GetSingleInstance().IsEmpty)
            {
                endSelfPresentationHandler(this, new System.EventArgs { });
            }
            else
            {
                PresentSlide(Providers.PlaylistProvider.GetSingleInstance().DequeueForPresentation());
            }
        }
    }
}
