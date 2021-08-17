using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MegaTFLT.Models.Edq.Models;
using MegaTFLT.Models.MegaEcm.MapProfiles;
using MegaTFLT.Models.MegaEcm.Models;
using MegaTFLT.Utilitys;

namespace MegaTFLT.Services.EdqServices
{
    public class EdqServiceFactory
    {
        private readonly static EdqConfigModel _edqConfig = ConfigUtility.EdqConfigModel;
        private readonly static string _baseUrl = _edqConfig.EdqBaseUrl;
        private readonly static string _nameAddressUrl = $"{_baseUrl}{_edqConfig.NameAddress}";
        private readonly static string _countryCityUrl = $"{_baseUrl}{_edqConfig.CountryCity}";
        private readonly static string _goodsUrl = $"{_baseUrl}{_edqConfig.Goods}";
        private readonly static string _narrativeUrl = $"{_baseUrl}{_edqConfig.Narrative}";
        private readonly static string _portUrl = $"{_baseUrl}{_edqConfig.Port}";
        private readonly static string _bicUrl = $"{_baseUrl}{_edqConfig.BIC}";
        public static async Task Screen(string screenType, EdqRequestModel edqRequestModel, EdqResponseModel edqResponseModel)
        {
            // screenType = screenType.ToUpper();
            using (HttpClientSerivce service = new HttpClientSerivce())
            {
                switch (screenType)
                {
                    case "NameAddressRequestModels":
                        if (edqRequestModel.NameAddressRequestModels.Count() > 0)
                        {
                            edqResponseModel.NameAddressResponseModels = await service.PostAsync<List<NameAddressRequestModel>, List<NameAddressResponseModel>>(_nameAddressUrl, edqRequestModel.NameAddressRequestModels);
                        }
                        break;
                    case "CountryCityRequestModels":
                        if (edqRequestModel.CountryCityRequestModels.Count() > 0)
                        {
                            edqResponseModel.CountryCityResponseModels = await service.PostAsync<List<CountryCityRequestModel>, List<CountryCityResponseModel>>(_countryCityUrl, edqRequestModel.CountryCityRequestModels);
                        }
                        break;
                    case "BicRequestModels":

                        if (edqRequestModel.BicRequestModels.Count() > 0)
                        {
                            edqResponseModel.BicResponseModels = await service.PostAsync<List<BicRequestModel>, List<BicResponseModel>>(_bicUrl, edqRequestModel.BicRequestModels);
                        }
                        break;
                    case "NarrativeRequestModels":
                        if (edqRequestModel.NarrativeRequestModels.Count() > 0)
                        {
                            edqResponseModel.NarrativeResponseModels = await service.PostAsync<List<NarrativeRequestModel>, List<NarrativeResponseModel>>(_narrativeUrl, edqRequestModel.NarrativeRequestModels);
                        }
                        break;
                    case "PortRequestModels":
                        if (edqRequestModel.PortRequestModels.Count() > 0)
                        {
                            edqResponseModel.PortResponseModels = await service.PostAsync<List<PortRequestModel>, List<PortResponseModel>>(_portUrl, edqRequestModel.PortRequestModels);
                        }
                        break;
                    case "GoodsRequestModels":
                        if (edqRequestModel.GoodsRequestModels.Count() > 0)
                        {
                            edqResponseModel.GoodsResponseModels = await service.PostAsync<List<GoodsRequestModel>, List<GoodsResponseModel>>(_goodsUrl, edqRequestModel.GoodsRequestModels);
                        }
                        break;
                    default:
                        throw new NotImplementedException($"Request Type Error {screenType}");
                };
            }
        }

        
        
    }
}
