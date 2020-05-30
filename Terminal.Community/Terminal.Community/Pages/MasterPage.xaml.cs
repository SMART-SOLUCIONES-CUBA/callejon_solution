using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class MasterPage : Xamarin.Forms.ContentPage
    {
        private Models.View.NavigationSectionViewModel homeSection;
        private Models.View.NavigationSectionViewModel cinemaBillboardSection;
        private Models.View.NavigationSectionViewModel nextEventsSection;
        private Models.View.NavigationSectionViewModel databaseStatusSection;

        public MasterPage()
        {
            InitializeComponent();

            homeSection = new Models.View.NavigationSectionViewModel
            {
                Glyph = "\uF2DC",
                Title = "Inicio",
                SelectionFunc = () => new HomePage()
            };

            cinemaBillboardSection = new Models.View.NavigationSectionViewModel
            {
                Glyph = "\uF381", //F381
                Title = "Cartelera de Cine",
                SelectionFunc = () => new BillboardPage()
            };

            nextEventsSection = new Models.View.NavigationSectionViewModel
            {
                Glyph = "\uFA31",
                Title = "Próximos Eventos",
                SelectionFunc = () => new EventsPage()
            };

            databaseStatusSection = new Models.View.NavigationSectionViewModel
            {
                Glyph = "\uF1B8",
                Title = "Actualización de Datos",
                SelectionFunc = () => new DatabaseStatusPage()
            };

            var cpccSectionGroup = new Models.View.NavigationSectionGroupViewModel
            {
                Title = "Callejón de los Milagros"
            };

            cpccSectionGroup.Add(homeSection);
            cpccSectionGroup.Add(cinemaBillboardSection);
            cpccSectionGroup.Add(nextEventsSection);
            cpccSectionGroup.Add(new Models.View.NavigationSectionViewModel
            {
                Glyph = "\uF395",
                Title = "Portal del Proyecto",
                SelectionFunc = () => new ProjectWebPortalPage()
            });

            var appSectionGroup = new Models.View.NavigationSectionGroupViewModel
            {
                Title = "Aplicación"
            };

            appSectionGroup.Add(databaseStatusSection);
            appSectionGroup.Add(new Models.View.NavigationSectionViewModel
            {
                Glyph = "\uF2D7",
                Title = "Información y Ayuda",
                SelectionFunc = () => new SupportPage()
            });
            appSectionGroup.Add(new Models.View.NavigationSectionViewModel
            {
                Glyph = "\uFC31",
                Title = "Perfil de Usuario",
                SelectionFunc = () => new ProfileOverviewPage()
            });
            appSectionGroup.Add(new Models.View.NavigationSectionViewModel
            {
                Glyph = "\uF5FD",
                Title = "Cerrar Sesión",
                SelectionFunc = () => new SignOutPage()
            });

            BindingContext = new Models.Page.MasterPageModel
            {
                NavigationSectionGroups = new System.Collections.Generic.List<Models.View.NavigationSectionGroupViewModel>
                {
                    cpccSectionGroup,
                    appSectionGroup
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
            {
                var safeInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();

                NavigationListView.Margin = new Xamarin.Forms.Thickness(0, safeInsets.Top, 0, 0);
            }
        }

        public void PresentHomeSection(bool resetScroll = false)
        {
            NavigationListView.SelectedItem = homeSection;

            if (resetScroll)
            {
                NavigationListView.ScrollTo(homeSection, Xamarin.Forms.ScrollToPosition.End, false);
            }
        }

        public void PresentDatabaseStatusSection()
        {
            NavigationListView.SelectedItem = databaseStatusSection;
        }

        public void PresentCinemaBillboardSection()
        {
            NavigationListView.SelectedItem = cinemaBillboardSection;
        }

        public void PresentEventsSection()
        {
            NavigationListView.SelectedItem = nextEventsSection;
        }

        private void NavigationListView_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (!IsEnabled)
            {
                IsEnabled = true;

                return;
            }

            ((Parent as Xamarin.Forms.MasterDetailPage).Detail as Xamarin.Forms.NavigationPage).PopToRootAsync(false);

            var outgoingPage = (Parent as Xamarin.Forms.MasterDetailPage).Detail;

            var outgoingNavigationPage = (Parent as Xamarin.Forms.MasterDetailPage).Detail as Xamarin.Forms.NavigationPage;

            var incomingNavigationPage = new Xamarin.Forms.NavigationPage((e.SelectedItem as Models.View.NavigationSectionViewModel).SelectionFunc.Invoke());

            incomingNavigationPage.BarBackgroundColor = outgoingNavigationPage.BarBackgroundColor;
            incomingNavigationPage.BarTextColor = outgoingNavigationPage.BarTextColor;

            (Parent as Xamarin.Forms.MasterDetailPage).Detail = incomingNavigationPage;

            if (outgoingPage is System.IDisposable)
            {
                (outgoingPage as System.IDisposable).Dispose();
            }

            System.Threading.Tasks.Task.Delay(System.TimeSpan.FromMilliseconds(100)).ContinueWith(task =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    (Parent as Xamarin.Forms.MasterDetailPage).IsPresented = false;
                });
            });
        }
    }
}