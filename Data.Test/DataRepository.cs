using Data.Test.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Test
{
    public class DataRepository
    {
        private DataContext _context;

        public DataRepository()
        {
            this._context = new DataContext();
        }

        public async Task<Dictionary<string, object>> Get(int pageId, int maxRows)
        {
            var assets = await _context.Assets.OrderBy(asset => asset.AssetId)
                        .Skip((pageId - 1) * maxRows)
                        .Take(maxRows).ToListAsync();

            double pageCount = (double)((decimal)_context.Assets.Count() / Convert.ToDecimal(maxRows));

            Dictionary<string, object> result = new Dictionary<string, object>
                {
                    { "data", assets },
                    { "totalPages", (int)Math.Ceiling(pageCount) },
                    { "currentPage", pageId }
                };
            return result;
        }

        public Asset GetAssetById(string id)
        {
            return _context.Assets.Where(x => x.AssetId == id).FirstOrDefault();
        }

        public Asset SaveAsset(Asset asset)
        {
            asset.AssetId = Guid.NewGuid().ToString();
            _context.Assets.Add(asset);
            _context.SaveChanges();
            return asset;
        }

        public Asset UpdateAsset(string id, Asset asset)
        {
            _context.Entry(asset).State = EntityState.Modified;
            _context.SaveChanges();
            return asset;
        }

        public Asset Delete(string id)
        {
            Asset asset = _context.Assets.Where(x => x.AssetId == id).FirstOrDefault();
            if (asset != null)
            {
                _context.Assets.Remove(asset);
                _context.SaveChanges();
            }
            return asset;
        }

        public void SaveAssetDataFromCSV(List<Asset> assets)
        {
            // Insert the available country list
            Dictionary<string, int> countryList = assets.GroupBy(x => x.Country.CountryName).Select(x => new
            {
                Key = x.Key.ToString()
            }).ToDictionary(t => t.Key, t => 0);

            List<string> countryListKeys = countryList.Keys.ToList();

            foreach (var countryKey in countryListKeys)
            {
                Country country = _context.Countries.Where(x => x.CountryName == countryKey).FirstOrDefault();
                if (country == null)
                {
                    country = new Country
                    {
                        CountryName = countryKey
                    };
                    _context.Countries.Add(country);
                }
                _context.SaveChanges();
                countryList[countryKey] = country.CountryId;
            }

            // Insert the available mime-type list
            Dictionary<string, int> mimeTypeList = assets.GroupBy(x => x.MimeType.Type).Select(x => new
            {
                Key = x.Key.ToString()
            }).ToDictionary(t => t.Key, t => 0);

            List<string> mimeTypeListKeys = countryList.Keys.ToList();

            foreach (var typeKey in mimeTypeListKeys)
            {
                MimeType type = _context.MimeTypes.Where(x => x.Type == typeKey).FirstOrDefault();
                if (type == null)
                {
                    type = new MimeType
                    {
                        Type = typeKey
                    };
                    _context.MimeTypes.Add(type);
                }
                _context.SaveChanges();
                countryList[typeKey] = type.MimeTypeId;
            }

            // Insert the Asset
            int count = 1;
            int commitCount = 500;
            foreach (Asset asset in assets)
            {
                if (!_context.Assets.Any(x => x.AssetId == asset.AssetId))
                {
                    asset.CountryId = countryList[asset.Country.CountryName];
                    asset.MimeTypeId = mimeTypeList[asset.MimeType.Type];
                    asset.CreatedOn = DateTime.UtcNow;
                    _context.Assets.Add(asset);
                }
                else
                {
                    Console.WriteLine($"Asset with ID:{asset.AssetId} already exists!");
                }

                if (count % commitCount == 0)
                {
                    _context.SaveChanges();
                    _context.Dispose();
                    _context = new DataContext();
                    _context.Configuration.AutoDetectChangesEnabled = false;
                    Console.WriteLine($"Processing at Row# {count}");
                }
                count++;
            }
            _context.SaveChanges();
            Console.WriteLine($"Total Records processed = {assets.Count()}");
        }
    }
}
