namespace ShopManagement.Domain.Services
{
    public interface IShopAccountAcl
    {
        (string FullName, string Mobile) GetAccountInfo(long id);
    }
}
