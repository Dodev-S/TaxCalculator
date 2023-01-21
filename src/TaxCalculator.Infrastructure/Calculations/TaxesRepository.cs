using Microsoft.Extensions.Caching.Memory;

namespace TaxCalculator.Domain.Calculations;

public class TaxesRepository : ITaxesRepository
{
    private readonly IMemoryCache _memoryCache;

    public TaxesRepository(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void Add(string key, Taxes taxes)
    {
        _memoryCache.Set(key, taxes);
    }

    public Taxes? Get(string key)
    {
        return _memoryCache.Get<Taxes>(key);
    }
}