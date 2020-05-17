using SmartSolucionesCuba.XFToolKit.Mvvm.Extensions;

namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class SessionConfigurationPage : XFToolKit.Mvvm.Pages.CarouselPage
    {
        private bool imagePicked;
        private bool imageCropping;

        public SessionConfigurationPage()
        {
            InitializeComponent();

            BindingContext = new Models.Page.SessionConfigurationPageModel();

            ProfileImage.Source = Providers.ImageSourceProvider.Instance.ProfileMaleDefault();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (imageCropping)
            {
                (BindingContext as Models.Page.SessionConfigurationPageModel).IsBusy = false;

                imageCropping = false;
            }
        }        

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            if (Children.IndexOf(CurrentPage) == 2)
            {
                if ((BindingContext as Models.Page.SessionConfigurationPageModel).Profile.IsValid())
                {
                    if (imagePicked)
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            using (var profileImageFileStream = System.IO.File.OpenWrite(Helpers.LocalFileStorageHelper.GetImageFilePath((BindingContext as Models.Page.SessionConfigurationPageModel).Profile.Id)))
                            {
                                using (var pickImageFileStream = new System.IO.FileStream((ProfileImage.Source as Xamarin.Forms.FileImageSource).File, System.IO.FileMode.Open))
                                {
                                    pickImageFileStream.CopyTo(profileImageFileStream);
                                }
                            }
                        });
                    }
                }
                else
                {
                    CurrentPage = Children[1];

                    DisplayAlert("Aviso", "Para continuar es necesario completar correctamente los campos solicitados.", "Aceptar");
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
                    ProfileImage.Source = Xamarin.Forms.ImageSource.FromFile(imageFile);

                    imagePicked = true;
                    imageCropping = false;

                    (BindingContext as Models.Page.SessionConfigurationPageModel).IsBusy = false;
                },
                Faiure = () =>
                {
                    imageCropping = false;

                    (BindingContext as Models.Page.SessionConfigurationPageModel).IsBusy = false;
                }
            };

            Plugin.Media.Abstractions.MediaFile file;

            var action = await DisplayActionSheet(cropper.SelectSourceTitle, cropper.CancelButtonTitle, null, cropper.TakePhotoTitle, cropper.PhotoLibraryTitle);

            if (action == cropper.TakePhotoTitle)
            {
                (BindingContext as Models.Page.SessionConfigurationPageModel).IsBusy = true;

                await Plugin.Media.CrossMedia.Current.Initialize();

                if (!Plugin.Media.CrossMedia.Current.IsCameraAvailable || !Plugin.Media.CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");

                    cropper.Faiure?.Invoke();
                    
                    return;
                }

                file = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions());
            }
            else if (action == cropper.PhotoLibraryTitle)
            {
                (BindingContext as Models.Page.SessionConfigurationPageModel).IsBusy = true;

                await Plugin.Media.CrossMedia.Current.Initialize();

                if (!Plugin.Media.CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Error", "This device is not supported to pick photo.", "OK");

                    cropper.Faiure?.Invoke();
                    
                    return;
                }

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