using BookStore.Model.ViewModels;

namespace BookStore.Repository
{
    public class BaseRepository
    {
        protected readonly BookStoreContext _context = new BookStoreContext();
    }
}
