using BL.Repositories;
using DAL.Entity;
using System.Collections.Generic;
using System;

namespace BL.Services
{
    public class BoardGamesService : BGGItemService<BoardGame,int>
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

        public BoardGame GetWithRelated(int BoardGameId)
        {
            return _repository.GetWithRelated(BoardGameId);
        }

        public override BoardGame Get(int ID)
        {
            return _repository.GetById(ID);
        }

        public override BoardGame GetByBGGIdentifier(int Key)
        {
            return _repository.ByBGGId(Key);
        }

        public IEnumerable<BoardGame> GetList(int page)
        {
            return _repository.GetList(page);
        }

        public IEnumerable<BoardGame> GetByFulltextSearch(string term)
        {
            return _repository.GetByFulltextSearch(term);
        }
    }
}
