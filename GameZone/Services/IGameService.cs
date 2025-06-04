namespace GameZone.Services
{
    public interface IGameService
    {
        IEnumerable<Game>GetAll();
        Game? GetById(int id);
        Task create(CreateGameFormViewModel model);
        Task<Game?> Update(EditGameFormViewmodel model);
        bool Delete(int id);
    }
}
