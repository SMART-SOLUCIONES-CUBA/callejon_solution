using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class ProfileEditPage : XFToolKit.Mvvm.Pages.ContentPage
    {
        private bool imagePicked;
        private bool imageCropping;

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

            if (imageCropping)
            {
                (BindingContext as Models.Page.ProfileEditPageModel).IsBusy = false;

                imageCropping = false;
            }
        }

        protected void SaveButton_Clicked(object sender, System.EventArgs e)
        {
            if (!IsEnabled || imageCropping) return;

            var profile = (BindingContext as Models.Page.ProfileEditPageModel).Profile;

            if (profile.IsValid())
            {
                (BindingContext as Models.Page.ProfileEditPageModel).IsBusy = true;

                System.Threading.Tasks.Task.Run(async () =>
                {
                    if (imagePicked)
                    {
                        using (var profileImageFileStream = System.IO.File.OpenWrite(Helpers.LocalFileStorageHelper.GetImageFilePath(profile.Id)))
                        {
                            using (var pickImageFileStream = new System.IO.FileStream((ProfileImage.Source as Xamarin.Forms.FileImageSource).File, System.IO.FileMode.Open))
                            {
                                pickImageFileStream.CopyTo(profileImageFileStream);
                            }
                        }
                    }

                    await Managers.ProfileManager.Instance.SaveAsCurrentProfileAsync(profile);

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        ((Xamarin.Forms.Application.Current.MainPage as Xamarin.Forms.MasterDetailPage).Master.BindingContext as Models.Page.MasterPageModel).Profile = profile;

                        this.NavigationPopPageAsync();
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
            var cropper = new Stormlion.ImageCropper.ImageCropper()
            {
                PageTitle = "Seleccionar Imágen de Perfil",
                AspectRatioX = 1,
                AspectRatioY = 1,
                CropShape = Stormlion.ImageCropper.ImageCropper.CropShapeType.Rectangle,
                SelectSourceTitle = "Seleccionar desde",
                TakePhotoTitle = "Cámara",
                PhotoLibraryTitle = "Galería de Imágenes",
                Success = (imageFile) =>
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        ProfileImage.Source = Xamarin.Forms.ImageSource.FromFile(imageFile);

                        imagePicked = true;
                        imageCropping = false;

                        (BindingContext as Models.Page.ProfileEditPageModel).IsBusy = false;
                        (BindingContext as Models.Page.ProfileEditPageModel).BusyProgressMessage = string.Empty;
                    });
                },
                Faiure = () =>
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        imageCropping = false;

                        (BindingContext as Models.Page.ProfileEditPageModel).IsBusy = false;
                        (BindingContext as Models.Page.ProfileEditPageModel).BusyProgressMessage = string.Empty;
                    });                    
                }
            };

            Plugin.Media.Abstractions.MediaFile file;

            var action = await DisplayActionSheet(cropper.SelectSourceTitle, cropper.CancelButtonTitle, null, cropper.TakePhotoTitle, cropper.PhotoLibraryTitle);

            if (action == cropper.TakePhotoTitle)
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

                    cropper.Faiure?.Invoke();

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

                file = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions());
            }
            else if (action == cropper.PhotoLibraryTitle)
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

                    cropper.Faiure?.Invoke();

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

                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync();
            }
            else
            {
                return;
            }

            if (file == null)
            {
                cropper.Faiure?.Invoke();

                return;
            }

            await System.Threading.Tasks.Task.Delay(System.TimeSpan.FromMilliseconds(100));

            imageCropping = true;

            Xamarin.Forms.DependencyService.Get<Stormlion.ImageCropper.IImageCropperWrapper>().ShowFromFile(cropper, file.Path);
        }
    }
}