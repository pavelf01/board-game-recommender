using BL.Repositories;
using DAL.Entity;

namespace BL.Services
{
    public class BoardGameCategoriesService : BGGItemService<BoardGameCategory,int,string>
    {
        private readonly BoardGameCategoriesRepository _repository;

        public BoardGameCategoriesService(BoardGameCategoriesRepository BoardGameCategoriesRepository)
        {
            this._repository = BoardGameCategoriesRepository;
        }
        public override void Create(BoardGameCategory BoardGameCategory)
        {
            _repository.Insert(BoardGameCategory);
        }

        public override BoardGameCategory Get(int BoardGameCategoryId)
        {
            return _repository.GetById(BoardGameCategoryId);
        }
        public override BoardGameCategory GetByBGGIdentifier(string Key)
        {
            return this._repository.ByName(Key);
        }
    }
}
