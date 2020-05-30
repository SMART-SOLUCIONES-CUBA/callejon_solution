using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class SessionConfigurationPage : XFToolKit.Mvvm.Pages.CarouselPage
    {
        private byte[] imagePickedData;

        public SessionConfigurationPage()
        {
            InitializeComponent();

            BindingContext = new Models.Page.SessionConfigurationPageModel();

            ProfileImage.Source = Providers.ImageSourceProvider.Instance.ProfileMaleDefault();
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            if (Children.IndexOf(CurrentPage) == 2)
            {
                if ((BindingContext as Models.Page.SessionConfigurationPageModel).Profile.IsValid())
                {
                    if (imagePickedData != null)
                    {
                        using (var profileImageFileStream = System.IO.File.OpenWrite(Helpers.LocalFileStorageHelper.GetImageFilePath((BindingContext as Models.Page.SessionConfigurationPageModel).Profile.Id)))
                        {
                            profileImageFileStream.Write(imagePickedData, 0, imagePickedData.Length);
                        }
                    }
                }
                else
                {
                    DisplayAlert("Aviso", "Para continuar es necesario completar correctamente los campos solicitados.", "Aceptar").ContinueWith(task =>
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            CurrentPage = Children[1];
                        });
                    });
                }
            }
        }

        protected void NextPageButton_Clicked(object sender, System.EventArgs e)
        {
            var currentPageIndex = Children.IndexOf(CurrentPage);

            if (currentPageIndex == Children.Count - 1) return;

            CurrentPage = Children[currentPageIndex + 1];
        }

        protected async void EndSetupButton_Clicked(object sender, System.EventArgs e)
        {
            var profile = (BindingContext as Models.Page.SessionConfigurationPageModel).Profile;

            await Managers.ProfileManager.Instance.SaveAsCurrentProfileAsync(profile);

            await this.NavigationPopModalPageAsync();
        }

        private async void PickImageButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            Plugin.Media.Abstractions.MediaFile mediaFile;

            var action = await DisplayActionSheet("Seleccionar Imágen de Perfil desde", "Cancelar", null, Providers.ConstantsProvider.ACTIONSHEET_PICKIMAGE_OPTION_CAMARA, Providers.ConstantsProvider.ACTIONSHEET_PICKIMAGE_OPTION_GALLERY);

            if (action == Providers.ConstantsProvider.ACTIONSHEET_PICKIMAGE_OPTION_CAMARA)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    (BindingContext as Models.Page.SessionConfigurationPageModel).IsBusy = true;
                    (BindingContext as Models.Page.SessionConfigurationPageModel).BusyProgressMessage = "Capturando imagen para el perfil ...";
                });

                await Plugin.Media.CrossMedia.Current.Initialize();

                if (!Plugin.Media.CrossMedia.Current.IsCameraAvailable || !Plugin.Media.CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        (BindingContext as Models.Page.SessionConfigurationPageModel).IsBusy = false;
                        (BindingContext as Models.Page.SessionConfigurationPageModel).BusyProgressMessage = string.Empty;
                    });

                    return;
                }

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1)).ContinueWith(delayTask =>
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            (BindingContext as Models.Page.SessionConfigurationPageModel).BusyProgressMessage = "Preparando imagen para recortar ...";
                        });
                    });
                });

                mediaFile = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions());
            }
            else if (action == Providers.ConstantsProvider.ACTIONSHEET_PICKIMAGE_OPTION_GALLERY)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    (BindingContext as Models.Page.SessionConfigurationPageModel).IsBusy = true;
                    (BindingContext as Models.Page.SessionConfigurationPageModel).BusyProgressMessage = "Seleccionando imagen para el perfil ...";
                });

                await Plugin.Media.CrossMedia.Current.Initialize();

                if (!Plugin.Media.CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Error", "This device is not supported to pick photo.", "OK");

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        (BindingContext as Models.Page.SessionConfigurationPageModel).IsBusy = false;
                        (BindingContext as Models.Page.SessionConfigurationPageModel).BusyProgressMessage = string.Empty;
                    });

                    return;
                }

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1)).ContinueWith(delayTask =>
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            (BindingContext as Models.Page.SessionConfigurationPageModel).BusyProgressMessage = "Preparando imagen para recortar ...";
                        });
                    });
                });

                mediaFile = await Plugin.Media.CrossMedia.Current.PickPhotoAsync();
            }
            else
            {
                return;
            }

            if (mediaFile == null)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    (BindingContext as Models.Page.SessionConfigurationPageModel).IsBusy = false;
                    (BindingContext as Models.Page.SessionConfigurationPageModel).BusyProgressMessage = string.Empty;
                });

                return;
            }

            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromMilliseconds(100));

            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                (BindingContext as Models.Page.SessionConfigurationPageModel).BusyProgressMessage = "Preparando imagen para recortar ...";
            });

            SkiaSharp.SKBitmap croppingImageTarget = null;

            using (var imageFileStream = System.IO.File.OpenRead(mediaFile.Path))
            {
                using (var skStream = new SkiaSharp.SKManagedStream(imageFileStream))
                {
                    croppingImageTarget = SkiaSharp.SKBitmap.Decode(skStream);
                }
            }

            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                (BindingContext as Models.Page.SessionConfigurationPageModel).BusyProgressMessage = "Recortando imagen ...";
            });

            await this.NavigationPushModalPageAsync(new Xamarin.Forms.NavigationPage(new ProfileImageCroppingPage(croppingImageTarget, OnPickedImageCroppingComplete))
            {
                BarBackgroundColor = (Xamarin.Forms.Color)Xamarin.Forms.Application.Current.Resources["PrimaryBrandColor"],
                BarTextColor = Xamarin.Forms.Color.White
            });
        }

        private void OnPickedImageCroppingComplete(SkiaSharp.SKBitmap result)
        {
            if (result != null)
            {
                using (var skImage = SkiaSharp.SKImage.FromBitmap(result))
                {
                    using (var skData = skImage.Encode(SkiaSharp.SKEncodedImageFormat.Jpeg, 75))
                    {
                        imagePickedData = skData.ToArray();
                    }
                }

                result.Dispose();

                ProfileImage.Source = Xamarin.Forms.ImageSource.FromStream(() => new System.IO.MemoryStream(imagePickedData));
            }

            this.NavigationPopModalPageAsync();

            (BindingContext as Models.Page.SessionConfigurationPageModel).IsBusy = false;
            (BindingContext as Models.Page.SessionConfigurationPageModel).BusyProgressMessage = string.Empty;
        }
    }
}