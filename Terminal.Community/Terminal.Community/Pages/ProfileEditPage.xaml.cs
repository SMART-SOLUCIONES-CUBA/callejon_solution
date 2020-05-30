using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class ProfileEditPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        private byte[] imagePickedData;

        public ProfileEditPage()
        {
            InitializeComponent();

            IsEnabled = false;

            BindingContext = new Models.Page.ProfileEditPageModel
            {
                IsBusy = true
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!IsEnabled)
            {
                System.Threading.Tasks.Task.Run(() =>
                {
                    var currentProfile = Managers.ProfileManager.Instance.GetCurrentProfile();

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        (BindingContext as Models.Page.ProfileEditPageModel).Profile = currentProfile;
                        (BindingContext as Models.Page.ProfileEditPageModel).IsBusy = false;

                        var converter = new Converters.ProfileViewModelToImageSourceConverter();

                        ProfileImage.Source = (Xamarin.Forms.ImageSource)converter.Convert(currentProfile, null, null, null);

                        IsEnabled = true;
                    });                    
                });

                return;
            }
        }

        protected void SaveButton_Clicked(object sender, System.EventArgs e)
        {
            if (!IsEnabled) return;

            var profile = (BindingContext as Models.Page.ProfileEditPageModel).Profile;

            if (profile.IsValid())
            {
                (BindingContext as Models.Page.ProfileEditPageModel).IsBusy = true;

                System.Threading.Tasks.Task.Run(async () =>
                {
                    if (imagePickedData != null)
                    {
                        using (var profileImageFileStream = System.IO.File.OpenWrite(Helpers.LocalFileStorageHelper.GetImageFilePath(profile.Id)))
                        {
                            await profileImageFileStream.WriteAsync(imagePickedData, 0, imagePickedData.Length);
                        }
                    }

                    await Managers.ProfileManager.Instance.SaveAsCurrentProfileAsync(profile);

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        ((Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Master.BindingContext as Models.Page.MasterPageModel).Profile = profile;

                        this.NavigationPopPageAsync();

                        imagePickedData = null;
                    });                    
                });
            }
            else
            {
                DisplayAlert("Aviso", "Para continuar es necesario completar correctamente los campos solicitados.", "Aceptar");
            }
        }

        private async void PickImageButton_Clicked(object sender, System.EventArgs eventArgs)
        {
            Plugin.Media.Abstractions.MediaFile mediaFile;

            var action = await DisplayActionSheet("Seleccionar Imágen de Perfil desde", "Cancelar", null, Providers.ConstantsProvider.ACTIONSHEET_PICKIMAGE_OPTION_CAMARA, Providers.ConstantsProvider.ACTIONSHEET_PICKIMAGE_OPTION_GALLERY);

            if (action == Providers.ConstantsProvider.ACTIONSHEET_PICKIMAGE_OPTION_CAMARA)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    (BindingContext as Models.Page.ProfileEditPageModel).IsBusy = true;
                    (BindingContext as Models.Page.ProfileEditPageModel).BusyProgressMessage = "Capturando imagen para el perfil ...";
                });            

                await Plugin.Media.CrossMedia.Current.Initialize();

                if (!Plugin.Media.CrossMedia.Current.IsCameraAvailable || !Plugin.Media.CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        (BindingContext as Models.Page.ProfileEditPageModel).IsBusy = false;
                        (BindingContext as Models.Page.ProfileEditPageModel).BusyProgressMessage = string.Empty;
                    });

                    return;
                }

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1)).ContinueWith(delayTask =>
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            (BindingContext as Models.Page.ProfileEditPageModel).BusyProgressMessage = "Preparando imagen para recortar ...";
                        });
                    });
                });

                mediaFile = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions());
            }
            else if (action == Providers.ConstantsProvider.ACTIONSHEET_PICKIMAGE_OPTION_GALLERY)
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    (BindingContext as Models.Page.ProfileEditPageModel).IsBusy = true;
                    (BindingContext as Models.Page.ProfileEditPageModel).BusyProgressMessage = "Seleccionando imagen para el perfil ...";
                });                

                await Plugin.Media.CrossMedia.Current.Initialize();

                if (!Plugin.Media.CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Error", "This device is not supported to pick photo.", "OK");

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        (BindingContext as Models.Page.ProfileEditPageModel).IsBusy = false;
                        (BindingContext as Models.Page.ProfileEditPageModel).BusyProgressMessage = string.Empty;
                    });

                    return;
                }

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(1)).ContinueWith(delayTask =>
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            (BindingContext as Models.Page.ProfileEditPageModel).BusyProgressMessage = "Preparando imagen para recortar ...";
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
                    (BindingContext as Models.Page.ProfileEditPageModel).IsBusy = false;
                    (BindingContext as Models.Page.ProfileEditPageModel).BusyProgressMessage = string.Empty;
                });

                return;
            }

            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromMilliseconds(100));

            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                (BindingContext as Models.Page.ProfileEditPageModel).BusyProgressMessage = "Preparando imagen para recortar ...";
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
                (BindingContext as Models.Page.ProfileEditPageModel).BusyProgressMessage = "Recortando imagen ...";
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

            (BindingContext as Models.Page.ProfileEditPageModel).IsBusy = false;
            (BindingContext as Models.Page.ProfileEditPageModel).BusyProgressMessage = string.Empty;
        }
    }
}