using BusinessLogicLayer.DataModel;
namespace BusinessLogicLayer.Interfaces
{
    public interface ITagService
    {
        void Create(TagDataModel tagDataModel);
        void Edit(TagDataModel tagDataModel);
        void Delete(int id);
        void Dispose();
    }
}
