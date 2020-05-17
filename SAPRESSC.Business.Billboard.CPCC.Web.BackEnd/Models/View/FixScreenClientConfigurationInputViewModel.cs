using SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Web.BackEnd.Models.View
{
    public class FixScreenClientConfigurationInputViewModel : Core.Web.Management.Models.View.IEntityInputViewModel<FixScreenClientConfiguration, System.Guid>
    {
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Identificador")]
        public System.Guid Id { get; set; }

        public bool Enabled { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Acerca de")]
        public string Description { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage"), MaxLength(255, ErrorMessage = "MaxLengthValidationErrorMessage")]
        [Display(Name = "Aviso por Defecto")]
        public string AdvisesChainPlaceholder { get; set; }

        [MaxLength(100)]
        [Display(Name = "Cabecera de Avisos")]
        public string AdvisesChainWellcome { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage"), Range(2, 10, ErrorMessage = "RangeValidationErrorMessage")]
        [Display(Name = "Cantidad Máxima en Programación Diaria")]
        public int DailyRoomBillboardItemMaxCount { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage"), Range(1, 60, ErrorMessage = "RangeValidationErrorMessage")]
        [Display(Name = "Tiempo de Visualización de Programación Diaria")]
        public int DailyRoomBillboardDisplayTime { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage"), Range(1, 300, ErrorMessage = "RangeValidationErrorMessage")]
        [Display(Name = "Tiempo de Visualización de Detalles de Presentación")]
        public int PresentationDetailDisplayTime { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage"), Range(1, 300, ErrorMessage = "RangeValidationErrorMessage")]
        [Display(Name = "Tiempo de Visualización de Detalles de Evento")]
        public int EventDetailDisplayTime { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage"), Range(1, 300, ErrorMessage = "RangeValidationErrorMessage")]
        [Display(Name = "Tiempo de Visualización de Vista Previa del Cine")]
        public int CinemaOverviewDisplayTime { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage"), Range(0, 60, ErrorMessage = "RangeValidationErrorMessage")]
        [Display(Name = "Tiempo de Retardo de Inicio")]
        public int StartUpDelayTime { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Cantidad de Elementos en Lista de Reproducción")]
        public int PlaylistSidebarItemsCount { get; set; }

        public bool ShowPresentations { get; set; }

        public bool ShowEvents { get; set; }

        public FixScreenClientConfigurationInputViewModel()
        {
            Enabled = true;
            DailyRoomBillboardItemMaxCount = 4;
            PlaylistSidebarItemsCount = 5;
        }

        public FixScreenClientConfiguration Export()
        {
            var entity = new FixScreenClientConfiguration();

            Mergue(entity);

            return entity;
        }

        public void Import(FixScreenClientConfiguration entity)
        {
            Id = entity.Id;
            Enabled = entity.Enabled;
            Description = entity.Description;
            AdvisesChainPlaceholder = entity.AdvisesChainPlaceholder;
            AdvisesChainWellcome = entity.AdvisesChainWellcome;
            DailyRoomBillboardItemMaxCount = entity.DailyRoomBillboardItemMaxCount;
            DailyRoomBillboardDisplayTime = entity.DailyRoomBillboardDisplayTime;
            PresentationDetailDisplayTime = entity.PresentationDetailDisplayTime;
            EventDetailDisplayTime = entity.EventDetailDisplayTime;
            CinemaOverviewDisplayTime = entity.CinemaOverviewDisplayTime;
            PlaylistSidebarItemsCount = entity.PlaylistSidebarItemsCount;
            StartUpDelayTime = entity.StartUpDelayTime;
            ShowPresentations = entity.ShowPresentations;
            ShowEvents = entity.ShowEvents;
        }

        public void Mergue(FixScreenClientConfiguration entity)
        {
            entity.Id = Id;
            entity.Enabled = Enabled;
            entity.Description = Description;
            entity.AdvisesChainPlaceholder = AdvisesChainPlaceholder;
            entity.AdvisesChainWellcome = AdvisesChainWellcome;
            entity.DailyRoomBillboardItemMaxCount = DailyRoomBillboardItemMaxCount;
            entity.DailyRoomBillboardDisplayTime = DailyRoomBillboardDisplayTime;
            entity.PresentationDetailDisplayTime = PresentationDetailDisplayTime;
            entity.EventDetailDisplayTime = EventDetailDisplayTime;
            entity.CinemaOverviewDisplayTime = CinemaOverviewDisplayTime;
            entity.PlaylistSidebarItemsCount = PlaylistSidebarItemsCount;
            entity.StartUpDelayTime = StartUpDelayTime;
            entity.ShowPresentations = ShowPresentations;
            entity.ShowEvents = ShowEvents;
        }
    }
}
