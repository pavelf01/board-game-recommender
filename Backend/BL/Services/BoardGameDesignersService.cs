using BL.Repositories;
using DAL.Entity;

namespace BL.Services
{
    public class BoardGameDesignersService : BGGItemService<BoardGameDesigner,int,string>
    {
        private readonly BoardGameDesignersRepository _repository;

        public BoardGameDesignersService(BoardGameDesignersRepository BoardGameDesignersRepository)
        {
            this._repository = BoardGameDesignersRepository;
        }
        public override void Create(BoardGameDesigner BoardGameDesigner)
        {
            _repository.Insert(BoardGameDesigner);
        }

        public override BoardGameDesigner Get(int BoardGameDesignerId)
        {
            return _repository.GetById(BoardGameDesignerId);
        }
        public override BoardGameDesigner GetByBGGIdentifier(string Key)
        {
            return this._repository.ByName(Key);
        }
    }
}
