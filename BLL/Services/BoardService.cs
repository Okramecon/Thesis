using BLL.Services.Bases;
using DAL.EF;
using DAL.Entities;

namespace BLL.Services
{
    public class BoardService : EntityService<Board, int>
    {
        public BoardService(AppDbContext context) : base(context, context.Boards) { }

    }
}
