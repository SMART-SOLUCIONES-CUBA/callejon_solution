namespace SmartSolucionesCuba.SAPRESSC.Business.Billboard.CPCC.Persistence.Entities
{
    public class ProductionComment : Comment
    {
        public ProductionComment()
        {
            Delations = new System.Collections.Generic.List<ProductionCommentDelation>();
        }

        public Cinema.Persistence.Entities.Production Production { get; set; }

        public ProductionComment Reference { get; set; }

        public System.Collections.Generic.ICollection<ProductionCommentDelation> Delations { get; set; }
    }
}
