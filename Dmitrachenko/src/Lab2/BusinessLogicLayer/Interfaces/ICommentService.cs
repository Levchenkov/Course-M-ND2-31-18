using BusinessLogicLayer.DataModel;

namespace BusinessLogicLayer.Interfaces
{
    public interface ICommentService
    {
        void Create(CommentDataModel commentDataModel);
        CommentDataModel Get(int id);
    }
}
