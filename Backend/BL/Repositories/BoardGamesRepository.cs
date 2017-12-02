using DAL.Entity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BL.Repositories
{
    public class BoardGamesRepository : BaseRepository<BoardGame>
    {
        private const int PAGE_SIZE = 20;

        public BoardGamesRepository(DbContext Context) : base(Context) { }

        public BoardGame ByBGGId(int Id)
        {
            return Context.Set<BoardGame>().Where(i => i.BGGId == Id).FirstOrDefault();
        }
        public IEnumerable<BoardGame> GetList(int page)
        {
            var games = Context.Set<BoardGame>().OrderBy(x => x.Id).Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE).ToList();
            this.LoadRealtions(games);
            return games;
        }

        public void Update(BoardGame game)
        {
            this.Context.SaveChanges();
        }
        
        public IEnumerable<BoardGame> GetAll()
        {
            return Context.Set<BoardGame>().ToList();
        }

        public IEnumerable<BoardGame> GetByFulltextSearch(string term)
        {
            var games = Context.Set<BoardGame>().Where(x => x.Name.Contains(term)).ToList();
            this.LoadRealtions(games);
            return games;
        }

        public BoardGame GetWithRelated(int Id)
        {
            var game = Context.Set<BoardGame>().Where(i => i.Id == Id).FirstOrDefault();
            this.LoadRealtions(new BoardGame[] { game });
            return game;
        }

        public BoardGame GetWithCategories(int Id)
        {
            var game = Context.Set<BoardGame>().Where(i => i.Id == Id).FirstOrDefault();
            Context.Entry(game).Collection(g => g.Categories).Load();
            return game;
        }

        private void LoadRealtions(IEnumerable<BoardGame> boardGames)
        {
            foreach (var game in boardGames)
            {
                Context.Entry(game).Collection(g => g.Artists).Load();
                Context.Entry(game).Collection(g => g.Categories).Load();
                Context.Entry(game).Collection(g => g.Designers).Load();
                Context.Entry(game).Collection(g => g.Publishers).Load();
            }
        }
    }
}
