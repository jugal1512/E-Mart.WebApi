using E_Mart.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Domain.Categories
{
    public class SubCategoriesService : GenericService<SubCategories>, ISubCategoriesService
    {
        private readonly ISubCategoriesRepository _subCategoriesRepository;
        public SubCategoriesService(ISubCategoriesRepository subCategoriesRepository) : base(subCategoriesRepository)
        {
            _subCategoriesRepository = subCategoriesRepository;
        }
    }
}
