using Orange_Backend.ViewModels;

namespace Orange_Backend.Services.Interfaces
{
    public interface ILayoutService
    {
        Task<HeaderVM> GetHeaderDatas();
        FooterVM GetFooterDatas();
    }
}
