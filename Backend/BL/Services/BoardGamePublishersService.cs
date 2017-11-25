using BL.Repositories;
using DAL.Entity;

namespace BL.Services
{
    public class BoardGamePublishersService : BGGItemService<BoardGamePublisher,string>
    {
        private readonly BoardGamePublishersRepository _repository;

        public BoardGamePublishersService(BoardGamePublishersRepository BoardGamePublishersRepository)
        {
            this._repository = BoardGamePublishersRepository;
        }
        public override void Create(BoardGamePublisher BoardGamePublisher)
        {
            _repository.Insert(BoardGamePublisher);
        }

        public override BoardGamePublisher Get(int BoardGamePublisherId)
        {
            return _repository.GetById(BoardGamePublisherId);
        }

        public override BoardGamePublisher GetByBGGIdentifier(string Key)
        {
            return this._repository.ByName(Key);
        }
    }
}
