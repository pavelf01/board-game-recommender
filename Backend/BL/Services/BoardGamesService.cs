using BL.Repositories;
using DAL.Entity;

namespace BL.Services
{
    public class BoardGamesService : BGGItemService<BoardGame,int,int>
    {
        private readonly BoardGamesRepository _repository;

        public BoardGamesService(BoardGamesRepository BoardGamesRepository)
        {
            this._repository = BoardGamesRepository;
        }
        public override void Create(BoardGame BoardGame)
        {
            _repository.Insert(BoardGame);
        }

        public override BoardGame Get(int BoardGameId)
        {
            return _repository.GetById(BoardGameId);
        }
        public override BoardGame GetByBGGIdentifier(int Key)
        {
            return _repository.ByBGGId(Key);
        }
    }
}
