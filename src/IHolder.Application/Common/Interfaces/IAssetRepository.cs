namespace IHolder.Application.Common.Interfaces;

public interface IAssetRepository
{
    Task<bool> ExistsByProductIdAsync(Guid productId);
}