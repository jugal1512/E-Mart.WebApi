using E_Mart.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<SubCategories> GetSubCategoryByNameAsync(string subCategoryName)
        {
            return await _subCategoriesRepository.GetSubCategoryByNameAsync(subCategoryName);
        }

        public async Task<List<SubCategories>> SearchSubCategoryAsync(Expression<Func<SubCategories, bool>> predicate)
        {
            return await _subCategoriesRepository.SearchSubCategoryAsync(predicate);
        }
    }
}
