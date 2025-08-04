namespace GarnetAccounting.ViewModels.CommercialViewModel.ProductsAndServicesViewModels
{
    public class CategoryTreeNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public List<CategoryTreeNode> Children { get; set; }

        public CategoryTreeNode()
        {
            Children = new List<CategoryTreeNode>();
        }
    }

}
