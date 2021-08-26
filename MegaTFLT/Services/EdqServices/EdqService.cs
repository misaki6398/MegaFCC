using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MegaTFLT.MegaEcm.Models;
using MegaTFLT.Models.Edq.Models;
using MegaTFLT.Models.MegaEcm.MapProfiles;
using MegaTFLT.Models.MegaEcm.Models;
using MegaTFLT.Models.MegaEcm.Repositorys;
using MegaTFLT.Utilitys;
using Oracle.ManagedDataAccess.Client;

namespace MegaTFLT.Services.EdqServices
{
    public class EdqService 
    {

        private readonly IMapper _mapper;

        public EdqService()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ScreeningInputTagToRequestProfile());
                cfg.AddProfile(new EdqResponseToAlertsProfile());
            });

            _mapper = config.CreateMapper();

        }
        public async Task<List<TfAlertsModel>> ProcessScreeningAsync(Dictionary<TfScreenConfigKeyModel, List<ScreeningInputTagModel>> screeningInputTags)
        {
            var intersectKeys = screeningInputTags.Keys.Intersect(ConfigUtility.ScreenConfigs.Keys);

            EdqRequestModel edqRequestModel = new EdqRequestModel();

            this.DispatchScreenData(screeningInputTags, edqRequestModel);

            // Start Screen
            EdqResponseModel edqResponseModel = new EdqResponseModel();
            foreach (var item in edqRequestModel.GetType().GetProperties())
            {
                await EdqServiceFactory.Screen(item.Name, edqRequestModel, edqResponseModel);
            }

            var tfAlertsModels = new List<TfAlertsModel>();
            tfAlertsModels.AddRange(_mapper.Map<List<NameAddressResponseModel>, List<TfAlertsModel>>(edqResponseModel.NameAddressResponseModels));
            tfAlertsModels.AddRange(_mapper.Map<List<CountryCityResponseModel>, List<TfAlertsModel>>(edqResponseModel.CountryCityResponseModels));
            tfAlertsModels.AddRange(_mapper.Map<List<GoodsResponseModel>, List<TfAlertsModel>>(edqResponseModel.GoodsResponseModels));
            tfAlertsModels.AddRange(_mapper.Map<List<NarrativeResponseModel>, List<TfAlertsModel>>(edqResponseModel.NarrativeResponseModels));
            tfAlertsModels.AddRange(_mapper.Map<List<PortResponseModel>, List<TfAlertsModel>>(edqResponseModel.PortResponseModels));
            tfAlertsModels.AddRange(_mapper.Map<List<BicResponseModel>, List<TfAlertsModel>>(edqResponseModel.BicResponseModels));

            return tfAlertsModels;
        }

        private void DispatchScreenData(Dictionary<TfScreenConfigKeyModel, List<ScreeningInputTagModel>> screeningInputTags, EdqRequestModel edqRequestModel)
        {
            var intersectKeys = screeningInputTags.Keys.Intersect(ConfigUtility.ScreenConfigs.Keys);
            foreach (TfScreenConfigKeyModel intersectKey in intersectKeys)
            {

                if (ConfigUtility.ScreenConfigs[intersectKey].NameAddress)
                {
                    screeningInputTags[intersectKey].ForEach(screeningInputTag =>
                    {
                        edqRequestModel.NameAddressRequestModels.Add(_mapper.Map<ScreeningInputTagModel, NameAddressRequestModel>(screeningInputTag));
                    });
                }

                if (ConfigUtility.ScreenConfigs[intersectKey].CountryAndCity)
                {
                    screeningInputTags[intersectKey].ForEach(screeningInputTag =>
                    {
                        edqRequestModel.CountryCityRequestModels.Add(_mapper.Map<ScreeningInputTagModel, CountryCityRequestModel>(screeningInputTag));
                    });
                }

                if (ConfigUtility.ScreenConfigs[intersectKey].BicCode)
                {
                    screeningInputTags[intersectKey].ForEach(screeningInputTag =>
                    {
                        edqRequestModel.BicRequestModels.Add(_mapper.Map<ScreeningInputTagModel, BicRequestModel>(screeningInputTag));
                    });
                }

                if (ConfigUtility.ScreenConfigs[intersectKey].Narrative)
                {
                    screeningInputTags[intersectKey].ForEach(screeningInputTag =>
                    {
                        edqRequestModel.NarrativeRequestModels.Add(_mapper.Map<ScreeningInputTagModel, NarrativeRequestModel>(screeningInputTag));
                    });
                }

                if (ConfigUtility.ScreenConfigs[intersectKey].Port)
                {
                    screeningInputTags[intersectKey].ForEach(screeningInputTag =>
                    {
                        edqRequestModel.PortRequestModels.Add(_mapper.Map<ScreeningInputTagModel, PortRequestModel>(screeningInputTag));
                    });
                }

                if (ConfigUtility.ScreenConfigs[intersectKey].Goods)
                {
                    screeningInputTags[intersectKey].ForEach(screeningInputTag =>
                    {
                        edqRequestModel.GoodsRequestModels.Add(_mapper.Map<ScreeningInputTagModel, GoodsRequestModel>(screeningInputTag));
                    });
                }

            }
        }
    }
}