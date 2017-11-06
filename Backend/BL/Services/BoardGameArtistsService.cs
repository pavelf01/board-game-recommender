using BL.Repositories;
using DAL.Entity;

namespace BL.Services
{
    public class BoardGameArtistsService : BGGItemService<BoardGameArtist, string>
    {
        private readonly BoardGameArtistsRepository _repository;

        public BoardGameArtistsService(BoardGameArtistsRepository BoardGameArtistsRepository)
        {
            this._repository = BoardGameArtistsRepository;
        }
        public override void Create(BoardGameArtist BoardGameArtist)
        {
            _repository.Insert(BoardGameArtist);
        }

        public override BoardGameArtist Get(int ID)
        {
            return _repository.GetById(ID);
        }
        public override BoardGameArtist GetByBGGIdentifier(string Key)
        {
            return this._repository.ByName(Key);
        }
    }
}
