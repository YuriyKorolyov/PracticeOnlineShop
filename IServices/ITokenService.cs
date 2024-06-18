using MyApp.Models;

namespace MyApp.IServices
{
    /// <summary>
    /// Интерфейс для создания JWT-токена аутентификации.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Создает JWT-токен на основе пользователя и списка его ролей.
        /// </summary>
        /// <param name="user">Пользователь для которого создается токен.</param>
        /// <param name="roles">Список ролей пользователя, которые будут включены в токен.</param>
        /// <returns>Сгенерированный JWT-токен.</returns>
        string CreateToken(User user, IList<string> roles);
    }
}
