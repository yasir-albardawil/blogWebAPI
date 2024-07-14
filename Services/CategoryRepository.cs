
using BiteWebAPI.DBContext;
using BiteWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BiteWebAPI.Services
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly BiteDbContext _context;

        public CategoryRepository(BiteDbContext context)
        {
            _context = context;
        }
        /** public IEnumerable<Category> AllCategories =>
            new List<Category>
            {
            new Category{CategoryId=1, CategoryName="Fruit categorys", Description="All-fruity categorys"},
            new Category{CategoryId=2, CategoryName="Cheese cakes", Description="Cheesy all the way"},
            new Category{CategoryId=3, CategoryName="Seasonal categorys", Description="Get in the mood for a seasonal category"}
            }; */

        public IEnumerable<Category> AllCategories
        {
            get
            {
                return _context.Categories;
            }
        }

        public async Task<Category?> GeCategoryByIdAsync(int? id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task AddAsync(Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            var items = _context.Categories.Include(c => c.CategoryId);
            if (category != null)
            {
                _context.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Category?> FindAsync(int? id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _context.Categories.AnyAsync(p => p.CategoryId == id);
        }

        public async Task<bool> DoesCategoryHaveItemsAsync(int id)
        {
            return await _context.Items.AnyAsync(p => p.CategoryId == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
